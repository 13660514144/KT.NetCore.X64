#pragma once
#include <windows.h>

//#include"ocr.h"
//#pragma comment (lib,"ImportDll.lib")
#define   H531       0xA1
#define   Passport   0xA2


#define SCAN_R_MODE				1
#define SCAN_G_MODE				2	
#define SCAN_B_MODE				3
#define SCAN_GRAY_MODE			4
#define SCAN_IR_MODE			5
#define	SCAN_COLOR_MODE		    6

#define SCAN_200_DPI			200
#define SCAN_300_DPI			300
#define SCAN_400_DPI			400
#define SCAN_600_DPI			600

struct Scan_Para
{
	short scan_mode;
	// SCAN_R_MODE				1
	// SCAN_G_MODE				2
	// SCAN_B_MODE				3
	// SCAN_GRAY_MODE			4
	// SCAN_IR_MODE			5
	// SCAN_COLOR_MODE		6
	short sp_top;             	// unit pixel, min = 0 max = 600
	short sp_left;             	// unit pixel, min = 0 max = 512
	short sp_bottom;          	// unit pixel, min = 0 max = 600
	short sp_right;		   // unit pixel, min = 0 max = 512
	short resolution;       	// in dpi
								  // 推荐：SCAN_300_DPI			300
	short scan_size;        	// paper size; 0 : user defined ,1 - 8
	short brightness;       	// 0
	short contrast;
	short gamma;         	// 10
	short highlight;     //0   正常亮度
  //1   亮度增强
  //2   亮度更强
	short shadow;
	short Rbrightness;
	short Rcontrast;
	short Rgamma;
	short Rhighlight;
	short Rshadow;
	short Gbrightness;
	short Gcontrast;
	short Ggamma;
	short Ghighlight;
	short Gshadow;
	short Bbrightness;
	short Bcontrast;
	short Bgamma;
	short Bhighlight;
	short Bshadow;

	short halftone_pattern;
	short delay;            // 0 : false, 1 : true, ignore it
	short compress;         // 0 : no compress , 1 : compress
	short trimedge;         // 0 : no trimedge , 1: trimedge
	short deskew;           // 0 : no deskew , 1: deskew
	short scanpage;         // 0 : single page 1: multiple page
	short autoscan;         // 0 : use manual scan, 1 : use auto scan
	short ADF;              // 0 : no ADF    1: use ADF
	short qualityscan;      // 0 : speed scan 1:quality scan
	short BWthreshold;      // from 0 to 255
	short filter;           // 0: none; 1 : despeckle; 2:descreen; 3:sharpen
	short inversion;        // 0 : no inverse image 1: inverse image
	short colormatch;       // 0 : no color match   1 use color match
	short channel;          // 0 : all, 1: R, 2 G, 3 B
	short papertype;
	short exposure;
	short method;           // 0: auto mode 1 :manual mode
}sp;

 struct CARDRST
{
	char name[40];			//姓名
	char SurnameCH[100];	//中文姓
	char nameCH[100];		//中文名
	char sex[10];			//性别
	char Gender[10];		//性别（英文）
	char birthday[50];		//出生日期
	char people[20];			//民族
	char signdate[50];		//发证日期
	char validterm[50];		//有效期限
	char address[200];		//地址
	char number[40];		//身份证号码
	char organs[50];		//发证机关
	char SurnameEN[100];	//英文姓(汉语拼音)
	char nameEN[100];		//英文名(汉语拼音)
	char ENfullname[40];	//英文姓名	
	char Nationality[200];	//国籍
	char id[100];			//证件号码
	char Leavetime[50];		//离开时间
	char placeCH[100];		//签发地点中文
	char placeEN[100];		//签发地点英文
	char BirthplaceCH[100]; //出生地中文 
	char BirthplaceEN[100];	//出生地英文
	char szCodeOne[256];	//第一行代码识别结果
	char szCodeTwo[256];	//第二行代码识别结果
	char szCodeThree[256];	//第三行代码识别结果
	char Permitnumber_number[40]; //港澳证件号码
	char Vocational[200];	//职业
	char DocumentsCategory[100]; //证件类别
	char Other[200];
}IDCARD_ALL;


extern "C" __declspec(dllexport) int findScanner();

extern "C" __declspec(dllexport) int startScan(char chResolution, char chScanFace, char *chBackImagePath, char *chScanImagePath, Scan_Para& sP1, Scan_Para& sP2, int lCardType, int lContrlType, int lJPEG_QUALITY);
extern "C" __declspec(dllexport) int scan(char *chBackImagePath, char *chScanImagePath, int CardType, int lJPEG_QUALITY);

extern "C" __declspec(dllexport) int resetScanner();

extern "C" __declspec(dllexport) int discernImage(char* pchFilePath, char* HeadImgFName, CARDRST & rcgCard, int lCardType, int lSaveContrl, int lJPEG_QUALITY, char* ocrdata, char* cfgPath, char* outData, int outSize);
extern "C" __declspec(dllexport) int discernImage1(char* pchFilePath, char* HeadImgFName, int lCardType, int lJPEG_QUALITY, char* ocrdata, char* outData);

//extern "C" __declspec(dllexport) int scanAndDiscern(char* filePath);

extern "C" __declspec(dllexport) int caliScanner();

extern "C" __declspec(dllexport) int closeScanner();