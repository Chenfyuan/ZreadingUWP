using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class HotPage : Page
    {
        public static ObservableCollection<Zreading> ls = new ObservableCollection<Zreading>();


        Windows.Storage.ApplicationDataContainer localSettings =
   Windows.Storage.ApplicationData.Current.LocalSettings;
        public HotPage()
        {
            this.InitializeComponent();
            listview.ItemsSource = ls;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New)
                GetHotArticle("http://widget.wumii.cn/ext/widget/hot?prefix=http%3A%2F%2Fwww.zreading.cn%2F&num=10&t=1", "//*[@class='itemTitle']/a");
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            tbk.Text = "今日热门";
            GetHotArticle("http://widget.wumii.cn/ext/widget/hot?prefix=http%3A%2F%2Fwww.zreading.cn%2F&num=10&t=1", "//*[@class='itemTitle']/a");

        }

        private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            tbk.Text = "近100天文章排行";
            GetHotArticle("http://www.zreading.cn/%E7%83%AD%E6%96%87", "//*[@id='427']/div/ol[1]/li/a");

        }
        //
        private void MenuFlyoutItem_Click_2(object sender, RoutedEventArgs e)
        {
            tbk.Text = "总的文章排行";
            GetHotArticle("http://www.zreading.cn/%E7%83%AD%E6%96%87", "//*[@id='427']/div/ol[2]/li/a");
        }
        #region 获取热文方法体
        /// <summary>
        /// 获取热文页面
        /// </summary>
        private async void GetHotArticle(string url, string xpath)
        {
            string result = await HttpHelper.RequestAwait(url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);
            //遍历输出文章标题
            HtmlNodeCollection node = doc.DocumentNode.SelectNodes(xpath);
            ls.Clear();
            foreach (var item in node)
            {
                Zreading _zread = new Zreading();

                _zread.Title = item.InnerText;
                _zread.Url = item.Attributes["href"].Value;
                ls.Add(_zread);
            }
            listview.ItemsSource = ls;
            loading.Visibility = Visibility.Collapsed;
        }
        #endregion

        private void listview_ItemClick(object sender, ItemClickEventArgs e)
        {
            Zreading art = e.ClickedItem as Zreading;
            if (art != null && art is Zreading)
            {

                Frame.Navigate(typeof(ReadingPage), art);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //if (localSettings.Values["theme"] != null)
            //{
            //    string s = localSettings.Values["theme"].ToString();
            //    if (s == "light")
            //    {
            //        RequestedTheme = ElementTheme.Light;
            //    }
            //    else
            //        RequestedTheme = ElementTheme.Dark;

            //}
        }

        private void fav_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MyFavorite));
        }

        private void settingbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingPage));
        }

        private void homepage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomePage));
        }

        private void hotpage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HotPage));
        }

        private void classify_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ClassifyPage));
        }


    }
}
