using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Elevator.Manage.Service.Helpers;
using KT.Elevator.Manage.Service.IDaos;
using KT.Elevator.Manage.Service.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Services
{
    public class FaceInfoService : IFaceInfoService
    {
        private IFaceInfoDao _dao;
        private ILogger<FaceInfoService> _logger;

        public FaceInfoService(IFaceInfoDao serialConfigDataDao,
            ILogger<FaceInfoService> logger)
        {
            _dao = serialConfigDataDao;
            _logger = logger; 
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _dao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _dao.SelectByIdAsync(id);
            if (entity == null)
            {
                return;
            }

            //先删除数据
            await _dao.DeleteAsync(entity);

            //删除原文件
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, entity.SourceUrl);
            if (System.IO.File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            //删除人脸文件
            if (!string.IsNullOrEmpty(entity.FaceUrl))
            {
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, entity.FaceUrl);
                if (System.IO.File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            //数据库删除数据
            await _dao.DeleteByIdAsync(id);
        }

        public async Task<FaceInfoModel> AddOrEditAsync(FaceInfoModel model)
        {
            var entity = await _dao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = FaceInfoModel.ToEntity(model);

                await _dao.InsertAsync(entity);

                model.Id = entity.Id;
            }
            else
            {
                entity = FaceInfoModel.SetEntity(entity, model);

                await _dao.AttachUpdateAsync(entity);
            }
            model = FaceInfoModel.SetModel(model, entity);

            return model;
        }

        public async Task<List<FaceInfoModel>> GetAllAsync()
        {
            var entities = await _dao.SelectAllAsync();

            var models = FaceInfoModel.ToModels(entities);

            return models;
        }

        public async Task<FaceInfoModel> GetByIdAsync(string id)
        {
            var entity = await _dao.SelectByIdAsync(id);

            var model = FaceInfoModel.ToModel(entity);

            return model;
        }

        public async Task<(bool IsExists, FaceInfoModel FaceInfo)> AddOrEditAsync(FaceRequestModel faceRequest)
        {
            if (!string.IsNullOrEmpty(faceRequest?.Id))
            {
                _logger.LogInformation($"删除旧人脸：id:{faceRequest.Id} ");
                //先删除掉旧数据，因为有文件，不作事务
                await DeleteAsync(faceRequest.Id);
            }
            else
            {
                faceRequest.Id = IdUtil.NewId();
            }

            //文件扩展名
            string fileExt = Path.GetExtension(faceRequest.File.FileName);
            //获得文件大小，以字节为单位
            long fileSize = faceRequest.File.Length;
            //新文件名
            string newFileName = faceRequest.Id + fileExt;

            //要随机的字母
            var letters = "abcdefghijklmnopqrstuvwxyz";
            //随机类
            Random random = new Random();
            //通过索引下标随机 
            var letter1 = letters[random.Next(25)].ToString();
            var letter2 = letters[random.Next(25)].ToString();

            //创建文件夹
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "uploads", letter1, letter2);
            Directory.CreateDirectory(path);

            //保存文件
            var filePath = Path.Combine(path, newFileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                await faceRequest.File.CopyToAsync(stream);
            }

            //保存数据库数据
            var result = new FaceInfoModel();
            result.Name = faceRequest.File.FileName;
            result.Id = faceRequest.Id;
            result.Type = faceRequest.Type;
            result.SourceUrl = Path.Combine("uploads", letter1, letter2, newFileName);
            result.Extension = Path.GetExtension(faceRequest.File.FileName).TrimStart('.').ToUpper();

            ////生成特征
            //var faceFeature = ArcFaceHelper.GetFaceFeatureBytes(filePath);
            //if (!faceFeature.IsSuccess)
            //{
            //    _logger.LogInformation(faceFeature.Message);

            //    //删除文件
            //    File.Delete(filePath);

            //    throw CustomException.Run(faceFeature.Message);
            //}

            //result.Feature = faceFeature.Bytes;
            //result.FeatureSize = faceFeature.Bytes?.Length ?? 0;

            //_logger.LogInformation($"人脸特征：bytes:{faceFeature.Bytes?.ToSeparateCommaStr()} ");

            ////较验人脸是否存在
            //var checkResult = _faceHelper.IsExistsPersonFace(faceRequest.Id, filePath);
            //if (checkResult.IsExists)
            //{
            //    File.Delete(filePath);
            //    _logger.LogInformation("已存在相同的人脸！");

            //    //已经存在相同的人脸，则返回旧人脸数据
            //    var existsFaceInfo = await _dao.SelectByIdAsync(checkResult.Value.Id);
            //    if (existsFaceInfo == null)
            //    {
            //        throw CustomException.Run($"存在相同的人脸但系统中找不到相应的数据：id:{checkResult.Value.Id} ");
            //    }

            //    var model = FaceInfoModel.ToModel(existsFaceInfo);

            //    return (true, model);
            //}

            //执久化数据
            await AddOrEditAsync(result);

            ////向队列中添加头像
            //_faceHelper.AddOrEditPassRight(result);

            return (false, result);
        }
         
        public async Task<Unit.Entity.Models.UnitFaceModel> GetBytesById(string id)
        {
            var face = await _dao.SelectByIdAsync(id);
            if (face == null)
            {
                return null;
            }

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, face.SourceUrl);
            if (!File.Exists(filePath))
            {
                return null;
            }

            var result = new Unit.Entity.Models.UnitFaceModel();
            result.FaceUrl = face.SourceUrl;
            using (var fs = File.OpenRead(filePath))
            {
                result.FaceSize = (int)fs.Length; //获得文件长度 
                result.FaceBytes = new byte[result.FaceSize]; //建立一个字节数组 
                fs.Read(result.FaceBytes, 0, result.FaceSize); //按字节流读取  
            }

            return result;
        }
    }
}
