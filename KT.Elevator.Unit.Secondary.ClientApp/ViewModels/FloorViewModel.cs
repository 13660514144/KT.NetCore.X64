using KT.Common.Core.Utils;
using KT.Common.WpfApp.ViewModels;
using KT.Elevator.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace KT.Elevator.Unit.Secondary.ClientApp.ViewModels
{
    public class FloorViewModel : BindableBase
    {
        private string _id;
        private string _name;
        private string _realFloorId;
        private bool _isPublic;
        private string _edificeId;
        private string _edificeName;
        private string _elevatorGroupId;
        private bool _hasRight;
        private UnitPassRightEntity _passRight;

        private long _sort;

        /// <summary>
        /// Id主键
        /// </summary>
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        /// <summary>
        /// 梯控楼层Id
        /// </summary>
        public string RealFloorId
        {
            get
            {
                return _realFloorId;
            }
            set
            {
                SetProperty(ref _realFloorId, value);

                Sort = ConvertUtil.ToLong(value, 9999);
            }
        }

        /// <summary>
        /// 是否为公共楼层
        /// </summary>
        public bool IsPublic
        {
            get
            {
                return _isPublic;
            }
            set
            {
                SetProperty(ref _isPublic, value);
            }
        }

        /// <summary>
        /// 大厦Id
        /// </summary>
        public string EdificeId
        {
            get
            {
                return _edificeId;
            }
            set
            {
                SetProperty(ref _edificeId, value);
            }
        }

        /// <summary>
        /// 大厦名称
        /// </summary>
        public string EdificeName
        {
            get
            {
                return _edificeName;
            }
            set
            {
                SetProperty(ref _edificeName, value);
            }
        }

        /// <summary>
        /// 关联电梯组
        /// </summary>
        public string ElevatorGroupId
        {
            get
            {
                return _elevatorGroupId;
            }
            set
            {
                SetProperty(ref _elevatorGroupId, value);
            }
        }

        /// <summary>
        /// 是否有权限
        /// </summary>
        public bool HasRight
        {
            get
            {
                return _hasRight;
            }

            set
            {
                SetProperty(ref _hasRight, value);
            }
        }

        public UnitPassRightEntity PassRight
        {
            get
            {
                return _passRight;
            }

            set
            {
                SetProperty(ref _passRight, value);
            }
        }

        public long Sort
        {
            get
            {
                return _sort;
            }

            set
            {
                SetProperty(ref _sort, value);
            }
        }
        /// <summary>
        /// 日立电梯需要用到
        /// </summary>
        public string CommBox { get; set; } = string.Empty;
        public string DestinationFloorId { get; set; } = string.Empty;
        public string Sourceid { get; set; } = string.Empty;
        public string HandDeviceid {get;set;} = string.Empty;
        public string AccessType { get; set; } = string.Empty;
        public string DeviceType { get; set; } = string.Empty;
        public string Sign { get; set; } = string.Empty;
        /// <summary>
        /// 通行权限
        /// </summary>
        /// <param name="floorEntities"></param>
        /// <param name="passRights"></param>
        /// <returns>
        /// AllFloors：所有楼层 
        /// PassableFloors：可通行楼层
        /// RightFloors：有权限楼层
        /// </returns>
        internal static (ObservableCollection<FloorViewModel> AllFloors, List<FloorViewModel> PassableFloors, List<FloorViewModel> RightFloors) ToModels(List<UnitFloorEntity> floorEntities, List<UnitPassRightEntity> passRights)
        {
            //记录当前用户是否有权限 
            var passableFloors = new List<FloorViewModel>();
            var rightFloors = new List<FloorViewModel>();

            var viewModels = new ObservableCollection<FloorViewModel>();
            if (floorEntities == null || floorEntities.FirstOrDefault() == null)
            {
                return (viewModels, passableFloors, rightFloors);
            }

            foreach (var item in floorEntities)
            {
                var viewModel = ToModel(item);
                var passRight = passRights?.FirstOrDefault(x => x.PassRightDetails?.FirstOrDefault(y => y.FloorId == item.Id) != null);

                viewModel.HasRight = passRight != null;
                viewModel.PassRight = passRight;
                viewModels.Add(viewModel);

                //记录有权限楼层(公共楼层不派梯）
                if (passRight != null)
                {
                    rightFloors.Add(viewModel);
                }

                if (passRight != null || viewModel.IsPublic)
                {
                    passableFloors.Add(viewModel);
                }
            }

            return (viewModels, passableFloors, rightFloors);
        }

        /// <summary>
        /// 通行权限
        /// </summary>
        /// <param name="floorEntities"></param>
        /// <param name="passRights"></param>
        /// <returns>
        /// AllFloors：所有楼层 
        /// PassableFloors：可通行楼层
        /// RightFloors：有权限楼层
        /// </returns>
        internal static (ObservableCollection<FloorViewModel> AllFloors, List<FloorViewModel> PassableFloors, List<FloorViewModel> RightFloors) ToModels(List<UnitFloorEntity> floorEntities, UnitPassRightEntity passRight)
        {
            //记录当前用户是否有权限 
            var passableFloors = new List<FloorViewModel>();
            var rightFloors = new List<FloorViewModel>();

            var viewModels = new ObservableCollection<FloorViewModel>();
            if (floorEntities == null || floorEntities.FirstOrDefault() == null)
            {
                return (viewModels, passableFloors, rightFloors);
            }

            foreach (var item in floorEntities)
            {
                var viewModel = ToModel(item);

                viewModel.HasRight = passRight?.PassRightDetails?.Any(y => y.FloorId == item.Id) == true;
                if (viewModel.HasRight)
                {
                    viewModel.PassRight = passRight;
                }

                viewModels.Add(viewModel);

                //记录有权限楼层(公共楼层不派梯）
                if (passRight != null)
                {
                    rightFloors.Add(viewModel);
                }

                if (passRight != null || viewModel.IsPublic)
                {
                    passableFloors.Add(viewModel);
                }
            }

            return (viewModels, passableFloors, rightFloors);
        }


        /// <summary>
        /// 通行权限
        /// </summary>
        /// <param name="floorEntities"></param>
        /// <param name="passRights"></param>
        /// <returns>
        /// AllFloors：所有楼层 
        /// PassableFloors：可通行楼层
        /// RightFloors：有权限楼层
        /// </returns>
        internal static (ObservableCollection<FloorViewModel> AllFloors, List<FloorViewModel> PassableFloors, List<FloorViewModel> RightFloors) ToModels(List<UnitFloorEntity> floorEntities)
        {
            //记录当前用户是否有权限 
            var passableFloors = new List<FloorViewModel>();
            var rightFloors = new List<FloorViewModel>();

            var viewModels = new ObservableCollection<FloorViewModel>();
            if (floorEntities == null || floorEntities.FirstOrDefault() == null)
            {
                return (viewModels, passableFloors, rightFloors);
            }

            foreach (var item in floorEntities)
            {
                var viewModel = ToModel(item);

                viewModels.Add(viewModel);

                if (viewModel.IsPublic)
                {
                    passableFloors.Add(viewModel);
                }
            }

            return (viewModels, passableFloors, rightFloors);
        }

        public static FloorViewModel ToModel(UnitFloorEntity floorEntity)
        {
            var viewModel = new FloorViewModel();

            viewModel.Id = floorEntity.Id;
            viewModel.Name = floorEntity.Name;
            viewModel.RealFloorId = floorEntity.RealFloorId;
            viewModel.IsPublic = floorEntity.IsPublic;
            viewModel.EdificeId = floorEntity.EdificeId;
            viewModel.EdificeName = floorEntity.EdificeName;
            viewModel.ElevatorGroupId = floorEntity.ElevatorGroupId;

            viewModel.CommBox = floorEntity.CommBox;
            viewModel.DestinationFloorId = floorEntity.DestinationFloorId;
            viewModel.Sourceid = floorEntity.Sourceid;
            viewModel.HandDeviceid = floorEntity.HandDeviceid;
            viewModel.AccessType = floorEntity.AccessType;
            viewModel.DeviceType = floorEntity.DeviceType;
            viewModel.Sign = floorEntity.Sign;

            return viewModel;
        }
    }
}
