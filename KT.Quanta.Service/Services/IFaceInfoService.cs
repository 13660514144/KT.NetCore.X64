using KT.Elevator.Unit.Entity.Models;
using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
{
    /// <summary>
    /// 人脸信息数据存储服务
    /// </summary>
    public interface IFaceInfoService
    {
        /// <summary>
        /// 检查对象是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsAsync(string id);

        /// <summary>
        /// 根据Id获取人脸信息信息
        /// </summary>
        /// <param name="id">人脸信息Id</param>
        /// <returns>人脸信息信息详情</returns>
        Task<FaceInfoModel> GetByIdAsync(string id);

        /// <summary>
        /// 查询所有人脸信息信息
        /// </summary>
        /// <returns>人脸信息信息列表</returns>
        Task<List<FaceInfoModel>> GetAllAsync();

        /// <summary>
        /// 修改人脸信息
        /// </summary>
        /// <param name="model">人脸信息详情</param>
        /// <returns>是否成功</returns>
        Task<FaceInfoModel> AddOrEditAsync(FaceInfoModel model);


        /// <summary>
        /// 新增或修改人脸
        /// </summary>
        /// <param name="faceRequest">人脸信息</param>
        /// <returns></returns>
        Task<FaceInfoModel> AddOrEditAsync(FaceRequestModel faceRequest);

        /// <summary>
        /// 物理删除人脸信息
        /// </summary>
        /// <param name="id">人脸信息Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);
 
        /// <summary>
        /// 获取人脸图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UnitFaceModel> GetBytesById(string id);
    }
}
