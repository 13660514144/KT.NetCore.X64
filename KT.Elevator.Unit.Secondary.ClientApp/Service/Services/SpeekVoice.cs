using System;
using System.Collections.Generic;
using System.Text;
using System.Speech;
using System.Speech.Synthesis;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.Services
{
    public class SpeekVoice
    {
        private SpeechSynthesizer voice;
        public SpeekVoice()
        {
            voice = new SpeechSynthesizer();
        }
        /// <summary>
        /// 语音播报，通过委托调用
        /// </summary>
        /// <param name="Str"></param>
        private void Speek(string Str)
        {
            voice.Rate = 0; //设置语速,[-10,10]
            voice.Volume = 100; //设置音量,[0,100]
            voice.Speak(Str);  //播放指定的字符串,这是同步朗读
        }
     
        private async void SpeekCompleted()
        {
            voice.SpeakAsyncCancelAll();
            //voice.Dispose();  //释放所有语音资源
        }
        public async void ToSpeek(string Str)
        {
            Task.Run(()=> {
                Speek(Str);
            }).ContinueWith(t=> { SpeekCompleted(); });
        }
    }
}
