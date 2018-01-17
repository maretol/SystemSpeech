using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SystemSpeechWPF.Engine;
using Prism.Commands;

namespace SystemSpeechWPF.Content
{
    class TextBoxData : INotifyPropertyChanged
    {
        /// <summary>
        /// 認識開始ボタンのコマンド
        /// </summary>
        public DelegateCommand RecognitionCommand { get; private set; }

        /// <summary>
        /// 認識エンジン
        /// </summary>
        private RecognizeEngine RecognizeEngine;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TextBoxData()
        {
            RecognitionCommand = new DelegateCommand(this.RecognitionExecute, this.CanRecognitionExecute);
        }

        /// <summary>
        /// INotifyPropertyChanged のイベント
        /// </summary>
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
        /// 認識開始ボタンの実処理
        /// </summary>
        private void RecognitionExecute()
        {
            return;
        }

        /// <summary>
        /// 認識開始ボタンの可不可
        /// </summary>
        /// <returns></returns>
        private bool CanRecognitionExecute()
        {
            return false;
        }
        
    }
}
