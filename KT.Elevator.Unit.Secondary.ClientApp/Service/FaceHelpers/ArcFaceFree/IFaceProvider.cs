using KT.Elevator.Unit.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.FaceHelpers.ArcFaceFree
{
    public interface IFaceProvider
    {
        int ProviderIndex { get; set; }
        FaceEngine FaceEngine { get; set; }

        Task AddPassRightsAsync(List<UnitPassRightEntity> faceRights);
        Task<List<FaceDistinguishResult>> CompareFeatureAsync(IntPtr feature);
        Task AddAsync(int index, string fullFileName);
        void Close();
        (bool IsSuccess, byte[] Bytes, string Message) GetFaceFeatureBytes(string fileUrl);
        (bool IsSuccess, byte[] Bytes, string Message) GetFaceFeatureBytes(Image image);
        (bool IsSuccess, IntPtr Feature, string Message, byte[] Bytes) GetFaceFeature(string fileUrl);
        IntPtr GetFaceFeature(byte[] bytes);
        List<FacePassRightModel> FaceEntitiesToModels(List<UnitPassRightEntity> passRightEntities);
        void DeletePassRight(string id);
        Task AddPassRightAsync(UnitPassRightEntity faceRight);
    }
}
