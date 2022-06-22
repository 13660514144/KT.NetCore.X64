using KT.Quanta.Kone.ToolApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Kone.ToolApp.Helper
{
    public class DopGlobalMaskSelectHelper
    {
        public static List<DopGlobalMaskSelectModel> DopGlobalMaskSelectList { get; set; } = new List<DopGlobalMaskSelectModel>() {
           new DopGlobalMaskSelectModel()
           {
               Name = "Step3-Group1-Connect-Mask",
               MaskFloors = new List<DopGlobalDefaultAccessFloorMaskViewModel>()
               {
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 1
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 2
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 3
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 4
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 5
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 6
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 7
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 8
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 9
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 10
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 11
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 12
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 13
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 14
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 15
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 16
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 17
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 18
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 19
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 20
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 21
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 22
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 23
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 24
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 25
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 26
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 27
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 28
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 29
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 30
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 31
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 32
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 33
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 34
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 35
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 36
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 37
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 38
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 39
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 40
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 41
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 42
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 43
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 44
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 45
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 46
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 47
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 48
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 49
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 50
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 51
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 52
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 53
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 54
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 55
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 56
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 57
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 58
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 59
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 60
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 61
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 62
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 63
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
               }
           },

           new DopGlobalMaskSelectModel()
           {
               Name = "Step3-Group1-Disconnect-Mask",
               MaskFloors = new List<DopGlobalDefaultAccessFloorMaskViewModel>()
               {
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 1
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 2
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 3
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 4
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 5
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 6
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 7
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 8
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 9
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 10
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 11
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 12
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 13
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 14
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 15
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 16
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 17
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 18
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 19
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 20
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 21
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 22
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 23
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 24
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 25
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 26
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 27
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 28
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 29
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 30
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 31
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 32
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 33
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 34
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 35
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 36
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 37
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 38
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 39
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 40
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 41
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 42
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 43
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 44
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 45
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 46
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 47
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 48
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 49
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 50
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 51
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 52
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 53
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 54
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 55
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 56
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 57
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 58
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 59
                        },
                        IsSourceFront = false ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 60
                        },
                        IsSourceFront = false ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 61
                        },
                        IsSourceFront = false ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 62
                        },
                        IsSourceFront = false ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 63
                        },
                        IsSourceFront = false ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
               }
           },

           new DopGlobalMaskSelectModel()
           {
               Name = "Step3-Group2-Connect-Mask",
               MaskFloors = new List<DopGlobalDefaultAccessFloorMaskViewModel>()
               {
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 1
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 2
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 3
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 4
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 5
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 6
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 7
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 8
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 9
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 10
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 11
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 12
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 13
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 14
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 15
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 16
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 17
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 18
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 19
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 20
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 21
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 22
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 23
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 24
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 25
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 26
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 27
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 28
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 29
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 30
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 31
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 32
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 33
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 34
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 35
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 36
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 37
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 38
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 39
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 40
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 41
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 42
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 43
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 44
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 45
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 46
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 47
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 48
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 49
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 50
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 51
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 52
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 53
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 54
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 55
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 56
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 57
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 58
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 59
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 60
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 61
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 62
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 63
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
               }
           },

           new DopGlobalMaskSelectModel()
           {
               Name = "Step3-Group2-Disconnect-Mask",
               MaskFloors = new List<DopGlobalDefaultAccessFloorMaskViewModel>()
               {
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 1
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 2
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 3
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 4
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 5
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 6
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 7
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 8
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 9
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 10
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 11
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 12
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 13
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 14
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 15
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 16
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 17
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 18
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 19
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 20
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 21
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 22
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 23
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 24
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 25
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 26
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 27
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 28
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 29
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 30
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 31
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 32
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 33
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 34
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 35
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 36
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 37
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 38
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 39
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 40
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 41
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 42
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 43
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 44
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 45
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 46
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 47
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 48
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 49
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 50
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 51
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 52
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 53
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 54
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 55
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 56
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 57
                        },
                        IsSourceFront = true ,
                        IsSourceRear = false ,
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 58
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 59
                        },
                        IsSourceFront = false ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 60
                        },
                        IsSourceFront = false ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 61
                        },
                        IsSourceFront = false ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 62
                        },
                        IsSourceFront = false ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 63
                        },
                        IsSourceFront = false ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
               }
           },

           new DopGlobalMaskSelectModel()
           {
               Name = "Step31-Group1-Connect-Mask",
               MaskFloors = new List<DopGlobalDefaultAccessFloorMaskViewModel>()
               {
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 1
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 2
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 3
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 4
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 5
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 6
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 7
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 8
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 9
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 10
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 11
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 12
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 13
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 14
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 15
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 16
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 17
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 18
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 19
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 20
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 21
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 22
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 23
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 24
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 25
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 26
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 27
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 28
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 29
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 30
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 31
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 32
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 33
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 34
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 35
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 36
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 37
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 38
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 39
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 40
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 41
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 42
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 43
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 44
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 45
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 46
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 47
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 48
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 49
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 50
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 51
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 52
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 53
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 54
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 55
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 56
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 57
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 58
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 59
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 60
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 61
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 62
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 63
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
               }
           },

           new DopGlobalMaskSelectModel()
           {
               Name = "Step31-Group2-Connect-Mask",
               MaskFloors = new List<DopGlobalDefaultAccessFloorMaskViewModel>()
               {
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 1
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 2
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 3
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 4
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 5
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 6
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 7
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 8
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 9
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 10
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 11
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 12
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 13
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 14
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 15
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 16
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 17
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 18
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 19
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 20
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 21
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 22
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 23
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 24
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 25
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 26
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 27
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 28
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 29
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 30
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 31
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 32
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 33
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 34
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 35
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 36
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 37
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 38
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 39
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 40
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 41
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 42
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 43
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 44
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 45
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 46
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 47
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 48
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 49
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 50
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 51
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 52
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 53
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 54
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 55
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 56
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 57
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 58
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 59
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 60
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 61
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 62
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 63
                        },
                        IsSourceFront = true ,
                        IsSourceRear = true ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
               }
           },

           new DopGlobalMaskSelectModel()
           {
               Name = "Step33-Group1-Disconnect-Mask",
               MaskFloors = new List<DopGlobalDefaultAccessFloorMaskViewModel>()
               {
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 1
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 2
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 3
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 4
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 5
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 6
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 7
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 8
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 9
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 10
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 11
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 12
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 13
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 14
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 15
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 16
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 17
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 18
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 19
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 20
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 21
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 22
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 23
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 24
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 25
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 26
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 27
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 28
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 29
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 30
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 31
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 32
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 33
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 34
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 35
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 36
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 37
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 38
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 39
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 40
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 41
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 42
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 43
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 44
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 45
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 46
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 47
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 48
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 49
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 50
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 51
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 52
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 53
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 54
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 55
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 56
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 57
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 58
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 59
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 60
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 61
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 62
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 63
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
               }
           },

           new DopGlobalMaskSelectModel()
           {
               Name = "Step33-Group2-Disconnect-Mask",
               MaskFloors = new List<DopGlobalDefaultAccessFloorMaskViewModel>()
               {
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 1
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 2
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 3
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 4
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 5
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 6
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 7
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 8
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 9
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 10
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 11
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 12
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 13
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 14
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 15
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 16
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 17
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 18
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 19
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 20
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 21
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 22
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 23
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 24
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 25
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 26
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 27
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 28
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 29
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 30
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 31
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 32
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 33
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 34
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 35
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 36
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 37
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 38
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 39
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 40
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 41
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 42
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 43
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 44
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 45
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 46
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 47
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 48
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 49
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 50
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 51
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 52
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 53
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 54
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 55
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 56
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 57
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 58
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 59
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 60
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 61
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 62
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopGlobalDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 63
                        },
                        IsSourceFront = false ,
                        IsSourceRear = false ,
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
               }
           },



        };











    }
}
