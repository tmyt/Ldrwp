using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Microsoft.Phone.Reactive;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO.IsolatedStorage;
namespace Ldrwp.Models {
    public class LivedoorReaderModel {
        string ApiUrlBase = "http://reader.livedoor.com/api/";
        string FeedListApi = "subs";
        string LoginUrl = "https://member.livedoor.com/login/index";
        private CookieContainer _cookie;
        private const string CookieKey = "cookie";
        private const string ReaderSidKey = "reader_sid";
        private string _readerSid = "";
        Regex TokenPattern = new Regex(@"<input type=""hidden"" name=""_token"" value=""(?<token>.*)"" />");
        Regex FailedPattern = new Regex(@"正しく入力されていません");
        public LivedoorReaderModel() {
            var s = IsolatedStorageSettings.ApplicationSettings;
            if (!s.TryGetValue<CookieContainer>(CookieKey, out _cookie)) {
                _cookie = new CookieContainer();
            }
            if(!s.TryGetValue<string>(ReaderSidKey, out _readerSid)){
                _readerSid = "";
            }
        }
        private void SaveCookie() {
            var s = IsolatedStorageSettings.ApplicationSettings;
            s[CookieKey] = _cookie;
            s[ReaderSidKey] = _readerSid;
            s.Save();
        }
        #region ログイン
        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="SuccessAction">成功時</param>
        /// <param name="FailedAction">失敗時</param>
        public void Login() {

            _cookie = new CookieContainer();
            return CreateHttpWebRequestWithCookie(LoginUrl)
               .DownloadStringAsync()
               .SelectMany((s) => {
                   //tokenの抽出
                   var m = TokenPattern.Match(s);

                   //パラメータの作成
                   var dic = new Dictionary<string, string>();
                   dic["livedoor_id"] = SettingUtil.GetInstance().UserName ?? "";
                   dic["password"] = SettingUtil.GetInstance().Password ?? "";
                   dic["auto_login"] = "1";
                   dic["_token"] = m.Groups["token"].Value;
                   dic[".sv"] = "";
                   dic[".next"] = "";
                   dic["x"] = "100";
                   dic["y"] = "19";

                   var req = CreateHttpWebRequestWithCookie(LoginUrl);
                   req.Method = "post";
                   req.ContentType = "application/x-www-form-urlencoded";
                   return req.UploadValuesAsync(dic);
               })
               .SelectMany((s) => {
                   return s.DownloadStringAsync();
               })
               .SelectMany((s) => {
                   //本文から認証成功かどうか確認する。
                   if (FailedPattern.IsMatch(s)) {
                       return Observable.Empty<WebResponse>();
                   }
                   var readerUri = "http://reader.livedoor.com/";
                   var req = CreateHttpWebRequestWithCookie(readerUri);
                   return req.GetResponseAsObservable();
               }).Take(1)
               .SelectMany((s) => {
                   var headers = s.Headers["Set-Cookie"].Split(';');
                   foreach (var h in headers) {
                       var hkv = h.Split('=');
                       if (hkv[0] == "reader_sid") {
                           _readerSid = hkv[1];
                           break;
                       }
                   }
                   SaveCookie();
                   //return Observable.Take(1);
               });
               //.ObserveOnDispatcher()
               //.Catch((WebException e) => {
               //      //FailedAction();
               //    return Observable.Empty<WebResponse>();
               //})
               //.Subscribe((s) => {
               //      try {
               //          var headers = s.Headers["Set-Cookie"].Split(';');
               //          foreach (var h in headers) {
               //              var hkv = h.Split('=');
               //              if (hkv[0] == "reader_sid") {
               //                  _readerSid = hkv[1];
               //                  break;
               //              }
               //          }
               //          SaveCookie();
               //      } catch {
               //      }
               //});

        }
        #endregion 
        public void GetFeedTest(){
            Login();
            string param = "?unread=0&ApiKey="+_readerSid;
            var c = CreateHttpWebRequestWithCookie(Path.Combine(ApiUrlBase, FeedListApi)+param);
            c.DownloadStringAsync()
                .Subscribe((s) => {
                    var test = s;
                });
        }
        private HttpWebRequest CreateHttpWebRequestWithCookie(string url) {
            var req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.CookieContainer = _cookie;
            return req;
        }
        
    }
}
