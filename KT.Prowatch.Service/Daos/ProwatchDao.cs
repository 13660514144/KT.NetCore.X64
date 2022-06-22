using KT.Prowatch.Service.Base;
using KT.Prowatch.Service.Extensions;
using KT.Prowatch.Service.IDaos;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Prowatch.Service.Daos
{
    public class ProwatchDao : IProwatchDao
    {
        private ProwatchContext _context;

        public ProwatchDao(ProwatchContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 说明：
        /// 对每次修改卡状态后，分别依次执行下面内容
        /// 
        /// exec HI_QUEUE_COMMAND 6,N'卡号ID',N'卡号','N','N','U' 
        /// 
        /// Update HI_QUEUE set Flag=N'D' where CMD=N'6' and CPAR1=N'卡号ID'
        /// 
        /// EXEC QUEUE_CARD_RETRY_DOWNLOAD  N'卡号'
        /// 
        /// Delete from Hi_Queue where  CMD=N'6' and CPAR1=N'卡号ID'
        /// 
        /// 
        /// 
        /// 实例
        /// exec HI_QUEUE_COMMAND 6,N'0x00293d5086600dfb4a2ca89a3112769f7de8',N'88975817','N','N','U' 
        /// 
        /// Update HI_QUEUE set Flag=N'D' where CMD=N'6' and CPAR1=N'0x00293d5086600dfb4a2ca89a3112769f7de8'
        /// 
        /// EXEC QUEUE_CARD_RETRY_DOWNLOAD  N'88975817'
        /// 
        /// Delete from Hi_Queue where  CMD=N'6' and CPAR1=N'0x00293d5086600dfb4a2ca89a3112769f7de8'
        /// 
        /// 
        /// 重复操作
        /// 
        /// exec HI_QUEUE_COMMAND 6,N'卡号ID',N'卡号','N','N','U' 
        /// exec HI_QUEUE_COMMAND 6,N'卡号ID',N'卡号','N','N','U' 
        /// exec HI_QUEUE_COMMAND 6,N'卡号ID',N'卡号','N','N','U' 
        /// 
        /// Update HI_QUEUE set Flag=N'D' where CMD=N'6' and CPAR1=N'卡号ID'
        /// Update HI_QUEUE set Flag=N'D' where CMD=N'6' and CPAR1=N'卡号ID'
        /// Update HI_QUEUE set Flag=N'D' where CMD=N'6' and CPAR1=N'卡号ID'
        /// 
        /// EXEC QUEUE_CARD_RETRY_DOWNLOAD  N'卡号'
        /// EXEC QUEUE_CARD_RETRY_DOWNLOAD  N'卡号'
        /// EXEC QUEUE_CARD_RETRY_DOWNLOAD  N'卡号'
        /// 
        /// Delete from Hi_Queue where  CMD=N'6' and CPAR1=N'卡号ID'
        /// 
        /// 
        /// 
        /// _context.Database.SqlExecute($" EXEC HI_QUEUE_COMMAND 6,N'{cardId}',N'{cardNo}','N','N','U' ");
        /// _context.Database.SqlExecute($" EXEC HI_QUEUE_COMMAND 6,N'{cardId}',N'{cardNo}','N','N','U' ");
        /// _context.Database.SqlExecute($" UPDATE HI_QUEUE SET FLAG=N'D' WHERE CMD=N'6' AND CPAR1=N'{cardId}' ");
        /// _context.Database.SqlExecute($" UPDATE HI_QUEUE SET FLAG=N'D' WHERE CMD=N'6' AND CPAR1=N'{cardId}' ");
        /// _context.Database.SqlExecute($" EXEC QUEUE_CARD_RETRY_DOWNLOAD  N'{cardNo}' ");
        /// _context.Database.SqlExecute($" EXEC QUEUE_CARD_RETRY_DOWNLOAD  N'{cardNo}' ");
        /// _context.Database.SqlExecute($" DELETE FROM HI_QUEUE WHERE  CMD=N'6' AND CPAR1=N'{cardId}' "); 
        /// 
        /// 
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="cardNo"></param>
        public void DownloadCardStateExecHiQueueCommand(string cardId, string cardNo)
        {
            _context.Database.SqlExecute($" EXEC HI_QUEUE_COMMAND 6,N'{cardId}',N'{cardNo}','N','N','U' ");
        }

        public void DownloadCardStateUpdateHiQueue(string cardId)
        {
            _context.Database.SqlExecute($" UPDATE HI_QUEUE SET FLAG=N'D' WHERE CMD=N'6' AND CPAR1=N'{cardId}' ");
        }
         
        public void DownloadCardStateExecQueueCardRetryDownload(string cardNo)
        {
            _context.Database.SqlExecute($" EXEC QUEUE_CARD_RETRY_DOWNLOAD  N'{cardNo}' ");
        }
 
        public void DownloadCardStateDeleteHiQueue(string cardId)
        {
            _context.Database.SqlExecute($" DELETE FROM HI_QUEUE WHERE  CMD=N'6' AND CPAR1=N'{cardId}' ");
        }
    }
}
