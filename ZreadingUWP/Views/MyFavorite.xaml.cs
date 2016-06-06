using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class MyFavorite : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings =
  Windows.Storage.ApplicationData.Current.LocalSettings;
        public MyFavorite()
        {
            this.InitializeComponent();
        }

        ObservableCollection<Zreading> _list = new ObservableCollection<Zreading>();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            using (var conn = AppDatabase.GetDbConnection())
            {
                var myfav = conn.Table<DBfavorite>();
                foreach (var item in myfav)
                {
                    _list.Add(new Zreading
                    {
                        Title = item.title,
                        Url = item.url

                    });
                }
                listview.ItemsSource = _list;

            }

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

        private void listview_ItemClick(object sender, ItemClickEventArgs e)
        {
            Zreading art = e.ClickedItem as Zreading;
            if (art != null && art is Zreading)
            {
                this.Frame.Navigate(typeof(ReadingPage), art);
            }
        }

        private async void mydel_Click(object sender, RoutedEventArgs e)
        {

            using (var conn = AppDatabase.GetDbConnection())
            {

                conn.DeleteAll<DBfavorite>();

            }

            _list.Clear();
            listview.ItemsSource = _list;
            await new MessageDialog("删除成功").ShowAsync();

        }

        private void selectnewsbtn_Click(object sender, RoutedEventArgs e)
        {
            if (listview.Items.Count > 0)
            {
                listview.SelectionMode = ListViewSelectionMode.Multiple;
                listview.IsItemClickEnabled = false;
                del.Visibility = Visibility.Visible;
                selectnewsbtn.Visibility = Visibility.Collapsed;
                mycancel.Visibility = Visibility.Visible;
                mydel.Visibility = Visibility.Collapsed;


            }
        }

        private void mycancel_Click(object sender, RoutedEventArgs e)
        {
            listview.SelectionMode = ListViewSelectionMode.None;
            listview.IsItemClickEnabled = true;
            selectnewsbtn.Visibility = Visibility.Visible;
            mydel.Visibility = Visibility.Visible;
            mycancel.Visibility = Visibility.Collapsed;
            del.Visibility = Visibility.Collapsed;


        }

        private void find_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
         //   var bb = args.QueryText;
         //此方法不实现

        }

        private void find_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            //此处实现搜索建议功能
            var autosuggesbox = sender;
            var _aulist = _list.Where(k => k.Title.StartsWith(autosuggesbox.Text.Trim().ToString()));
            autosuggesbox.ItemsSource = _aulist;



        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //ChangeTheme.changeTheme(this.Frame);//夜间模式切换
        }

        private void find_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var tt = args.SelectedItem as Zreading;
            if (tt != null && tt is Zreading)
            {
                this.Frame.Navigate(typeof(ReadingPage), tt);
            }
        }
    }
}
