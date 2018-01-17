﻿using System;
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
        public DelegateCommand StartRecognize { get; private set; }

        private RecognizeEngine RecognizeEngine;

        public TextBoxData()
        {
            StartRecognize = new DelegateCommand(this.ConvertExecute, this.CanConvertExecute);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string commandList;
        public string CommandList
        {
            get { return commandList; }
            set { SetProperty(ref commandList, value); }
        }

        private string result;
        public string Result
        {
            get { return result; }
            set { SetProperty(ref result, value); }
        }


        private void ConvertExecute()
        {
            return;
        }

        private bool CanConvertExecute()
        {
            return false;
        }
        
    }
}