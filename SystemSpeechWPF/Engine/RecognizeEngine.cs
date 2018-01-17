using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace SystemSpeechWPF.Engine
{
    class RecognizeEngine : INotifyPropertyChanged
    {
        private SpeechRecognitionEngine Engine;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// プロパティの変更処理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        private void SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RecognizeEngine()
        {
            Engine = new SpeechRecognitionEngine(new CultureInfo("ja-jp"));
            IsActive = false;
        }

        /// <summary>
        /// 動いてるかどうか
        /// こいつも変更通知はしない
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// 音声コマンドのそのままのデータ（行ごとにコマンドにする）
        /// これは変更通知しない
        /// </summary>
        public string CommandList { get; set; }

        /// <summary>
        /// 認識結果のデータ
        /// </summary>
        private string resultList;
        public string ResultList
        {
            get { return resultList; }
            private set { SetProperty(ref resultList, value); }
        }

        /// <summary>
        /// 準備ができてるかどうか
        /// </summary>
        public bool IsReady
        {
            get
            {
                return !IsActive && !string.IsNullOrEmpty(CommandList);
            }
        }

        /// <summary>
        /// 文法データ作って入れる
        /// </summary>
        private void CreateGrammar()
        {

        }

        public void StartOrEndRecognize()
        {
            if (IsActive)
            {
                EndRecognize();
            }
            else
            {
                CreateGrammar();
                StartRecognize();
            }
        }


        private void StartRecognize()
        {
            if (IsReady)
            {
                IsActive = true;
                Task.Factory.StartNew(() => Engine.RecognizeAsync());
            }
        }

        private void EndRecognize()
        {
            if (IsActive)
            {
                Engine.RecognizeAsyncStop();
            }
        }
    }
}
