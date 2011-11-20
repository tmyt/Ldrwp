using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using System.ComponentModel;

namespace Ldrwp.Models {
    /// <summary>
    /// singletonにするのは、staticクラスだとバインディングできないため。
    /// </summary>
    public class SettingUtil : INotifyPropertyChanged {
        private string _userName = "";
        public string UserName {
            get { return _userName; }
            set {
                if (value != _userName) {
                    _userName = value;
                    NotifyPropertyChanged("UserName");
                }
            }
        }


        private string _password = "";
        public string Password {
            get { return _password; }
            set {
                if (value != _password) {
                    _password = value;
                    NotifyPropertyChanged("Password");
                }
            }
        }
        private const string LoginIdKey = "login_id";
        private const string PasswordKey = "password";
        static SettingUtil _this = new SettingUtil();
        static public SettingUtil GetInstance(){
            return _this;
        }
        private SettingUtil() {
            Load();
        }
        public void Load() { 
            //strageからデータ読み込む。
            var storage = GetMyStorageSetting();
            storage.TryGetValue(LoginIdKey, out _userName);
            storage.TryGetValue(PasswordKey, out _password);
            //テスト用
            UserName = "aaaa";
            Password = "aaaa";
        }
        public void Save() {
            var storage = GetMyStorageSetting();
            storage[LoginIdKey] = UserName;
            storage[PasswordKey] = Password;

            storage.Save();
        }

        private static IsolatedStorageSettings GetMyStorageSetting() {
            return IsolatedStorageSettings.ApplicationSettings;
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
