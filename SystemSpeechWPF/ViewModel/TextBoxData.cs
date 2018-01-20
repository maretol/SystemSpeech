using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SystemSpeechWPF.Model;
using Prism.Commands;
using Prism.Mvvm;

namespace SystemSpeechWPF.ViewModel
{
    class TextBoxData : BindableBase
    {
        /// <summary>
        /// 認識開始ボタンのコマンド
        /// </summary>
        public DelegateCommand RecognitionCommand { get; private set; }

        /// <summary>
        /// 認識エンジン
        /// </summary>
        private RecognizeEngine _RecognizeEngine;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TextBoxData()
        {
            _RecognizeEngine = new RecognizeEngine();
            // RecognizeEngineの変更イベントを受け取るやつを追加
            RecognitionCommand = new DelegateCommand(this.RecognitionExecute, this.CanRecognitionExecute);
        }

        /// <summary>
        /// 登録コマンドもTextBox
        /// </summary>
        private string commandList;
        public string CommandList
        {
            get { return commandList; }
            set { SetProperty(ref commandList, value); }
        }

        /// <summary>
        /// 認識結果のTextBox
        /// </summary>
        private string result;
        public string Result
        {
            get { return result; }
            set { SetProperty(ref result, value); }
        }

        /// <summary>
        /// ボタンに表示する開始・停止
        /// </summary>
        public string ButtonStatus
        {
            get { return (_RecognizeEngine.IsActive) ? "音声認識停止" : "音声認識開始"; }
        }

        /// <summary>
        /// 認識開始ボタンの実処理
        /// </summary>
        private void RecognitionExecute()
        {
            /// 辞書データを渡して開始処理
            _RecognizeEngine.CommandList = CommandList;
            _RecognizeEngine.StartOrEndRecognize();
        }

        /// <summary>
        /// 認識開始ボタンの可不可
        /// </summary>
        /// <returns></returns>
        private bool CanRecognitionExecute()
        {
            return _RecognizeEngine != null && string.IsNullOrWhiteSpace(CommandList);
        }
    }
}
