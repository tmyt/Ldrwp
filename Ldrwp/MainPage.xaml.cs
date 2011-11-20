using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Ldrwp.Models;

namespace Ldrwp {
    public partial class MainPage : PhoneApplicationPage {
        // コンストラクター
        public MainPage() {
            InitializeComponent();

            // ListBox コントロールのデータ コンテキストをサンプル データに設定します
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            //ユーザ名とパスワードが空の場合は入力させる。
            if (String.IsNullOrEmpty(SettingUtil.GetInstance().UserName) || String.IsNullOrEmpty(SettingUtil.GetInstance().Password)) {
                NavigationService.Navigate(new Uri("/Setting.xaml",UriKind.Relative));
                return;
            }
            var lrm = new LivedoorReaderModel();
            lrm.GetFeedTest();
        }

        // ViewModel Items のデータを読み込みます
        private void MainPage_Loaded(object sender, RoutedEventArgs e) {
            if (!App.ViewModel.IsDataLoaded) {
                App.ViewModel.LoadData();
            }

        }

        private void ApplicationBarMenuItemSetting_Click(object sender, EventArgs e) {
            //設定画面開く
            NavigationService.Navigate("/Setting.xaml");

        }
    }
}