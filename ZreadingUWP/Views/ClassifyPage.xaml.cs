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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace ZreadingUWP.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ClassifyPage : Page
    {
        public ClassifyPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New)
            {
                classifyframe.Navigate(typeof(ClassifyViews.PassToSuPage));
            }
        }

        private void passtosubtn_Click(object sender, RoutedEventArgs e)
        {
            classifyframe.Navigate(typeof(ClassifyViews.PassToSuPage));
        }

        private void creloadbtn_Click(object sender, RoutedEventArgs e)
        {
            classifyframe.Navigate(typeof(ClassifyViews.CreLoadPage));
        }

        private void homepa_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.HomePage));
        }

        private void hotart_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.HotPage));
        }

        private void classify_Click(object sender, RoutedEventArgs e)
        {

        }

        private void fav_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.MyFavorite));
        }

        private void settingbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.SettingPage));
        }

        private void totopbtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
