﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Ldrwp {
    public class ItemViewModel : INotifyPropertyChanged {
        private string _lineOne;
        /// <summary>
        /// Sample ViewModel プロパティ。このプロパティは、バインドを使用してプロパティ値を表示するビューで使用されます。
        /// </summary>
        /// <returns></returns>
        public string LineOne {
            get {
                return _lineOne;
            }
            set {
                if (value != _lineOne) {
                    _lineOne = value;
                    NotifyPropertyChanged("LineOne");
                }
            }
        }

        private string _lineTwo;
        /// <summary>
        /// Sample ViewModel プロパティ。このプロパティは、バインドを使用してプロパティ値を表示するビューで使用されます。
        /// </summary>
        /// <returns></returns>
        public string LineTwo {
            get {
                return _lineTwo;
            }
            set {
                if (value != _lineTwo) {
                    _lineTwo = value;
                    NotifyPropertyChanged("LineTwo");
                }
            }
        }

        private string _lineThree;
        /// <summary>
        /// Sample ViewModel プロパティ。このプロパティは、バインドを使用してプロパティ値を表示するビューで使用されます。
        /// </summary>
        /// <returns></returns>
        public string LineThree {
            get {
                return _lineThree;
            }
            set {
                if (value != _lineThree) {
                    _lineThree = value;
                    NotifyPropertyChanged("LineThree");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}