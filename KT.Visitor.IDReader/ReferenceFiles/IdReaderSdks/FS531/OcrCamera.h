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
 * �� JSON �ַ����еĵ��ڵ�ֵ
 * @param key
 * @param _json
 * @return
 */
OcrCamera_DLL const char *JSON_GetValue(const char *key, const char *_json);
/**
 * �� JSON �ַ�����ӽڵ��Լ�ֵ
 * @param key
 * @param value
 * @param _json
 * @return
 */
OcrCamera_DLL const char *JSON_SetValue(const char *key, const char *value, const char *_json);

/**
 * ��ȡ����ͷ�б�
 * @param list Ԥ�������ݣ�JSON �ַ�����ʽ
 * @param Len Ԥ�������ݴ�С
 * @return
 */
OcrCamera_DLL int OcrCamera_GetList(char *list, int Len);
OcrCamera_DLL int OcrCamera_GetListStruct(CameraInfo *list, int Len);
/**
 * ������ͷ
 * @param index ����ͷ index
 * @return
 */
OcrCamera_DLL int OcrCamera_Open(int index);
/**
 * �ر�����ͷ
 * @return
 */
OcrCamera_DLL int OcrCamera_Close();
/**
 * ����ͷ�Ƿ���
 * @return
 */
OcrCamera_DLL int OcrCamera_IsOpen();
/**
 * �õ�������
 * @param data ������
 * @param Len
 * @param Type ����������
 * @param isMirror �Ƿ���
 * @return
 */
OcrCamera_DLL int OcrCamera_TakePhoto(unsigned char data[], int Len, const char *Type = ".jpg",bool isMirror = false);
/**
 * ���յ�����
 * @param savePath
 * @param isMirror �Ƿ���
 * @return
 */
OcrCamera_DLL int OcrCamera_TakePhotoDisk(const char *savePath = "save.jpg",bool isMirror = false);

/**
 * OCR ʶ��
 * @param Type ֧�����֤ ���� ����
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
