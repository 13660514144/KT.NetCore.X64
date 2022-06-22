using KT.Common.Core.Enums;
using KT.Proxy.BackendApi.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace KT.Visitor.Common.ViewModels
{
    public class BlacklistViewModel : BindableBase
    {
        private long _id;
        private string _name;
        private string _idNumber;
        private string _operator;
        private string _phone;
        private string _reason;
        private string _snapshotImg;
        private string _company;
        private string _createTime;
        private string _faceImg;
        private string _gender;
        private string _genderText;
        private List<CompanyModel> _blockAreas;

        /// <summary>
        /// Id主键
        /// </summary>
        public long Id
        {
            get => _id;
            set
            {
                SetProperty(ref _id, value);
            }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
            }
        }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdNumber
        {
            get => _idNumber;
            set
            {
                SetProperty(ref _idNumber, value);
            }
        }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator
        {
            get => _operator;
            set
            {
                SetProperty(ref _operator, value);
            }
        }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone
        {
            get => _phone;
            set
            {
                SetProperty(ref _phone, value);
            }
        }
        /// <summary>
        /// 拉黑原因
        /// </summary>
        public string Reason
        {
            get => _reason;
            set
            {
                SetProperty(ref _reason, value);
            }
        }
        /// <summary>
        /// 抓拍照
        /// </summary>
        public string SnapshotImg
        {
            get => _snapshotImg;
            set
            {
                SetProperty(ref _snapshotImg, value);
            }
        }
        /// <summary>
        /// 公司
        /// </summary>
        public string Company
        {
            get => _company;
            set
            {
                SetProperty(ref _company, value);
            }
        }
        /// <summary>
        /// 拉黑时间
        /// </summary>
        public string CreateTime
        {
            get => _createTime;
            set
            {
                SetProperty(ref _createTime, value);
            }
        }
        /// <summary>
        /// 图片
        /// </summary>
        public string FaceImg
        {
            get => _faceImg;
            set
            {
                SetProperty(ref _faceImg, value);
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                var g = GenderEnum.GetByValue(value);
                GenderText = g != null ? g.Text : value;
            }
        }
        /// <summary>
        /// 性别显示文本
        /// </summary>
        public string GenderText
        {
            get => _genderText;
            set
            {
                SetProperty(ref _genderText, value);
            }
        }

        /// <summary>
        /// 禁访区域
        /// </summary>
        public List<CompanyModel> BlockAreas
        {
            get => _blockAreas;
            set
            {
                SetProperty(ref _blockAreas, value);
            }
        }

        public void SetValue(BlacklistModel model)
        {
            Id = model.Id;
            Name = model.Name;
            IdNumber = model.IdNumber;
            Operator = model.Operator;
            Phone = model.Phone;
            Reason = model.Reason;
            SnapshotImg = model.SnapshotImg;
            Company = model.Company;
            CreateTime = model.CreateTime;
            FaceImg = model.FaceImg;
            Gender = model.Gender;
            GenderText = model.GenderText;
            BlockAreas = model.BlockAreas;
        }

        public BlacklistModel GetValue()
        {
            var model = new BlacklistModel();
            model.Id = Id;
            model.Name = Name;
            model.IdNumber = IdNumber;
            model.Operator = Operator;
            model.Phone = Phone;
            model.Reason = Reason;
            model.SnapshotImg = SnapshotImg;
            model.Company = Company;
            model.CreateTime = CreateTime;
            model.FaceImg = FaceImg;
            model.Gender = Gender;
            model.GenderText = GenderText;
            model.BlockAreas = BlockAreas;
            return model;
        }
    }
}
