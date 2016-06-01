using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections.ObjectModel;
namespace ZreadingUWP.Model
{
  public  class ZreadingService
    {
       

        /// <summary>
        /// 获取文章数据
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public static async Task<List<Zreading>> GetArticleAsync(string Url,int _pageindex)
        {
          
            string result = await HttpHelper.RequestAwait(Url,_pageindex);
            List<Zreading> list_zreadings = new List<Zreading>();
          HtmlDocument doc = new HtmlDocument();
                     doc.LoadHtml(result);

                   HtmlNode hNode = doc.GetElementbyId("content");//查找元素                                                    
                    foreach (HtmlNode child in hNode.ChildNodes)
                      {
                        Zreading _zread = new Zreading();
                        if (child.Attributes["class"] == null ||
                           child.Attributes["class"].Value != "entry-common clearfix")
                              continue;
                        HtmlNode hn1 = HtmlNode.CreateNode(child.OuterHtml);
                         _zread.Title = hn1.SelectSingleNode
                            ("//*[@class='entry-name']/a").Attributes["title"].Value;//标题
                        _zread.AuthorName = "作者:" + hn1.SelectSingleNode("//*[@itemprop='author']").InnerText;//
                         _zread.Label = hn1.SelectSingleNode("//*[@class='entry-meta']/a").InnerText;//
                          _zread.PublishTime = "发布时间:" + hn1.SelectSingleNode("//*[@itemprop='datePublished']").InnerText;
                          _zread.Views = hn1.SelectSingleNode("//*[@itemprop='interactionCount']").InnerText;
                         _zread.Url = hn1.SelectSingleNode
                             ("//*[@class='entry-name']/a").Attributes["href"].Value;//超链接

                       list_zreadings.Add(_zread);  
                     }
            return list_zreadings;

        }

       

        }
    
}
