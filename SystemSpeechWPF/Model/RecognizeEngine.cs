using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace SystemSpeechWPF.Model
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

        /// <summary>
        /// コンストラクタ。
        /// TODO : イベント関係も処理してね
        /// </summary>
        public RecognizeEngine()
        {
            Engine = new SpeechRecognitionEngine(new CultureInfo("ja-jp"));
            Engine.LoadGrammarCompleted += LoadGrammarComplitedEventHandler;
            Engine.SpeechRecognized += RecognitionEventHandler;
            Engine.SetInputToDefaultAudioDevice();
            IsGrammarLoaded = false;
            IsActive = false;
        }

        /// <summary>
        /// 動いてるかどうか
        /// </summary>
        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            private set { SetProperty(ref isActive, value); }
        }

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
        /// 文法データが読み込まれているかどうか
        /// </summary>
        private bool isGrammarLoaded;
        public bool IsGrammarLoaded
        {
            get { return isGrammarLoaded; }
            private set { SetProperty(ref isGrammarLoaded, value); }
        }

        /// <summary>
        /// 準備ができてるかどうか
        /// </summary>
        public bool IsReady
        {
            get
            {
                return !IsActive && IsGrammarLoaded;
            }
        }

        /// <summary>
        /// 文法データ作って入れる
        /// </summary>
        private void CreateGrammar()
        {
            Engine.UnloadAllGrammars();
            var gb = new GrammarBuilder();
            gb.AppendWildcard();
            gb.Append(new Choices(CommandList.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)));
            gb.AppendWildcard();
            var grammar = new Grammar(gb);
            Task.Factory.StartNew(() => Engine.LoadGrammarAsync(grammar));
        }


        /// <summary>
        /// 文法データが読み終わったときの合図
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadGrammarComplitedEventHandler(object sender, LoadGrammarCompletedEventArgs e)
        {
            IsGrammarLoaded = true;
            StartRecognize();
        }

        private void RecognitionEventHandler(object sender, RecognitionEventArgs args)
        {
            var confidence = args.Result.Confidence;
            var text = args.Result.Text;
            var result = $"[Recognition] {confidence:f3} : {text}";
            ResultList += result;
        }

        /// <summary>
        /// 開始、または終了処理
        /// </summary>
        public void StartOrEndRecognize()
        {
            if (IsActive)
            {
                EndRecognize();
            }
            else
            {
                CreateGrammar();
            }
        }
        
        private void StartRecognize()
        {
            if (IsReady)
            {
                Task.Factory.StartNew(() => Engine.RecognizeAsync());
                IsActive = true;
                ResultList += "[Message] 認識開始";
            }
        }

        private void EndRecognize()
        {
            if (IsActive)
            {
                Engine.RecognizeAsyncStop();
                ResultList += "[Message] 認識終了";
            }
        }
    }
}
