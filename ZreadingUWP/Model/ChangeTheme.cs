using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ZreadingUWP.Model
{
  public  static class ChangeTheme
    {
        public static void changeTheme(Frame fs)
        {
            Windows.Storage.ApplicationDataContainer localSettings =
    Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values["theme"] != null)
            {
                string s = localSettings.Values["theme"].ToString();
                if (s == "light")
                {
                   fs.RequestedTheme = ElementTheme.Light;
                }
                else
                   fs.RequestedTheme = ElementTheme.Dark;

            }
        }
    }
}
