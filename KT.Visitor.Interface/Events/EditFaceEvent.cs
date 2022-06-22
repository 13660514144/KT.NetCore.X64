using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Interface.Events
{
    public class EditFaceEvent: PubSubEvent<EditFace>
    {
    }
    public class EditFace
    { 
        public bool EditFlg { get; set; }
        public string UrlImg { get; set; }
    }
}
