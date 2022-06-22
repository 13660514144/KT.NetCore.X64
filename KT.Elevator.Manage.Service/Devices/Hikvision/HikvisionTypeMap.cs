using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Hikvision
{
    public class HikvisionTypeMap
    {
        public static void AlarmMinorTypeMap(CHCNetSDK.NET_DVR_LOG_V30 stLogInfo, char[] csTmp)
        {
            string szTemp;
            switch (stLogInfo.dwMinorType)
            {
                //alarm
                case CHCNetSDK.MINOR_ALARMIN_SHORT_CIRCUIT:
                    szTemp = "MINOR_ALARMIN_SHORT_CIRCUIT";
                    break;
                case CHCNetSDK.MINOR_ALARMIN_BROKEN_CIRCUIT:
                    szTemp = "MINOR_ALARMIN_BROKEN_CIRCUIT";
                    break;
                case CHCNetSDK.MINOR_ALARMIN_EXCEPTION:
                    szTemp = "MINOR_ALARMIN_EXCEPTION";
                    break;
                case CHCNetSDK.MINOR_ALARMIN_RESUME:
                    szTemp = "MINOR_ALARMIN_RESUME";
                    break;
                case CHCNetSDK.MINOR_HOST_DESMANTLE_ALARM:
                    szTemp = "MINOR_HOST_DESMANTLE_ALARM";
                    break;
                case CHCNetSDK.MINOR_HOST_DESMANTLE_RESUME:
                    szTemp = "MINOR_HOST_DESMANTLE_RESUME";
                    break;
                case CHCNetSDK.MINOR_CARD_READER_DESMANTLE_ALARM:
                    szTemp = "MINOR_CARD_READER_DESMANTLE_ALARM";
                    break;
                case CHCNetSDK.MINOR_CARD_READER_DESMANTLE_RESUME:
                    szTemp = "MINOR_CARD_READER_DESMANTLE_RESUME";
                    break;
                case CHCNetSDK.MINOR_CASE_SENSOR_ALARM:
                    szTemp = "MINOR_CASE_SENSOR_ALARM";
                    break;
                case CHCNetSDK.MINOR_CASE_SENSOR_RESUME:
                    szTemp = "MINOR_CASE_SENSOR_RESUME";
                    break;
                case CHCNetSDK.MINOR_STRESS_ALARM:
                    szTemp = "MINOR_STRESS_ALARM";
                    break;
                case CHCNetSDK.MINOR_OFFLINE_ECENT_NEARLY_FULL:
                    szTemp = "MINOR_OFFLINE_ECENT_NEARLY_FULL";
                    break;
                case CHCNetSDK.MINOR_CARD_MAX_AUTHENTICATE_FAIL:
                    szTemp = "MINOR_CARD_MAX_AUTHENTICATE_FAIL";
                    break;
                case CHCNetSDK.MINOR_SD_CARD_FULL:
                    szTemp = "MINOR_SD_CARD_FULL";
                    break;
                case CHCNetSDK.MINOR_LINKAGE_CAPTURE_PIC:
                    szTemp = "MINOR_LINKAGE_CAPTURE_PIC";
                    break;
                case CHCNetSDK.MINOR_SECURITY_MODULE_DESMANTLE_ALARM:
                    szTemp = "MINOR_SECURITY_MODULE_DESMANTLE_ALARM";
                    break;
                case CHCNetSDK.MINOR_SECURITY_MODULE_DESMANTLE_RESUME:
                    szTemp = "MINOR_SECURITY_MODULE_DESMANTLE_RESUME";
                    break;
                case CHCNetSDK.MINOR_POS_START_ALARM:
                    szTemp = "MINOR_POS_START_ALARM";
                    break;
                case CHCNetSDK.MINOR_POS_END_ALARM:
                    szTemp = "MINOR_POS_END_ALARM";
                    break;
                case CHCNetSDK.MINOR_FACE_IMAGE_QUALITY_LOW:
                    szTemp = "MINOR_FACE_IMAGE_QUALITY_LOW";
                    break;
                case CHCNetSDK.MINOR_FINGE_RPRINT_QUALITY_LOW:
                    szTemp = "MINOR_FINGE_RPRINT_QUALITY_LOW";
                    break;
                case CHCNetSDK.MINOR_FIRE_IMPORT_SHORT_CIRCUIT:
                    szTemp = "MINOR_FIRE_IMPORT_SHORT_CIRCUIT";
                    break;
                case CHCNetSDK.MINOR_FIRE_IMPORT_BROKEN_CIRCUIT:
                    szTemp = "MINOR_FIRE_IMPORT_BROKEN_CIRCUIT";
                    break;
                case CHCNetSDK.MINOR_FIRE_IMPORT_RESUME:
                    szTemp = "MINOR_FIRE_IMPORT_RESUME";
                    break;
                case CHCNetSDK.MINOR_FIRE_BUTTON_TRIGGER:
                    szTemp = "MINOR_FIRE_BUTTON_TRIGGER";
                    break;
                case CHCNetSDK.MINOR_FIRE_BUTTON_RESUME:
                    szTemp = "MINOR_FIRE_BUTTON_RESUME";
                    break;
                case CHCNetSDK.MINOR_MAINTENANCE_BUTTON_TRIGGER:
                    szTemp = "MINOR_MAINTENANCE_BUTTON_TRIGGER";
                    break;
                case CHCNetSDK.MINOR_MAINTENANCE_BUTTON_RESUME:
                    szTemp = "MINOR_MAINTENANCE_BUTTON_RESUME";
                    break;
                case CHCNetSDK.MINOR_EMERGENCY_BUTTON_TRIGGER:
                    szTemp = "MINOR_EMERGENCY_BUTTON_TRIGGER";
                    break;
                case CHCNetSDK.MINOR_EMERGENCY_BUTTON_RESUME:
                    szTemp = "MINOR_EMERGENCY_BUTTON_RESUME";
                    break;
                case CHCNetSDK.MINOR_DISTRACT_CONTROLLER_ALARM:
                    szTemp = "MINOR_DISTRACT_CONTROLLER_ALARM";
                    break;
                case CHCNetSDK.MINOR_DISTRACT_CONTROLLER_RESUME:
                    szTemp = "MINOR_DISTRACT_CONTROLLER_RESUME";
                    break;
                default:
                    szTemp = "0x" + stLogInfo.dwMinorType;
                    break;
            }
            szTemp.CopyTo(0, csTmp, 0, szTemp.Length);
            return;
        }

        public static void OperationMinorTypeMap(CHCNetSDK.NET_DVR_LOG_V30 stLogInfo, char[] csTmp)
        {
            string szTemp;
            switch (stLogInfo.dwMinorType)
            {
                //operation
                case CHCNetSDK.MINOR_LOCAL_UPGRADE:
                    szTemp = "MINOR_LOCAL_UPGRADE";
                    break;
                case CHCNetSDK.MINOR_REMOTE_LOGIN:
                    szTemp = "REMOTE_LOGIN";
                    break;
                case CHCNetSDK.MINOR_REMOTE_LOGOUT:
                    szTemp = "REMOTE_LOGOUT";
                    break;
                case CHCNetSDK.MINOR_REMOTE_ARM:
                    szTemp = "REMOTE_ARM";
                    break;
                case CHCNetSDK.MINOR_REMOTE_DISARM:
                    szTemp = "REMOTE_DISARM";
                    break;
                case CHCNetSDK.MINOR_REMOTE_REBOOT:
                    szTemp = "REMOTE_REBOOT";
                    break;
                case CHCNetSDK.MINOR_REMOTE_UPGRADE:
                    szTemp = "REMOTE_UPGRADE";
                    break;
                case CHCNetSDK.MINOR_REMOTE_CFGFILE_OUTPUT:
                    szTemp = "REMOTE_CFGFILE_OUTPUT";
                    break;
                case CHCNetSDK.MINOR_REMOTE_CFGFILE_INTPUT:
                    szTemp = "REMOTE_CFGFILE_INTPUT";
                    break;
                case CHCNetSDK.MINOR_REMOTE_ALARMOUT_OPEN_MAN:
                    szTemp = "MINOR_REMOTE_ALARMOUT_OPEN_MAN";
                    break;
                case CHCNetSDK.MINOR_REMOTE_ALARMOUT_CLOSE_MAN:
                    szTemp = "MINOR_REMOTE_ALARMOUT_CLOSE_MAN";
                    break;
                case CHCNetSDK.MINOR_REMOTE_OPEN_DOOR:
                    szTemp = "MINOR_REMOTE_OPEN_DOOR";
                    break;
                case CHCNetSDK.MINOR_REMOTE_CLOSE_DOOR:
                    szTemp = "MINOR_REMOTE_CLOSE_DOOR";
                    break;
                case CHCNetSDK.MINOR_REMOTE_ALWAYS_OPEN:
                    szTemp = "MINOR_REMOTE_ALWAYS_OPEN";
                    break;
                case CHCNetSDK.MINOR_REMOTE_ALWAYS_CLOSE:
                    szTemp = "MINOR_REMOTE_ALWAYS_CLOSE";
                    break;
                case CHCNetSDK.MINOR_REMOTE_CHECK_TIME:
                    szTemp = "MINOR_REMOTE_CHECK_TIME";
                    break;
                case CHCNetSDK.MINOR_NTP_CHECK_TIME:
                    szTemp = "MINOR_NTP_CHECK_TIME";
                    break;
                case CHCNetSDK.MINOR_REMOTE_CLEAR_CARD:
                    szTemp = "MINOR_REMOTE_CLEAR_CARD"; ;
                    break;
                case CHCNetSDK.MINOR_REMOTE_RESTORE_CFG:
                    szTemp = "MINOR_REMOTE_RESTORE_CFG";
                    break;
                case CHCNetSDK.MINOR_ALARMIN_ARM:
                    szTemp = "MINOR_ALARMIN_ARM";
                    break;
                case CHCNetSDK.MINOR_ALARMIN_DISARM:
                    szTemp = "MINOR_ALARMIN_DISARM";
                    break;
                case CHCNetSDK.MINOR_LOCAL_RESTORE_CFG:
                    szTemp = "MINOR_LOCAL_RESTORE_CFG";
                    break;
                case CHCNetSDK.MINOR_MOD_NET_REPORT_CFG:
                    szTemp = "MINOR_MOD_NET_REPORT_CFG";
                    break;
                case CHCNetSDK.MINOR_MOD_GPRS_REPORT_PARAM:
                    szTemp = "MINOR_MOD_GPRS_REPORT_PARAM";
                    break;
                case CHCNetSDK.MINOR_MOD_REPORT_GROUP_PARAM:
                    szTemp = "MINOR_MOD_REPORT_GROUP_PARAM";
                    break;
                case CHCNetSDK.MINOR_UNLOCK_PASSWORD_OPEN_DOOR:
                    szTemp = "MINOR_UNLOCK_PASSWORD_OPEN_DOOR";
                    break;
                case CHCNetSDK.MINOR_REMOTE_CAPTURE_PIC:
                    szTemp = "MINOR_REMOTE_CAPTURE_PIC"; ;
                    break;
                case CHCNetSDK.MINOR_AUTO_RENUMBER:
                    szTemp = "MINOR_AUTO_RENUMBER";
                    break;
                case CHCNetSDK.MINOR_AUTO_COMPLEMENT_NUMBER:
                    szTemp = "MINOR_AUTO_COMPLEMENT_NUMBER";
                    break;
                case CHCNetSDK.MINOR_NORMAL_CFGFILE_INPUT:
                    szTemp = "MINOR_NORMAL_CFGFILE_INPUT";
                    break;
                case CHCNetSDK.MINOR_NORMAL_CFGFILE_OUTTPUT:
                    szTemp = "MINOR_NORMAL_CFGFILE_OUTTPUT";
                    break;
                case CHCNetSDK.MINOR_CARD_RIGHT_INPUT:
                    szTemp = "MINOR_CARD_RIGHT_INPUT";
                    break;
                case CHCNetSDK.MINOR_CARD_RIGHT_OUTTPUT:
                    szTemp = "MINOR_CARD_RIGHT_OUTTPUT";
                    break;
                case CHCNetSDK.MINOR_LOCAL_USB_UPGRADE:
                    szTemp = "MINOR_LOCAL_USB_UPGRADE";
                    break;
                case CHCNetSDK.MINOR_REMOTE_VISITOR_CALL_LADDER:
                    szTemp = "MINOR_REMOTE_VISITOR_CALL_LADDER"; ;
                    break;
                case CHCNetSDK.MINOR_REMOTE_HOUSEHOLD_CALL_LADDER:
                    szTemp = "MINOR_REMOTE_HOUSEHOLD_CALL_LADDER"; ;
                    break;
                default:
                    szTemp = "0x" + stLogInfo.dwMinorType;
                    break;
            }
            szTemp.CopyTo(0, csTmp, 0, szTemp.Length);
            return;
        }

        public static void ExceptionMinorTypeMap(CHCNetSDK.NET_DVR_LOG_V30 stLogInfo, char[] csTmp)
        {
            String szTemp = null;
            switch (stLogInfo.dwMinorType)
            {
                //exception
                case CHCNetSDK.MINOR_NET_BROKEN:
                    szTemp = "MINOR_NET_BROKEN";
                    break;
                case CHCNetSDK.MINOR_RS485_DEVICE_ABNORMAL:
                    szTemp = "MINOR_RS485_DEVICE_ABNORMAL";
                    break;
                case CHCNetSDK.MINOR_RS485_DEVICE_REVERT:
                    szTemp = "MINOR_RS485_DEVICE_REVERT";
                    break;
                case CHCNetSDK.MINOR_DEV_POWER_ON:
                    szTemp = "MINOR_DEV_POWER_ON";
                    break;
                case CHCNetSDK.MINOR_DEV_POWER_OFF:
                    szTemp = "MINOR_DEV_POWER_OFF";
                    break;
                case CHCNetSDK.MINOR_WATCH_DOG_RESET:
                    szTemp = "MINOR_WATCH_DOG_RESET";
                    break;
                case CHCNetSDK.MINOR_LOW_BATTERY:
                    szTemp = "MINOR_LOW_BATTERY";
                    break;
                case CHCNetSDK.MINOR_BATTERY_RESUME:
                    szTemp = "MINOR_BATTERY_RESUME";
                    break;
                case CHCNetSDK.MINOR_AC_OFF:
                    szTemp = "MINOR_AC_OFF";
                    break;
                case CHCNetSDK.MINOR_AC_RESUME:
                    szTemp = "MINOR_AC_RESUME";
                    break;
                case CHCNetSDK.MINOR_NET_RESUME:
                    szTemp = "MINOR_NET_RESUME";
                    break;
                case CHCNetSDK.MINOR_FLASH_ABNORMAL:
                    szTemp = "MINOR_FLASH_ABNORMAL";
                    break;
                case CHCNetSDK.MINOR_CARD_READER_OFFLINE:
                    szTemp = "MINOR_CARD_READER_OFFLINE";
                    break;
                case CHCNetSDK.MINOR_CARD_READER_RESUME:
                    szTemp = "MINOR_CAED_READER_RESUME";
                    break;
                case CHCNetSDK.MINOR_INDICATOR_LIGHT_OFF:
                    szTemp = "MINOR_INDICATOR_LIGHT_OFF";
                    break;
                case CHCNetSDK.MINOR_INDICATOR_LIGHT_RESUME:
                    szTemp = "MINOR_INDICATOR_LIGHT_RESUME";
                    break;
                case CHCNetSDK.MINOR_CHANNEL_CONTROLLER_OFF:
                    szTemp = "MINOR_CHANNEL_CONTROLLER_OFF";
                    break;
                case CHCNetSDK.MINOR_CHANNEL_CONTROLLER_RESUME:
                    szTemp = "MINOR_CHANNEL_CONTROLLER_RESUME";
                    break;
                case CHCNetSDK.MINOR_SECURITY_MODULE_OFF:
                    szTemp = "MINOR_SECURITY_MODULE_OFF";
                    break;
                case CHCNetSDK.MINOR_SECURITY_MODULE_RESUME:
                    szTemp = "MINOR_SECURITY_MODULE_RESUME";
                    break;
                case CHCNetSDK.MINOR_BATTERY_ELECTRIC_LOW:
                    szTemp = "MINOR_BATTERY_ELECTRIC_LOW";
                    break;
                case CHCNetSDK.MINOR_BATTERY_ELECTRIC_RESUME:
                    szTemp = "MINOR_BATTERY_ELECTRIC_RESUME";
                    break;
                case CHCNetSDK.MINOR_LOCAL_CONTROL_NET_BROKEN:
                    szTemp = "MINOR_LOCAL_CONTROL_NET_BROKEN";
                    break;
                case CHCNetSDK.MINOR_LOCAL_CONTROL_NET_RSUME:
                    szTemp = "MINOR_LOCAL_CONTROL_NET_RSUME";
                    break;
                case CHCNetSDK.MINOR_MASTER_RS485_LOOPNODE_BROKEN:
                    szTemp = "MINOR_MASTER_RS485_LOOPNODE_BROKEN";
                    break;
                case CHCNetSDK.MINOR_MASTER_RS485_LOOPNODE_RESUME:
                    szTemp = "MINOR_MASTER_RS485_LOOPNODE_RESUME";
                    break;
                case CHCNetSDK.MINOR_LOCAL_CONTROL_OFFLINE:
                    szTemp = "MINOR_LOCAL_CONTROL_OFFLINE";
                    break;
                case CHCNetSDK.MINOR_LOCAL_CONTROL_RESUME:
                    szTemp = "MINOR_LOCAL_CONTROL_RESUME";
                    break;
                case CHCNetSDK.MINOR_LOCAL_DOWNSIDE_RS485_LOOPNODE_BROKEN:
                    szTemp = "MINOR_LOCAL_DOWNSIDE_RS485_LOOPNODE_BROKEN";
                    break;
                case CHCNetSDK.MINOR_LOCAL_DOWNSIDE_RS485_LOOPNODE_RESUME:
                    szTemp = "MINOR_LOCAL_DOWNSIDE_RS485_LOOPNODE_RESUME";
                    break;
                case CHCNetSDK.MINOR_DISTRACT_CONTROLLER_ONLINE:
                    szTemp = "MINOR_DISTRACT_CONTROLLER_ONLINE";
                    break;
                case CHCNetSDK.MINOR_DISTRACT_CONTROLLER_OFFLINE:
                    szTemp = "MINOR_DISTRACT_CONTROLLER_OFFLINE";
                    break;
                case CHCNetSDK.MINOR_ID_CARD_READER_NOT_CONNECT:
                    szTemp = "MINOR_ID_CARD_READER_NOT_CONNECT";
                    break;
                case CHCNetSDK.MINOR_ID_CARD_READER_RESUME:
                    szTemp = "MINOR_ID_CARD_READER_RESUME";
                    break;
                case CHCNetSDK.MINOR_FINGER_PRINT_MODULE_NOT_CONNECT:
                    szTemp = "MINOR_FINGER_PRINT_MODULE_NOT_CONNECT";
                    break;
                case CHCNetSDK.MINOR_FINGER_PRINT_MODULE_RESUME:
                    szTemp = "MINOR_FINGER_PRINT_MODULE_RESUME";
                    break;
                case CHCNetSDK.MINOR_CAMERA_NOT_CONNECT:
                    szTemp = "MINOR_CAMERA_NOT_CONNECT";
                    break;
                case CHCNetSDK.MINOR_CAMERA_RESUME:
                    szTemp = "MINOR_CAMERA_RESUME";
                    break;
                case CHCNetSDK.MINOR_COM_NOT_CONNECT:
                    szTemp = "MINOR_COM_NOT_CONNECT";
                    break;
                case CHCNetSDK.MINOR_COM_RESUME:
                    szTemp = "MINOR_COM_RESUME";
                    break;
                case CHCNetSDK.MINOR_DEVICE_NOT_AUTHORIZE:
                    szTemp = "MINOR_DEVICE_NOT_AUTHORIZE";
                    break;
                case CHCNetSDK.MINOR_PEOPLE_AND_ID_CARD_DEVICE_ONLINE:
                    szTemp = "MINOR_PEOPLE_AND_ID_CARD_DEVICE_ONLINE";
                    break;
                case CHCNetSDK.MINOR_PEOPLE_AND_ID_CARD_DEVICE_OFFLINE:
                    szTemp = "MINOR_PEOPLE_AND_ID_CARD_DEVICE_OFFLINE";
                    break;
                case CHCNetSDK.MINOR_LOCAL_LOGIN_LOCK:
                    szTemp = "MINOR_LOCAL_LOGIN_LOCK";
                    break;
                case CHCNetSDK.MINOR_LOCAL_LOGIN_UNLOCK:
                    szTemp = "MINOR_LOCAL_LOGIN_UNLOCK";
                    break;
                default:
                    szTemp = "0x" + stLogInfo.dwMinorType;
                    break;
            }
            szTemp.CopyTo(0, csTmp, 0, szTemp.Length);
            return;
        }

        public static void EventMinorTypeMap(CHCNetSDK.NET_DVR_LOG_V30 stLogInfo, char[] csTmp)
        {
            String szTemp = null;
            switch (stLogInfo.dwMinorType)
            {
                case CHCNetSDK.MINOR_LEGAL_CARD_PASS:
                    szTemp = "MINOR_LEGAL_CARD_PASS";
                    break;
                case CHCNetSDK.MINOR_CARD_AND_PSW_PASS:
                    szTemp = "MINOR_CARD_AND_PSW_PASS";
                    break;
                case CHCNetSDK.MINOR_CARD_AND_PSW_FAIL:
                    szTemp = "MINOR_CARD_AND_PSW_FAIL";
                    break;
                case CHCNetSDK.MINOR_CARD_AND_PSW_TIMEOUT:
                    szTemp = "MINOR_CARD_AND_PSW_TIMEOUT";
                    break;
                case CHCNetSDK.MINOR_CARD_AND_PSW_OVER_TIME:
                    szTemp = "MINOR_CARD_AND_PSW_OVER_TIME";
                    break;
                case CHCNetSDK.MINOR_CARD_NO_RIGHT:
                    szTemp = "MINOR_CARD_NO_RIGHT";
                    break;
                case CHCNetSDK.MINOR_CARD_INVALID_PERIOD:
                    szTemp = "MINOR_CARD_INVALID_PERIOD";
                    break;
                case CHCNetSDK.MINOR_CARD_OUT_OF_DATE:
                    szTemp = "MINOR_CARD_OUT_OF_DATE";
                    break;
                case CHCNetSDK.MINOR_INVALID_CARD:
                    szTemp = "MINOR_INVALID_CARD";
                    break;
                case CHCNetSDK.MINOR_ANTI_SNEAK_FAIL:
                    szTemp = "MINOR_ANTI_SNEAK_FAIL";
                    break;
                case CHCNetSDK.MINOR_INTERLOCK_DOOR_NOT_CLOSE:
                    szTemp = "MINOR_INTERLOCK_DOOR_NOT_CLOSE";
                    break;
                case CHCNetSDK.MINOR_NOT_BELONG_MULTI_GROUP:
                    szTemp = "MINOR_NOT_BELONG_MULTI_GROUP";
                    break;
                case CHCNetSDK.MINOR_INVALID_MULTI_VERIFY_PERIOD:
                    szTemp = "MINOR_INVALID_MULTI_VERIFY_PERIOD";
                    break;
                case CHCNetSDK.MINOR_MULTI_VERIFY_SUPER_RIGHT_FAIL:
                    szTemp = "MINOR_MULTI_VERIFY_SUPER_RIGHT_FAIL";
                    break;
                case CHCNetSDK.MINOR_MULTI_VERIFY_REMOTE_RIGHT_FAIL:
                    szTemp = "MINOR_MULTI_VERIFY_REMOTE_RIGHT_FAIL";
                    break;
                case CHCNetSDK.MINOR_MULTI_VERIFY_SUCCESS:
                    szTemp = "MINOR_MULTI_VERIFY_SUCCESS";
                    break;
                case CHCNetSDK.MINOR_LEADER_CARD_OPEN_BEGIN:
                    szTemp = "MINOR_LEADER_CARD_OPEN_BEGIN";
                    break;
                case CHCNetSDK.MINOR_LEADER_CARD_OPEN_END:
                    szTemp = "MINOR_LEADER_CARD_OPEN_END";
                    break;
                case CHCNetSDK.MINOR_ALWAYS_OPEN_BEGIN:
                    szTemp = "MINOR_ALWAYS_OPEN_BEGIN";
                    break;
                case CHCNetSDK.MINOR_ALWAYS_OPEN_END:
                    szTemp = "MINOR_ALWAYS_OPEN_END";
                    break;
                case CHCNetSDK.MINOR_LOCK_OPEN:
                    szTemp = "MINOR_LOCK_OPEN";
                    break;
                case CHCNetSDK.MINOR_LOCK_CLOSE:
                    szTemp = "MINOR_LOCK_CLOSE";
                    break;
                case CHCNetSDK.MINOR_DOOR_BUTTON_PRESS:
                    szTemp = "MINOR_DOOR_BUTTON_PRESS";
                    break;
                case CHCNetSDK.MINOR_DOOR_BUTTON_RELEASE:
                    szTemp = "MINOR_DOOR_BUTTON_RELEASE";
                    break;
                case CHCNetSDK.MINOR_DOOR_OPEN_NORMAL:
                    szTemp = "MINOR_DOOR_OPEN_NORMAL";
                    break;
                case CHCNetSDK.MINOR_DOOR_CLOSE_NORMAL:
                    szTemp = "MINOR_DOOR_CLOSE_NORMAL";
                    break;
                case CHCNetSDK.MINOR_DOOR_OPEN_ABNORMAL:
                    szTemp = "MINOR_DOOR_OPEN_ABNORMAL";
                    break;
                case CHCNetSDK.MINOR_DOOR_OPEN_TIMEOUT:
                    szTemp = "MINOR_DOOR_OPEN_TIMEOUT";
                    break;
                case CHCNetSDK.MINOR_ALARMOUT_ON:
                    szTemp = "MINOR_ALARMOUT_ON";
                    break;
                case CHCNetSDK.MINOR_ALARMOUT_OFF:
                    szTemp = "MINOR_ALARMOUT_OFF";
                    break;
                case CHCNetSDK.MINOR_ALWAYS_CLOSE_BEGIN:
                    szTemp = "MINOR_ALWAYS_CLOSE_BEGIN";
                    break;
                case CHCNetSDK.MINOR_ALWAYS_CLOSE_END:
                    szTemp = "MINOR_ALWAYS_CLOSE_END";
                    break;
                case CHCNetSDK.MINOR_MULTI_VERIFY_NEED_REMOTE_OPEN:
                    szTemp = "MINOR_MULTI_VERIFY_NEED_REMOTE_OPEN";
                    break;
                case CHCNetSDK.MINOR_MULTI_VERIFY_SUPERPASSWD_VERIFY_SUCCESS:
                    szTemp = "MINOR_MULTI_VERIFY_SUPERPASSWD_VERIFY_SUCCESS";
                    break;
                case CHCNetSDK.MINOR_MULTI_VERIFY_REPEAT_VERIFY:
                    szTemp = "MINOR_MULTI_VERIFY_REPEAT_VERIFY";
                    break;
                case CHCNetSDK.MINOR_MULTI_VERIFY_TIMEOUT:
                    szTemp = "MINOR_MULTI_VERIFY_TIMEOUT";
                    break;
                case CHCNetSDK.MINOR_DOORBELL_RINGING:
                    szTemp = "MINOR_DOORBELL_RINGING";
                    break;
                case CHCNetSDK.MINOR_FINGERPRINT_COMPARE_PASS:
                    szTemp = "MINOR_FINGERPRINT_COMPARE_PASS";
                    break;
                case CHCNetSDK.MINOR_FINGERPRINT_COMPARE_FAIL:
                    szTemp = "MINOR_FINGERPRINT_COMPARE_FAIL";
                    break;
                case CHCNetSDK.MINOR_CARD_FINGERPRINT_VERIFY_PASS:
                    szTemp = "MINOR_CARD_FINGERPRINT_VERIFY_PASS";
                    break;
                case CHCNetSDK.MINOR_CARD_FINGERPRINT_VERIFY_FAIL:
                    szTemp = "MINOR_CARD_FINGERPRINT_VERIFY_FAIL";
                    break;
                case CHCNetSDK.MINOR_CARD_FINGERPRINT_VERIFY_TIMEOUT:
                    szTemp = "MINOR_CARD_FINGERPRINT_VERIFY_TIMEOUT";
                    break;
                case CHCNetSDK.MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_PASS:
                    szTemp = "MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_PASS";
                    break;
                case CHCNetSDK.MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_FAIL:
                    szTemp = "MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_FAIL";
                    break;
                case CHCNetSDK.MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_TIMEOUT:
                    szTemp = "MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_TIMEOUT";
                    break;
                case CHCNetSDK.MINOR_FINGERPRINT_PASSWD_VERIFY_PASS:
                    szTemp = "MINOR_FINGERPRINT_PASSWD_VERIFY_PASS";
                    break;
                case CHCNetSDK.MINOR_FINGERPRINT_PASSWD_VERIFY_FAIL:
                    szTemp = "MINOR_FINGERPRINT_PASSWD_VERIFY_FAIL";
                    break;
                case CHCNetSDK.MINOR_FINGERPRINT_PASSWD_VERIFY_TIMEOUT:
                    szTemp = "MINOR_FINGERPRINT_PASSWD_VERIFY_TIMEOUT";
                    break;
                case CHCNetSDK.MINOR_FINGERPRINT_INEXISTENCE:
                    szTemp = "MINOR_FINGERPRINT_INEXISTENCE";
                    break;
                case CHCNetSDK.MINOR_CARD_PLATFORM_VERIFY:
                    szTemp = "MINOR_CARD_PLATFORM_VERIFY";
                    break;
                case CHCNetSDK.MINOR_CALL_CENTER:
                    szTemp = "MINOR_CALL_CENTER";
                    break;
                case CHCNetSDK.MINOR_FIRE_RELAY_TURN_ON_DOOR_ALWAYS_OPEN:
                    szTemp = "MINOR_FIRE_RELAY_TURN_ON_DOOR_ALWAYS_OPEN";
                    break;
                case CHCNetSDK.MINOR_FIRE_RELAY_RECOVER_DOOR_RECOVER_NORMAL:
                    szTemp = "MINOR_FIRE_RELAY_RECOVER_DOOR_RECOVER_NORMAL";
                    break;
                case CHCNetSDK.MINOR_FACE_AND_FP_VERIFY_PASS:
                    szTemp = "MINOR_FACE_AND_FP_VERIFY_PASS";
                    break;
                case CHCNetSDK.MINOR_FACE_AND_FP_VERIFY_FAIL:
                    szTemp = "MINOR_FACE_AND_FP_VERIFY_FAIL";
                    break;
                case CHCNetSDK.MINOR_FACE_AND_FP_VERIFY_TIMEOUT:
                    szTemp = "MINOR_FACE_AND_FP_VERIFY_TIMEOUT";
                    break;
                case CHCNetSDK.MINOR_FACE_AND_PW_VERIFY_PASS:
                    szTemp = "MINOR_FACE_AND_PW_VERIFY_PASS";
                    break;
                case CHCNetSDK.MINOR_FACE_AND_PW_VERIFY_FAIL:
                    szTemp = "MINOR_FACE_AND_PW_VERIFY_FAIL";
                    break;
                case CHCNetSDK.MINOR_FACE_AND_PW_VERIFY_TIMEOUT:
                    szTemp = "MINOR_FACE_AND_PW_VERIFY_TIMEOUT";
                    break;
                case CHCNetSDK.MINOR_FACE_AND_CARD_VERIFY_PASS:
                    szTemp = "MINOR_FACE_AND_CARD_VERIFY_PASS";
                    break;
                case CHCNetSDK.MINOR_FACE_AND_CARD_VERIFY_FAIL:
                    szTemp = "MINOR_FACE_AND_CARD_VERIFY_FAIL";
                    break;
                case CHCNetSDK.MINOR_FACE_AND_CARD_VERIFY_TIMEOUT:
                    szTemp = "MINOR_FACE_AND_CARD_VERIFY_TIMEOUT";
                    break;
                case CHCNetSDK.MINOR_FACE_AND_PW_AND_FP_VERIFY_PASS:
                    szTemp = "MINOR_FACE_AND_PW_AND_FP_VERIFY_PASS";
                    break;
                case CHCNetSDK.MINOR_FACE_AND_PW_AND_FP_VERIFY_FAIL:
                    szTemp = "MINOR_FACE_AND_PW_AND_FP_VERIFY_FAIL";
                    break;
                case CHCNetSDK.MINOR_FACE_AND_PW_AND_FP_VERIFY_TIMEOUT:
                    szTemp = "MINOR_FACE_AND_PW_AND_FP_VERIFY_TIMEOUT";
                    break;
                case CHCNetSDK.MINOR_FACE_CARD_AND_FP_VERIFY_PASS:
                    szTemp = "MINOR_FACE_CARD_AND_FP_VERIFY_PASS";
                    break;
                case CHCNetSDK.MINOR_FACE_CARD_AND_FP_VERIFY_FAIL:
                    szTemp = "MINOR_FACE_CARD_AND_FP_VERIFY_FAIL";
                    break;
                case CHCNetSDK.MINOR_FACE_CARD_AND_FP_VERIFY_TIMEOUT:
                    szTemp = "MINOR_FACE_CARD_AND_FP_VERIFY_TIMEOUT";
                    break;
                case CHCNetSDK.MINOR_EMPLOYEENO_AND_FP_VERIFY_PASS:
                    szTemp = "MINOR_EMPLOYEENO_AND_FP_VERIFY_PASS";
                    break;
                case CHCNetSDK.MINOR_EMPLOYEENO_AND_FP_VERIFY_FAIL:
                    szTemp = "MINOR_EMPLOYEENO_AND_FP_VERIFY_FAIL";
                    break;
                case CHCNetSDK.MINOR_EMPLOYEENO_AND_FP_VERIFY_TIMEOUT:
                    szTemp = "MINOR_EMPLOYEENO_AND_FP_VERIFY_TIMEOUT";
                    break;
                case CHCNetSDK.MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_PASS:
                    szTemp = "MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_PASS";
                    break;
                case CHCNetSDK.MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_FAIL:
                    szTemp = "MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_FAIL";
                    break;
                case CHCNetSDK.MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_TIMEOUT:
                    szTemp = "MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_TIMEOUT";
                    break;
                case CHCNetSDK.MINOR_FACE_VERIFY_PASS:
                    szTemp = "MINOR_FACE_VERIFY_PASS";
                    break;
                case CHCNetSDK.MINOR_FACE_VERIFY_FAIL:
                    szTemp = "MINOR_FACE_VERIFY_FAIL";
                    break;
                case CHCNetSDK.MINOR_EMPLOYEENO_AND_FACE_VERIFY_PASS:
                    szTemp = "MINOR_EMPLOYEENO_AND_FACE_VERIFY_PASS";
                    break;
                case CHCNetSDK.MINOR_EMPLOYEENO_AND_FACE_VERIFY_FAIL:
                    szTemp = "MINOR_EMPLOYEENO_AND_FACE_VERIFY_FAIL";
                    break;
                case CHCNetSDK.MINOR_EMPLOYEENO_AND_FACE_VERIFY_TIMEOUT:
                    szTemp = "MINOR_EMPLOYEENO_AND_FACE_VERIFY_TIMEOUT";
                    break;
                case CHCNetSDK.MINOR_FACE_RECOGNIZE_FAIL:
                    szTemp = "MINOR_FACE_RECOGNIZE_FAIL";
                    break;
                case CHCNetSDK.MINOR_FIRSTCARD_AUTHORIZE_BEGIN:
                    szTemp = "MINOR_FIRSTCARD_AUTHORIZE_BEGIN";
                    break;
                case CHCNetSDK.MINOR_FIRSTCARD_AUTHORIZE_END:
                    szTemp = "MINOR_FIRSTCARD_AUTHORIZE_END";
                    break;
                case CHCNetSDK.MINOR_DOORLOCK_INPUT_SHORT_CIRCUIT:
                    szTemp = "MINOR_DOORLOCK_INPUT_SHORT_CIRCUIT";
                    break;
                case CHCNetSDK.MINOR_DOORLOCK_INPUT_BROKEN_CIRCUIT:
                    szTemp = "MINOR_DOORLOCK_INPUT_BROKEN_CIRCUIT";
                    break;
                case CHCNetSDK.MINOR_DOORLOCK_INPUT_EXCEPTION:
                    szTemp = "MINOR_DOORLOCK_INPUT_EXCEPTION";
                    break;
                case CHCNetSDK.MINOR_DOORCONTACT_INPUT_SHORT_CIRCUIT:
                    szTemp = "MINOR_DOORCONTACT_INPUT_SHORT_CIRCUIT";
                    break;
                case CHCNetSDK.MINOR_DOORCONTACT_INPUT_BROKEN_CIRCUIT:
                    szTemp = "MINOR_DOORCONTACT_INPUT_BROKEN_CIRCUIT";
                    break;
                case CHCNetSDK.MINOR_DOORCONTACT_INPUT_EXCEPTION:
                    szTemp = "MINOR_DOORCONTACT_INPUT_EXCEPTION";
                    break;
                case CHCNetSDK.MINOR_OPENBUTTON_INPUT_SHORT_CIRCUIT:
                    szTemp = "MINOR_OPENBUTTON_INPUT_SHORT_CIRCUIT";
                    break;
                case CHCNetSDK.MINOR_OPENBUTTON_INPUT_BROKEN_CIRCUIT:
                    szTemp = "MINOR_OPENBUTTON_INPUT_BROKEN_CIRCUIT";
                    break;
                case CHCNetSDK.MINOR_OPENBUTTON_INPUT_EXCEPTION:
                    szTemp = "MINOR_OPENBUTTON_INPUT_EXCEPTION";
                    break;
                case CHCNetSDK.MINOR_DOORLOCK_OPEN_EXCEPTION:
                    szTemp = "MINOR_DOORLOCK_OPEN_EXCEPTION";
                    break;
                case CHCNetSDK.MINOR_DOORLOCK_OPEN_TIMEOUT:
                    szTemp = "MINOR_DOORLOCK_OPEN_TIMEOUT";
                    break;
                case CHCNetSDK.MINOR_FIRSTCARD_OPEN_WITHOUT_AUTHORIZE:
                    szTemp = "MINOR_FIRSTCARD_OPEN_WITHOUT_AUTHORIZE";
                    break;
                case CHCNetSDK.MINOR_CALL_LADDER_RELAY_BREAK:
                    szTemp = "MINOR_CALL_LADDER_RELAY_BREAK";
                    break;
                case CHCNetSDK.MINOR_CALL_LADDER_RELAY_CLOSE:
                    szTemp = "MINOR_CALL_LADDER_RELAY_CLOSE";
                    break;
                case CHCNetSDK.MINOR_AUTO_KEY_RELAY_BREAK:
                    szTemp = "MINOR_AUTO_KEY_RELAY_BREAK";
                    break;
                case CHCNetSDK.MINOR_AUTO_KEY_RELAY_CLOSE:
                    szTemp = "MINOR_AUTO_KEY_RELAY_CLOSE";
                    break;
                case CHCNetSDK.MINOR_KEY_CONTROL_RELAY_BREAK:
                    szTemp = "MINOR_KEY_CONTROL_RELAY_BREAK";
                    break;
                case CHCNetSDK.MINOR_KEY_CONTROL_RELAY_CLOSE:
                    szTemp = "MINOR_KEY_CONTROL_RELAY_CLOSE";
                    break;
                case CHCNetSDK.MINOR_EMPLOYEENO_AND_PW_PASS:
                    szTemp = "MINOR_EMPLOYEENO_AND_PW_PASS";
                    break;
                case CHCNetSDK.MINOR_EMPLOYEENO_AND_PW_FAIL:
                    szTemp = "MINOR_EMPLOYEENO_AND_PW_FAIL";
                    break;
                case CHCNetSDK.MINOR_EMPLOYEENO_AND_PW_TIMEOUT:
                    szTemp = "MINOR_EMPLOYEENO_AND_PW_TIMEOUT";
                    break;
                case CHCNetSDK.MINOR_HUMAN_DETECT_FAIL:
                    szTemp = "MINOR_HUMAN_DETECT_FAIL";
                    break;
                case CHCNetSDK.MINOR_PEOPLE_AND_ID_CARD_COMPARE_PASS:
                    szTemp = "MINOR_PEOPLE_AND_ID_CARD_COMPARE_PASS";
                    break;
                case CHCNetSDK.MINOR_PEOPLE_AND_ID_CARD_COMPARE_FAIL:
                    szTemp = "MINOR_PEOPLE_AND_ID_CARD_COMPARE_FAIL";
                    break;
                case CHCNetSDK.MINOR_CERTIFICATE_BLACK_LIST:
                    szTemp = "MINOR_CERTIFICATE_BLACK_LIST";
                    break;
                case CHCNetSDK.MINOR_LEGAL_MESSAGE:
                    szTemp = "MINOR_LEGAL_MESSAGE";
                    break;
                case CHCNetSDK.MINOR_ILLEGAL_MESSAGE:
                    szTemp = "MINOR_ILLEGAL_MESSAGE";
                    break;
                case CHCNetSDK.MINOR_MAC_DETECT:
                    szTemp = "MINOR_MAC_DETECT";
                    break;
                default:
                    szTemp = "Main Event unknown:" + "0x" + "stLogInfo.dwMinorType";
                    break;
            }
            szTemp.CopyTo(0, csTmp, 0, szTemp.Length);
            return;
        }
    }
}
