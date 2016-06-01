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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace ZreadingUWP.ClassifyViews
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PassToSuPage : Page
    {
        ZreadingList _zreading_list;
        public PassToSuPage()
        {
            _zreading_list = new ZreadingList("http://www.zreading.cn/archives/category/success/page/");
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            listview.ItemsSource = _zreading_list;
            _zreading_list.LoadMoreStarted += _zreading_list_LoadMoreStarted;
            _zreading_list.LoadMoreEnd += _zreading_list_LoadMoreEnd;
        }

        private void _zreading_list_LoadMoreEnd(object sender, EventArgs e)
        {
            loading.Visibility = Visibility.Collapsed;
        }

        private void _zreading_list_LoadMoreStarted(object sender, EventArgs e)
        {
            loading.Visibility = Visibility.Visible;
        }

        private void listview_ItemClick(object sender, ItemClickEventArgs e)
        {
            Zreading _zread = e.ClickedItem as Zreading;
            if (_zread != null && _zread is Zreading)
            {



                ((Frame)Window.Current.Content).Navigate(typeof(ReadingPage), _zread);




            }
        }
    }
}
