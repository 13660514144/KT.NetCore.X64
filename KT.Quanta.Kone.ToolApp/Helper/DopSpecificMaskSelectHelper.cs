using KT.Quanta.Kone.ToolApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Kone.ToolApp.Helper
{
    public class DopSpecificMaskSelectHelper
    {
        public static List<DopSpecificMaskSelectModel> DopSpecificMaskSelectList { get; set; } = new List<DopSpecificMaskSelectModel>() {
           new DopSpecificMaskSelectModel()
           {
               Name = "Step3-Group1-Dop2-Connect-Mask",
               MaskFloors = new List<DopSpecificDefaultAccessFloorMaskViewModel>()
               {
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 1
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 2
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 3
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 4
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 5
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 6
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 7
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 8
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 9
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 10
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 11
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 12
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 13
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 14
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 15
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 16
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 17
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 18
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 19
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 20
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 21
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 22
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 23
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 24
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 25
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 26
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 27
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 28
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 29
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 30
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 31
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 32
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 33
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 34
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 35
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 36
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 37
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 38
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 39
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 40
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 41
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 42
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 43
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 44
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 45
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 46
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 47
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 48
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 49
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 50
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 51
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 52
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 53
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 54
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 55
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 56
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 57
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 58
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 59
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 60
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 61
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 62
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 63
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
               }
           },

           new DopSpecificMaskSelectModel()
           {
               Name = "Step3-Group1-Dop2-Disconnect-Mask",
               MaskFloors = new List<DopSpecificDefaultAccessFloorMaskViewModel>()
               {
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 1
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 2
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 3
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 4
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 5
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 6
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 7
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 8
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 9
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 10
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 11
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 12
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 13
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 14
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 15
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 16
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 17
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 18
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 19
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 20
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 21
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 22
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 23
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 24
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 25
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 26
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 27
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 28
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 29
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 30
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 31
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 32
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 33
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 34
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 35
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 36
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 37
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 38
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 39
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 40
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 41
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 42
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 43
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 44
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 45
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 46
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 47
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 48
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 49
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 50
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 51
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 52
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 53
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 54
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 55
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 56
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 57
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 58
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 59
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 60
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 61
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 62
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 63
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
               }
           },

           new DopSpecificMaskSelectModel()
           {
               Name = "Step32-Group1-Dop2-Connect-Mask",
               MaskFloors = new List<DopSpecificDefaultAccessFloorMaskViewModel>()
               {
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 1
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 2
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 3
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 4
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 5
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 6
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 7
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 8
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 9
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 10
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 11
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 12
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 13
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 14
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 15
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 16
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 17
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 18
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 19
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 20
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 21
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 22
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 23
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 24
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 25
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 26
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 27
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 28
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 29
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 30
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 31
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 32
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 33
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 34
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 35
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 36
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 37
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 38
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 39
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 40
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 41
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 42
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 43
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 44
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 45
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 46
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 47
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 48
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 49
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 50
                        },
                        IsDestinationFront = true ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 51
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 52
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 53
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 54
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 55
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 56
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 57
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 58
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 59
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 60
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 61
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 62
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 63
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = true
                    },
               }
           },

           new DopSpecificMaskSelectModel()
           {
               Name = "Step34-Group1-Dop2-Disconnect-Mask",
               MaskFloors = new List<DopSpecificDefaultAccessFloorMaskViewModel>()
               {
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 1
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 2
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 3
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 4
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 5
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 6
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 7
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 8
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 9
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 10
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 11
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 12
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 13
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 14
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 15
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 16
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 17
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 18
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 19
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 20
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 21
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 22
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 23
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 24
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 25
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 26
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 27
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 28
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 29
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 30
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 31
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 32
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 33
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 34
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 35
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 36
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 37
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 38
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 39
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 40
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 41
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 42
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 43
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 44
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 45
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 46
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 47
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 48
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 49
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 50
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 51
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 52
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 53
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 54
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 55
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 56
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 57
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 58
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 59
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 60
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 61
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 62
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
                    new DopSpecificDefaultAccessFloorMaskViewModel()
                    {
                        Floor = new FloorViewModel()
                        {
                            PhysicsFloor = 63
                        },
                        IsDestinationFront = false ,
                        IsDestinationRear = false
                    },
               }
           },


        };











    }
}
