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
        }


    }
}
