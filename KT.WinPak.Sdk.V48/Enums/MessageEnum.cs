using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.V48.Message
{
    public class MessageEnum
    {
        private static object locker = new object();

        public int Code { get; set; }
        public string Text { get; set; }

        private MessageEnum(int code, string text)
        {
            this.Code = code;
            this.Text = text;
        }
        private static List<MessageEnum> list;
        private static List<MessageEnum> List
        {
            get
            {
                if (list != null)
                {
                    return list;
                }
                lock (locker)
                {
                    if (list != null)
                    {
                        return list;
                    }

                    list = new List<MessageEnum>();
                    list.Add(new MessageEnum(0, "Card is added successfully to the account                                             "));
                    list.Add(new MessageEnum(1, "Card is not added to the account                                                      "));
                    list.Add(new MessageEnum(101, "Card number exists                                                                  "));
                    list.Add(new MessageEnum(102, "Card number is not valid                                                            "));
                    list.Add(new MessageEnum(103, "Card status is not valid                                                            "));
                    list.Add(new MessageEnum(104, "Access level is not valid                                                           "));
                    list.Add(new MessageEnum(105, "Account name is not valid                                                           "));
                    list.Add(new MessageEnum(106, "Year of card activation is not valid                                                "));
                    list.Add(new MessageEnum(107, "Card activation date is not valid                                                   "));
                    list.Add(new MessageEnum(108, "Card length is not valid                                                            "));
                    list.Add(new MessageEnum(109, "PIN is not valid                                                                    "));
                    list.Add(new MessageEnum(110, "Access Type is not valid                                                            "));
                    list.Add(new MessageEnum(111, "NetAXS Usage Limit is not valid                                                     "));
                    list.Add(new MessageEnum(112, "Expiry is not valid                                                                 "));
                    list.Add(new MessageEnum(113, "NetAXS Card Type is not valid                                                       "));
                    list.Add(new MessageEnum(114, "NetAXS Temporary card setting  is not valid                                         "));
                    list.Add(new MessageEnum(115, "NetAXS Limited card setting is not valid                                            ")); 
                    list.Add(new MessageEnum(201, "Access level name is not valid                                                      "));
                    list.Add(new MessageEnum(202, "Access level exists                                                                 "));
                    list.Add(new MessageEnum(203, "Access level is associated with a card                                              "));
                    list.Add(new MessageEnum(205, "List of card readers for which the access levels must be reassigned is empty        "));
                    list.Add(new MessageEnum(206, "Number of characters in the access level name / description is not valid            "));
                    list.Add(new MessageEnum(207, "ccess Reader name is invalid.                                                       "));
                    list.Add(new MessageEnum(208, "Reader Name is empty.                                                               "));
                    list.Add(new MessageEnum(209, "Group Name is invalid.                                                              "));
                    list.Add(new MessageEnum(210, "Timezone Name is invalid.                                                           "));
                    list.Add(new MessageEnum(301, "Card holder’s first / last name is not valid                                       "));
                    list.Add(new MessageEnum(302, "Number of characters in the card holder's first / last name is not valid            "));
                    list.Add(new MessageEnum(303, "photo index value is not valid.                                                     "));
                    list.Add(new MessageEnum(501, "Panel ID is not valid.                                                              "));
                    list.Add(new MessageEnum(502, "Output ID is not valid.                                                             "));
                    list.Add(new MessageEnum(503, "group ID is not valid.                                                              "));
                    list.Add(new MessageEnum(504, "Timezone ID is not valid.                                                           "));
                    list.Add(new MessageEnum(506, "The Timezone is associated with one or more Outputs.                                "));
                    list.Add(new MessageEnum(507, "The Timezone is associated with one or more Inputs.                                 "));
                    list.Add(new MessageEnum(508, "The Timezone is associated panel PIN Timezone.                                      "));
                    list.Add(new MessageEnum(509, "The Timezone is associated with one or more Groups.                                 "));
                    list.Add(new MessageEnum(511, "The Timezone is associated with one or more Readers.                                "));
                    list.Add(new MessageEnum(514, "The Timezone is associated with one of the operation mode of the FIN4000 Panel      "));
                    list.Add(new MessageEnum(515, "Invalid Lock / Unlock Option                                                        "));
                    list.Add(new MessageEnum(601, "Timezone name is not valid or empty.                                                "));
                    list.Add(new MessageEnum(602, "Timezone name already exists.                                                       "));
                    list.Add(new MessageEnum(603, "Timezone name is exceeding limits.                                                  "));
                    list.Add(new MessageEnum(604, "The mentioned timezone is a System Timezone, cannot be edited.                      "));
                    list.Add(new MessageEnum(605, "Timezone is Invalid.                                                                "));
                    list.Add(new MessageEnum(606, "Timezone is associated with one or more panels.                                     "));
                    list.Add(new MessageEnum(607, "Timezone range is not valid.                                                        "));
                    list.Add(new MessageEnum(608, "Day Type of the Timezone range is not valid                                         "));
                    list.Add(new MessageEnum(609, "Timezone is associated with one or more operator.                                   "));
                    list.Add(new MessageEnum(610, "Timezone is associated with one or more ADVs.                                       "));
                    list.Add(new MessageEnum(611, "Timezone is associated with one or more Action Messages.                            "));
                    list.Add(new MessageEnum(612, "The number of slots in the Timezone is exceeding the Panel limits.                  "));
                    list.Add(new MessageEnum(614, "Timezone does not exist.                                                            "));
                    list.Add(new MessageEnum(615, "Timezone is associated with one or more Access Levels.                              "));
                    list.Add(new MessageEnum(618, "The Action Group is invalid.                                                        "));
                    list.Add(new MessageEnum(619, "Same Timezone is already mapped in the Access Level.                                "));
                    list.Add(new MessageEnum(700, "The Name of the Holiday is Invalid                                                  "));
                    list.Add(new MessageEnum(701, "The Length of the Holiday Name is exceeding the maximum.                            "));
                    list.Add(new MessageEnum(703, "Holiday Date of the Holiday is invalid.                                             "));
                    list.Add(new MessageEnum(704, "EveryYear of the Holiday is invalid.                                                "));
                    list.Add(new MessageEnum(705, "One of the HolidayType of the Holiday is invaild.                                   "));
                    list.Add(new MessageEnum(707, "Holiday Name Exists.                                                                "));
                    list.Add(new MessageEnum(801, "Holiday Group Name already exists.                                                  "));
                    list.Add(new MessageEnum(802, "Holiday Group Name is invalid.                                                      "));
                    list.Add(new MessageEnum(803, "Length of the Holiday Group Name is invalid.                                        "));
                    list.Add(new MessageEnum(804, "Holiday List have no holidays.                                                      "));
                    list.Add(new MessageEnum(805, "One of the Name of the Holiday is invalid.                                          "));
                    list.Add(new MessageEnum(806, "One of the length of the holiday name is invalid.                                   "));
                    list.Add(new MessageEnum(807, "One of the Holiday name already exists in the Holiday Group.                        "));
                    list.Add(new MessageEnum(808, "One of the Holiday date already exists in the Holiday Group.                        "));
                    list.Add(new MessageEnum(809, "The Holiday Group Name is invalid.                                                  "));


                }
                return list;
            }
        }

        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetMessageByCode(int code)
        {
            return List.FirstOrDefault(x => x.Code == code)?.Text;
        }
    }
}
