using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ZreadingUWP.Model;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace ZreadingUWP.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ReadingPage : Page
    {
        public ReadingPage()
        {
            this.InitializeComponent();
        }
        #region CSS样式      
        string baseHtmlLight = @"<html><head><style>body {
    overflow-x: hidden;
   font-family: 'Helvetica Neue', Helvetica, Arial, Sans-serif;
 background-color:white;    
touch-action: pan-y;
-ms-content-zooming:none;


}
p{       
    text-align:left;
   color:black;   
    text-indent:2em;/*首行缩进两个单位*/  
    line-height:1.5em;/*行距为1.5个单位*/    
    word-break:break-all;
 overflow-wrap:break-word;   
}

.content{
     font-size:1.2em;/*字体大小*/
     /*padding:0.1em 0.15em 0em 0.15em;*/
     
}
img{
    margin:0px auto;
  height: auto; 
width: auto\9; 
width:100%; 
float:left;
      
   
}
ol, ul,li{
    font-size:1em;/*字体大小*/  
      line-height:1.5em;/*行距为1.5个单位*/ 
      color:gray;
     white-space:normal;
}
img, fieldset { 
border: 0; 
} 
embed{
    display:none;
}
.title {
    color:black;
    font-weight:bold;
    font-size:1.3em; 
    text-align:left;

}</style></head>

";
        string baseHtmlDark = @"<html><head><style>body {
    overflow-x: hidden;
   font-family: 'Helvetica Neue', Helvetica, Arial, Sans-serif;
 background-color:black;    
touch-action: pan-y;
-ms-content-zooming:none;


}
p{       
    text-align:left;
   color:white;   
    text-indent:2em;/*首行缩进两个单位*/  
    line-height:1.5em;/*行距为1.5个单位*/    
    word-break:break-all;
 overflow-wrap:break-word;   
}

.content{
     font-size:1.2em;/*字体大小*/
     /*padding:0.1em 0.15em 0em 0.15em;*/
     
}
img{
    margin:0px auto;
  height: auto; 
width: auto\9; 
width:100%; 
float:left;
      
   
}
ol, ul,li{
    font-size:1em;/*字体大小*/  
      line-height:1.5em;/*行距为1.5个单位*/ 
      color:white;
     white-space:normal;
}
img, fieldset { 
border: 0; 
} 
embed{
    display:none;
}
.title {
    color:white;
    font-weight:bold;
    font-size:1.3em; 
    text-align:left;

}</style></head>

";
        #endregion

        private string uri;
        string content = "";
        private string title = "";
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var _getread = (Zreading)e.Parameter;
            uri = _getread.Url;
            title = _getread.Title;

        }
        private void web_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            lr.Visibility = Visibility.Collapsed;
        }


        #region 查看文章
        private async void GetArticleContentAsync(string url)
        {
            //         try
            //         {
            //             HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //             request.Method = "GET";
            //             WebResponse response = await request.GetResponseAsync();
            //             using (Stream stream = response.GetResponseStream())
            //             using (StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8))
            //             {
            //                 string html = reader.ReadToEnd();
            //                 HtmlDocument doc = new HtmlDocument();
            //                 doc.LoadHtml(html);
            //                 HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//*[@class=\"entry-content\"]");

            //                 foreach (var p in node.Descendants("div").ToArray())
            //                     p.Remove();
            //                 foreach (var item in node)
            //                 {
            //                     content = item.InnerHtml;
            //                 }       

            //                     Windows.Storage.ApplicationDataContainer localSettings =
            //Windows.Storage.ApplicationData.Current.LocalSettings;
            //                 if (localSettings.Values["theme"] != null)
            //                 {
            //                     string s = localSettings.Values["theme"].ToString();
            //                     if (s == "light")
            //                     {
            //                         web.NavigateToString(baseHtmlLight + "<body><b class='title'>" + title + "</b><hr><div class='content'>" + content + "</div></body></html>");
            //                     }
            //                     else
            //                         web.NavigateToString(baseHtmlDark + "<body><b class='title'>" + title + "</b><hr><div class='content'>" + content + "</div></body></html>");
            //                 }                            

            //             }
            //         }
            //         catch (WebException e)
            //         {
            //             await new MessageDialog(e.Message).ShowAsync();
            //         }           
            string result = await HttpHelper.RequestAwait(url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);
            HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//*[@class=\"entry-content\"]");

            foreach (var p in node.Descendants("div").ToArray())
                p.Remove();
            foreach (var item in node)
            {
                content = item.InnerHtml;
            }

            Windows.Storage.ApplicationDataContainer localSettings =
Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values["theme"] != null)
            {
                string s = localSettings.Values["theme"].ToString();
                if (s == "light")
                {
                    web.NavigateToString(baseHtmlLight + "<body><b class='title'>" + title + "</b><hr><div class='content'>" + content + "</div></body></html>");
                }
                else
                    web.NavigateToString(baseHtmlDark + "<body><b class='title'>" + title + "</b><hr><div class='content'>" + content + "</div></body></html>");
            }
        }


        #endregion
        private void web_Loaded(object sender, RoutedEventArgs e)
        {
            GetArticleContentAsync(uri);
        }
        //回退按钮事件
        private void onclick(object sender, RoutedEventArgs e)
        {
            if (Frame == null)
            {
                return;
            }
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
        //刷新
        private void refreshme_Click(object sender, RoutedEventArgs e)
        {
            lr.Visibility = Visibility.Visible;

            GetArticleContentAsync(uri);
        }

        private async void lookme_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(uri));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //ChangeTheme.changeTheme(this.Frame);//夜间模式切换
        }
    }
}
