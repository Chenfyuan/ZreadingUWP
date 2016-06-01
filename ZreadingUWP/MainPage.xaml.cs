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
using ZreadingUWP.Views;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace ZreadingUWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings =
   Windows.Storage.ApplicationData.Current.LocalSettings;
        public MainPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New)
            {

                this.contentframe.Navigate(typeof(Views.HomePage));

            }

        }
        private void ShowSliptView(object sender, RoutedEventArgs e)
        {
            SamplesSplitView.IsPaneOpen = !SamplesSplitView.IsPaneOpen;
        }


        private void Root_Loaded(object sender, RoutedEventArgs e)
        {

            ChangeTheme.changeTheme(this.Frame);//夜间模式切换
        }

        private void setting_Tapped(object sender, TappedRoutedEventArgs e)
        {
            contentframe.Navigate(typeof(SettingPage));
            //((Frame)Window.Current.Content).Navigate(typeof(SettingPage));
        }

        private void myfavorite_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //((Frame)Window.Current.Content).Navigate(typeof(Views.MyFavorite));
            contentframe.Navigate(typeof(Views.MyFavorite));
        }

        private void hot_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //((Frame)Window.Current.Content).Navigate(typeof(Views.HotPage));
            this.contentframe.Navigate(typeof(Views.HotPage));
        }

        private void home_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.contentframe.Navigate(typeof(Views.HomePage));
        }

        private void classifytap_Tapped(object sender, TappedRoutedEventArgs e)
        {
            contentframe.Navigate(typeof(Views.ClassifyPage));
        }

    }
}
