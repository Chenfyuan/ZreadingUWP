using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ZreadingUWP.Model
{
  public class Zreading
    {
       
        public string Title
        {
            get; set;
        }//文章标题
        public string Views
        {
            get; set;
        }//阅读量
        public string Label
        {
            get; set;
        }//标签
        public string AuthorName
        {
            get; set;
        }//作者名
        public string Summary
        {
            get; set;
        }//文章概括
        public string PublishTime
        {
            get; set;
        }//发布时间
        public string Url
        {
            get; set;
        }//文章链接
        private ImageSource expicon;

        //public ImageSource ExpIcon
        //{
        //    get { return expicon; }
        //    set {
        //        if(value!=expicon)
        //        expicon = value;
        //        OnPropertyChanged("ExpIcon");

        //    }
        //}
        public ImageSource ExpIcon { get; set; }


      

       
    }
}
