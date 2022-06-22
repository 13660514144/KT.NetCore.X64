using KT.Quanta.Service.Devices.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler
{
    /// <summary>
    /// The telgram header consists of five numerical digits.
    /// </summary>
    public class SchindlerTelegramHeaderResponse : SchindlerDatabaseSerializer
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SchindlerTelegramHeaderResponse()
        {

        }

        /// <summary>
        /// 0/1/2 Message ID Reference number assigned by the transmitter
        /// Three numerical digits, starting with zero.
        /// The message ID associates an answer with a message.Therefore, an acknowledgement must
        /// contain the message ID of the operation that it refers to.
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// 3/4 Function type Message type
        /// Two numerical digits, left padded with zero.
        /// 
        /// The following functions are supported:
        /// Function                        Function Type   Transmitter         Receiver            Remark
        /// I am alive                      00              Third-party system  PORT Technology     -
        /// I am alive acknowledgement      01              Third-party system  PORT Technology     -
        /// Call by profile                 02              Third-party system  PORT Technology     Old @1,Use function type 12(recommended).
        /// Call by profile acknowledgement 03              PORT Technology     Third-party system  -
        /// Call by ID                      04              Third-party system  PORT Technology     Old @1,Use function type 14(recommended).
        /// Call by ID acknowledgement      05              PORT Technology     Third-party system  -
        /// Direct call                     06              Third-party system  PORT Technology     Old @1,Use function type 16(recommended).
        /// Direct call acknowledgement     07              PORT Technology     Third-party system  -
        /// Call by profile with NACK       12              Third-party system  PORT Technology     -
        /// Call by profile NACK            13              PORT Technology     Third-party system  -
        /// Call by ID with NACK            14              Third-party system  PORT Technology     -
        /// Call by ID NACK                 15              PORT Technology     Third-party system  -
        /// Direct call with NACK           16              Third-party system  PORT Technology     -
        /// Direct call NACK                17              PORT Technology     Third-party         -
        /// Old @1 :If the function has failed, the server does not send any error message. 
        /// </summary>
        public int FunctionType { get; set; }

        /// <summary>
        /// 数据结果
        /// </summary>
        public List<string> Datas { get; set; }

        protected override void Read()
        {
            MessageId = ReadIntSubstring(3);
            FunctionType = ReadIntSubstring(2);

            Datas = ReadEndStrings();
        }

        protected override void Write()
        {
            WriteIntSubstringPadLeft(MessageId, 3);
            WriteIntSubstringPadLeft(FunctionType, 2);
        }
    }
}
