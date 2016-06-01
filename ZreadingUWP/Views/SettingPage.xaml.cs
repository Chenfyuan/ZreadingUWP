using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ZreadingUWP.Theme;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace ZreadingUWP.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            List<Themes> _colorlist = new List<Themes>();

            this.InitializeComponent();
            _colorlist.Add(new Themes { Colorc = "pink", ColorName = "少女粉" });
            _colorlist.Add(new Themes { Colorc = "red", ColorName = "姨妈红" });
            _colorlist.Add(new Themes { Colorc = "yellow", ColorName = "咸蛋黄" });
            _colorlist.Add(new Themes { Colorc = "green", ColorName = "早苗绿" });
            _colorlist.Add(new Themes { Colorc = "blue", ColorName = "胖次蓝" });
            _colorlist.Add(new Themes { Colorc = "purple", ColorName = "基佬紫" });
            this.InitializeComponent();
            if (localSettings.Values["theme"] != null)
            {
                string s = localSettings.Values["theme"].ToString();
                if (s == "light")
                {
                    tog.IsOn = false;
                    // RequestedTheme = ElementTheme.Light;
                }
                else
                {
                    tog.IsOn = true;
                    // RequestedTheme = ElementTheme.Dark;
                }
            }
            selectcolor.ItemsSource = _colorlist;
        }
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
        Windows.Storage.ApplicationDataContainer localSettings =
    Windows.Storage.ApplicationData.Current.LocalSettings;
        private void tog_Toggled(object sender, RoutedEventArgs e)
        {
            var toggle = sender as ToggleSwitch;
            if (toggle.IsOn)
            {

                localSettings.Values["theme"] = "dark";
                // RequestedTheme = ElementTheme.Dark;
            }
            else
            {
                localSettings.Values["theme"] = "light";
                // RequestedTheme = ElementTheme.Light;

            }

        }

        private void selectcolor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (selectcolor.SelectedIndex)
            {
                case 0:
                   // panec.Background = new SolidColorBrush(Colors.Pink);
                    break;
                case 1:
                   // panec.Background = new SolidColorBrush(Colors.Red);
                    break;

            }


        }
    }
}
