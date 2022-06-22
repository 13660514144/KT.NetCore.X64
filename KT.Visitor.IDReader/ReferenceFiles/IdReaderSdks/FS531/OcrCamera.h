//
// Created by k on 2019/10/22.
//

#ifndef OCRCAMERA_OCRCAMERA_H
#define OCRCAMERA_OCRCAMERA_H

#ifdef OcrCamera_EXPORTS
#define OcrCamera_DLL __declspec(dllexport)
#else
#define OcrCamera_DLL __declspec(dllimport)
#endif

#define OCR_TYPE_ID             1
#define OCR_TYPE_JZ             2
#define OCR_TYPE_Passport      3

#define OcrCamera_Error_InitFail            -99
#define OcrCamera_Error_DataLenSmall       -98
#define OcrCamera_Error_CameraNotOpen      -97
#define OcrCamera_Error_Parameter           -96

struct CameraInfo{
    char Name[100] = {};
    int Index=0;
    char VID[20] = {};
    char PID[20] = {};
};
#ifdef __cplusplus
extern "C"
{
#endif

OcrCamera_DLL void OcrCamera_SetLog(int Level = 0);

OcrCamera_DLL void OcrCamera_InitPath(char* path);


/**
 * 从 JSON 字符串中的到节点值
 * @param key
 * @param _json
 * @return
 */
OcrCamera_DLL const char *JSON_GetValue(const char *key, const char *_json);
/**
 * 给 JSON 字符串添加节点以及值
 * @param key
 * @param value
 * @param _json
 * @return
 */
OcrCamera_DLL const char *JSON_SetValue(const char *key, const char *value, const char *_json);

/**
 * 获取摄像头列表
 * @param list 预分配数据，JSON 字符串格式
 * @param Len 预分配数据大小
 * @return
 */
OcrCamera_DLL int OcrCamera_GetList(char *list, int Len);
OcrCamera_DLL int OcrCamera_GetListStruct(CameraInfo *list, int Len);
/**
 * 打开摄像头
 * @param index 摄像头 index
 * @return
 */
OcrCamera_DLL int OcrCamera_Open(int index);
/**
 * 关闭摄像头
 * @return
 */
OcrCamera_DLL int OcrCamera_Close();
/**
 * 摄像头是否开启
 * @return
 */
OcrCamera_DLL int OcrCamera_IsOpen();
/**
 * 得到数据流
 * @param data 数据流
 * @param Len
 * @param Type 数据流类型
 * @param isMirror 是否镜像
 * @return
 */
OcrCamera_DLL int OcrCamera_TakePhoto(unsigned char data[], int Len, const char *Type = ".jpg",bool isMirror = false);
/**
 * 拍照到本地
 * @param savePath
 * @param isMirror 是否镜像
 * @return
 */
OcrCamera_DLL int OcrCamera_TakePhotoDisk(const char *savePath = "save.jpg",bool isMirror = false);

/**
 * OCR 识别
 * @param Type 支持身份证 驾照 护照
 * @param PhotoPath
 * @param outHeaderPath
 * @param outData
 * @param outDataLen
 * @return
 */
OcrCamera_DLL int
OcrCamera_OCR(int Type, const char *PhotoPath, const char *outHeaderPath, char *outData, int outDataLen);

#ifdef __cplusplus
}
#endif

#endif //OCRCAMERA_OCRCAMERA_H
