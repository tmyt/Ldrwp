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
    public partial class Setting : PhoneApplicationPage {
        public Setting() {
            InitializeComponent();
            DataContext = SettingUtil.GetInstance();
        }

        private void ApplicationBarIconSaveButton_Click(object sender, EventArgs e) {
            this.Focus();
            if (String.IsNullOrEmpty(SettingUtil.GetInstance().UserName) ||
                String.IsNullOrEmpty(SettingUtil.GetInstance().Password)) {
                    ShowErrorDialog();
                    return;
            }
            SettingUtil.GetInstance().Save();
            NavigationService.GoBack();
            //ここではログインせずに情報の保存だけを行う。
            //LivedoorReaderModel lrm = new LivedoorReaderModel();

            //lrm.Login(
            //    () => {
            //        MessageBox.Show("ログインできました。");
            //        SettingUtil.GetInstance().Save();
            //        NavigationService.GoBack();
            //    },
            //    () => {
            //        MessageBox.Show("正しい情報を入力してください。");
            //    }
            //    );
        }

        private void ShowErrorDialog() {
            MessageBox.Show("正しい情報を入力してください。");
        }
    }
}