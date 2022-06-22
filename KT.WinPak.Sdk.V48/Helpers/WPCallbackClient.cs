using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Accw;
using System.Threading;

namespace KT.WinPak.SDK.V48.Helpers
{

    //[AttributeUsageAttribute(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
    //[ComVisibleAttribute(true)]
    //public sealed class WPCallbackClient : Attribute, IWPAVCallBack, IDisposable

 
    public sealed class WPCallbackClient : IWPAVCallBack 
    {
        #region Member variables

        public Queue<string> messageQueue;
        public Queue<string> errorQueue;

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor
        /// </summary>
        public WPCallbackClient()
        {
            messageQueue = new Queue<string>();
            errorQueue = new Queue<string>();
        }

        #endregion


        #region IDisposable Member

        public void Dispose()
        {
            //messageQueue = null;
            //errorQueue = null;
        }

        #endregion

        //public override object TypeId
        //{
        //    get
        //    {
        //        return base.TypeId;
        //    }
        //}


        #region IWPAVCallBack Member

        void IWPAVCallBack.GotMessage(string bstrTranInfo)
        {
            if (String.IsNullOrEmpty(bstrTranInfo))
            {
                return;
            }

            lock (messageQueue)
            {
                messageQueue.Enqueue(bstrTranInfo);
            }
        }

        void IWPAVCallBack.ServerError(string bstrErrorInfo)
        {
            if (String.IsNullOrEmpty(bstrErrorInfo))
            {
                return;
            }

            lock (errorQueue)
            {
                errorQueue.Enqueue(bstrErrorInfo);
            }
        }

        #endregion
    }
}