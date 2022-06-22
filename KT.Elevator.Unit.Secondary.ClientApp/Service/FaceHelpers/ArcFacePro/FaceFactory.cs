using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Secondary.ClientApp.Events;
using KT.Quanta.Common.Enums;
using KT.Unit.Face.Arc.Pro.Entity;
using KT.Unit.Face.Arc.Pro.Models;
using KT.Unit.FaceRecognition.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.FaceHelpers.ArcFacePro
{
    public class FaceFactory
    {
        private int _currentFaceProviderIndex = 0;
        private bool _isInited = false;

        private List<IFaceProvider> _faceProviders;
        private ArcFaceSettings _arcFaceSettings;
        private ILogger _logger;
        private IEventAggregator _eventAggregator;
        private ArcInitHelper _arcInitHelper;

        public FaceFactory()
        {
            _arcFaceSettings = ContainerHelper.Resolve<FaceRecognitionAppSettings>().ArcProFaceSettings;
            _logger = ContainerHelper.Resolve<ILogger>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _arcInitHelper = ContainerHelper.Resolve<ArcInitHelper>();

            _eventAggregator.GetEvent<AddOrEditPassRightsEvent>().Subscribe(AddOrEditPassRights);
        }

        public IFaceProvider GetRandomProvider()
        {
            if (!_isInited)
            {
                return null;
            }
            if (_faceProviders.Count <= 0)
            {
                _logger.LogWarning($"人脸识别引擎为空！");
                return null;
            }
            var random = new Random();
            var randomNum = random.Next(_faceProviders.Count - 1);

            return _faceProviders[randomNum];
        }

        /// <summary>
        /// 异步初始化权限
        /// </summary>
        /// <param name="faceRights"></param>
        internal async void InitPageDataAsync(List<UnitPassRightEntity> faceRights)
        {
            //异步 添加人脸
            var currentFaceProvider = GetAndNextCurrent();
            await currentFaceProvider.AddPassRightsAsync(faceRights);
        }

        private async void AddOrEditPassRights(List<UnitPassRightEntity> faceRights)
        {
            await AddOrEditPassRightsAsync(faceRights);
        }

        private async Task AddOrEditPassRightsAsync(List<UnitPassRightEntity> faceRights)
        {
            //添加人脸 
            if (faceRights == null || faceRights.FirstOrDefault() == null)
            {
                _logger.LogError($"要添加的权限为空！");
                return;
            }
            foreach (var item in faceRights)
            {
                await AddOrEditPassRightAsync(item);
            }
        }

        private async Task AddOrEditPassRightAsync(UnitPassRightEntity item)
        {
            try
            {
                if (item.AccessType != AccessTypeEnum.FACE.Value)
                {
                    return;
                }
                _logger.LogInformation("FaceFactory:AddOrEditPassRight:start!! ");
                await DeleteByIdAsync(item.Id);
                _logger.LogInformation("FaceFactory:AddOrEditPassRight:delete:end!! ");
                var currentFaceProvider = GetAndNextCurrent();
                _logger.LogInformation("FaceFactory:AddOrEditPassRight:getProvider:end!! ");
                await currentFaceProvider.AddPassRightAsync(item);
                _logger.LogInformation("FaceFactory:AddOrEditPassRight:end!! ");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Arc新增权限列表失败！");
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            var tasks = new List<Task>();
            foreach (var item in _faceProviders)
            {
                var task = Task.Run(() =>
                {
                    item.DeletePassRight(id);
                });
                tasks.Add(task);
            }
            await Task.WhenAll(tasks.ToArray());
        }

        /// <summary>
        /// 初始化人脸SDK  收费离线激
        /// </summary>
        /// <returns></returns>
        public async Task InitFaceProvidesAsync()
        {
            await Task.Run(() =>
            {
                _faceProviders = new List<IFaceProvider>();
                for (int i = 0; i < _arcFaceSettings.LibraryThread; i++)
                {
                    try
                    {
                        //初始化人脸识别
                        var faceProvider = ContainerHelper.Resolve<IFaceProvider>();
                        faceProvider.ProviderIndex = i;
                        faceProvider.FaceEngineProvider = _arcInitHelper.InitFaceEngine();
                        _faceProviders.Add(faceProvider);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"初始化人脸识别失败：index:{i} ");
                    }
                }
                _isInited = true;
            });
        }

        /// <summary>
        /// 获取下一人脸SDK
        /// </summary>
        /// <returns></returns>
        private IFaceProvider GetAndNextCurrent()
        {
            if (_currentFaceProviderIndex >= _faceProviders.Count)
            {
                _currentFaceProviderIndex = 0;
            }
            var result = _faceProviders[_currentFaceProviderIndex];

            _currentFaceProviderIndex++;
            if (_currentFaceProviderIndex >= _faceProviders.Count)
            {
                _currentFaceProviderIndex = 0;
            }

            return result;
        }

        /// <summary>
        /// 人脸匹配
        /// </summary>
        /// <param name="feature">人脸特性</param>
        public async Task<FaceDistinguishResult> CompareFeatureAsync(FaceFeature feature, bool isMask)
        {
            var timeStart = DateTimeUtil.UtcNowMillis();

            var tasks = new List<Task>();
            var results = new List<FaceDistinguishResult>();
            foreach (var item in _faceProviders)
            {
                var task = Task.Run(async () =>
                {
                    var result = await item.CompareFeatureAsync(feature, isMask);
                    if (result != null && result.FirstOrDefault() != null)
                    {
                        results.AddRange(result);
                    }
                });
                tasks.Add(task);
            }
            await Task.WhenAll(tasks.ToArray());

            var timeSpan = (DateTimeUtil.UtcNowMillis() - timeStart) / 1000M;
            _logger.LogInformation($"检测人脸总结束：timeSpan:{timeSpan } mask:{isMask} ");

            if (results?.FirstOrDefault() == null)
            {
                _logger.LogInformation($"检测人脸总结束：results:null ");
                return null;
            }

            _logger.LogInformation($"检测人脸总结束：results:{JsonConvert.SerializeObject(results, JsonUtil.JsonPrintSettings)} ");

            var result = results.OrderByDescending(x => x.Similarity).FirstOrDefault();

            _logger.LogInformation($"检测人脸总结束：result:{JsonConvert.SerializeObject(result, JsonUtil.JsonPrintSettings)} ");

            return result;
        }

        /// <summary>
        /// 加载本地人脸
        /// </summary>
        public async Task LoadFaceFile()
        {
            await Task.Run(() =>
            {
                //获取本地文件
                var portraits = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "portraits-1w");
                if (!Directory.Exists(portraits))
                {
                    return;
                }
                var fileNames = Directory.GetFiles(portraits);
                if (fileNames == null || fileNames.FirstOrDefault() == null)
                {
                    return;
                }

                //人脸检测以及提取人脸特征 
                for (int i = 0; i < fileNames.Length; i++)
                {
                    GetAndNextCurrent().AddAsync(i, fileNames[i]);
                }
            });
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            if (_faceProviders?.FirstOrDefault() == null)
            {
                return;
            }
            foreach (var item in _faceProviders)
            {
                item.Close();
            }
        }
    }
}
