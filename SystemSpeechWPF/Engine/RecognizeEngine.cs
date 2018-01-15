using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace SystemSpeechWPF.Engine
{
    class RecognizeEngine
    {
        private SpeechRecognitionEngine Engine;

        public RecognizeEngine()
        {
            Engine = new SpeechRecognitionEngine(new CultureInfo("ja-jp"));
            IsActive = false;
        }

        /// <summary>
        /// 動いてるかどうか
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// 準備ができてるかどうか
        /// </summary>
        public bool IsReady
        {
            get
            {
                return !IsActive && false/*コマンドが入っているかどうか*/;
            }
        }

        /// <summary>
        /// 文法データ作って入れる
        /// </summary>
        private void CreateGrammar()
        {

        }

        public void StartRecognize()
        {
            if (IsReady)
            {
                IsActive = true;
                Task.Factory.StartNew(() => Engine.RecognizeAsync());
            }
        }

        public void EndRecognize()
        {
            if (IsActive)
            {
                Engine.RecognizeAsyncStop();
            }
        }
    }
}
