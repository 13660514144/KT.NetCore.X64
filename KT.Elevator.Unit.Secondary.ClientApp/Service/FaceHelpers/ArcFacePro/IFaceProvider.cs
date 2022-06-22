using KT.Elevator.Unit.Entity.Entities;
using KT.Unit.Face.Arc.Pro.Entity;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.FaceHelpers.ArcFacePro
{
    public interface IFaceProvider
    {
        FaceEngineProvider FaceEngineProvider { get; set; }
        int ProviderIndex { get; set; }

        Task AddAsync(int index, string fullFileName);
        Task AddPassRightAsync(UnitPassRightEntity faceRight);
        Task AddPassRightsAsync(List<UnitPassRightEntity> faceRights);
        void Close();
        Task<List<FaceDistinguishResult>> CompareFeatureAsync(FaceFeature feature, bool isMask);
        Image CropImage(string file);
        Image CropImage(Image image);
        void DeletePassRight(string id);
        List<FacePassRightModel> FaceEntitiesToModels(List<UnitPassRightEntity> passRightEntities);
        FacePassRightModel FaceEntityToModel(UnitPassRightEntity passRightEntity);
        FaceFullFeature GetFaceFeature(string fileUrl);
        FaceFullFeature GetFaceFeatureBytes(string fileUrl);
        FaceFullFeature GetFaceFeatureBytes(Image image);
    }
}
