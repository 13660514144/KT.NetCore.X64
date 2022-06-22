using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Prowatch.Service.IDaos
{
    public interface IProwatchDao
    {
        void DownloadCardStateDeleteHiQueue(string cardId);
        void DownloadCardStateExecHiQueueCommand(string cardId, string cardNo);
        void DownloadCardStateExecQueueCardRetryDownload(string cardNo);
        void DownloadCardStateUpdateHiQueue(string cardId);
    }
}
