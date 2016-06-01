using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class HomePage : Page
    {
        ZreadingList _zreading_list;
        public HomePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New)
            {

                listview.ItemsSource = _zreading_list = new ZreadingList("http://www.zreading.cn/page/");

                _zreading_list.LoadMoreStarted += _zreading_list_LoadMoreStarted;
                _zreading_list.LoadMoreEnd += _zreading_list_LoadMoreEnd;
            }
        }

        private void _zreading_list_LoadMoreEnd(object sender, EventArgs e)
        {
            this.loading.Visibility = Visibility.Collapsed;
            loadingpr.Visibility = Visibility.Collapsed;
        }

        private void _zreading_list_LoadMoreStarted(object sender, EventArgs e)
        {
            this.loading.Visibility = Visibility.Visible;
        }

        private void listview_ItemClick(object sender, ItemClickEventArgs e)
        {
            Zreading _zread = e.ClickedItem as Zreading;
            if (_zread != null && _zread is Zreading)
            {




                this.Frame.Navigate(typeof(ReadingPage), _zread);

            }
        }

        private void refreshbtn_Click(object sender, RoutedEventArgs e)
        {
            _zreading_list.DoRefresh();
        }





        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ClassifyViews.PassToSuPage));
        }

        private void selectbtn_Click(object sender, RoutedEventArgs e)
        {
            //点击选择文章按钮
            if (listview.Items.Count > 0)
            {
                listview.SelectionMode = ListViewSelectionMode.Multiple;
                listview.IsItemClickEnabled = false;

                favoritebtn.Visibility = Visibility.Visible;
                selectbtn.Visibility = Visibility.Collapsed;
                cancelbtn.Visibility = Visibility.Visible;
                refreshbtn.Visibility = Visibility.Collapsed;

            }
        }

        private async void favoritebtn_Click(object sender, RoutedEventArgs e)
        {
            //收藏
            if (listview.SelectedIndex != -1)
            {
                using (var con = AppDatabase.GetDbConnection())
                {
                    DBfavorite myfav = null;

                    foreach (Zreading item in listview.SelectedItems)
                    {
                        myfav = new DBfavorite
                        {
                            title = item.Title,
                            url = item.Url

                        };
                        con.Insert(myfav);
                    }

                    await new MessageDialog("收藏成功").ShowAsync();
                    listview.SelectionMode = ListViewSelectionMode.None;
                    listview.IsItemClickEnabled = true;
                    favoritebtn.Visibility = Visibility.Collapsed;
                    selectbtn.Visibility = Visibility.Visible;
                    cancelbtn.Visibility = Visibility.Collapsed;
                    refreshbtn.Visibility = Visibility.Visible;
                }
            }
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            listview.SelectionMode = ListViewSelectionMode.None;
            listview.IsItemClickEnabled = true;
            favoritebtn.Visibility = Visibility.Collapsed;
            selectbtn.Visibility = Visibility.Visible;

            cancelbtn.Visibility = Visibility.Collapsed;
            refreshbtn.Visibility = Visibility.Visible;
        }

        private void settingbtn_Click(object sender, RoutedEventArgs e)
        {
            // ((Frame)Window.Current.Content).Navigate(typeof(Views.SettingPage));
            Frame.Navigate(typeof(SettingPage));
        }

        private void fav_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.MyFavorite));
        }

        private void hotart_Click(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(Views.HotPage));
        }

        private void homepa_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.HomePage));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //ChangeTheme.changeTheme(this.Frame);//夜间模式切换
        }

        private void totopbtn_Click(object sender, RoutedEventArgs e)
        {


        }

        private void classify_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.ClassifyPage));
        }
    }
}
