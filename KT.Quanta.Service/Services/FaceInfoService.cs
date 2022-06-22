using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Elevator.Unit.Entity.Models;
using KT.Quanta.Service.Helpers;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
{
    public class FaceInfoService : IFaceInfoService
    {
        private readonly IFaceInfoDao _dao;
        private readonly ILogger<FaceInfoService> _logger;

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

                await _dao.AttachAsync(entity);
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

        public async Task<FaceInfoModel> AddOrEditAsync(FaceRequestModel faceRequest)
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
            string sourceFileExtension = Path.GetExtension(faceRequest.File.FileName);
            //获得文件大小，以字节为单位
            long sourceFileSize = faceRequest.File.Length;
            //新文件名
            string sourceFileName = faceRequest.Id + sourceFileExtension;

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
            var sourceFileFullName = Path.Combine(path, sourceFileName);
            using (var stream = System.IO.File.Create(sourceFileFullName))
            {
                await faceRequest.File.CopyToAsync(stream);
            }

            var faceFileName = IdUtil.NewId() + ".jpg";
            var faceFileFullName = Path.Combine(path, faceFileName);
            if (faceRequest.File.Length > 200 * 1024)
            {
                Image newImage;
                using (MemoryStream ms = new MemoryStream())
                {
                    await faceRequest.File.CopyToAsync(ms);
                    newImage = Image.FromStream(ms);
                }

                newImage = ImageUtil.CompressImageQualityLenth(newImage, 200 * 1024);
                ImageUtil.SaveToFile(newImage, faceFileFullName, true, ImageFormat.Jpeg);

                newImage.Dispose();
            }
            else
            {
                using (var stream = System.IO.File.Create(faceFileFullName))
                {
                    await faceRequest.File.CopyToAsync(stream);
                }
            }


            ////压缩
            //var fileInfo = new FileInfo(faceFileFullName);
            //if (fileInfo.Length > 200 * 1024)
            //{
            //    var newImage = ImageUtil.ReadFromFile(faceFileFullName);
            //    newImage = ImageUtil.CompressImageQualityLenth(newImage, 200 * 1024);
            //    ImageUtil.SaveToFile(newImage, faceFileFullName, true, ImageFormat.Jpeg);
            //    newImage.Dispose();
            //}

            //保存数据库数据
            var result = new FaceInfoModel();
            result.Name = faceRequest.File.FileName;
            result.Id = faceRequest.Id;
            result.Type = faceRequest.Type;
            result.SourceUrl = Path.Combine("uploads", letter1, letter2, sourceFileName);
            result.FaceUrl = Path.Combine("uploads", letter1, letter2, faceFileName);
            result.Extension = Path.GetExtension(faceRequest.File.FileName).TrimStart('.').ToUpper();

            //执久化数据
            await AddOrEditAsync(result);

            ////向队列中添加头像
            //_faceHelper.AddOrEditPassRight(result);

            return result;
        }

        public async Task<UnitFaceModel> GetBytesById(string id)
        {
            var face = await _dao.SelectByIdAsync(id);
            if (face == null)
            {
                return null;
            }

            //旧数据兼容
            if (string.IsNullOrEmpty(face.FaceUrl))
            {
                face.FaceUrl = face.SourceUrl;
            }
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, face.FaceUrl);
            if (!File.Exists(filePath))
            {
                return null;
            }

            var result = new UnitFaceModel();
            result.FaceUrl = face.FaceUrl;
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
