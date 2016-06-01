using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZreadingUWP.Model
{
 public  class HttpHelper
    {    
         public static Cookie ck { get; set; }
        public static string error_msg = string.Empty;
        public static int i = 2;
        public async static Task<string> RequestAwait(string url, int _pageindex, bool is_getcookit = false, bool is_dairucookie = false)
        {
            string json = string.Empty;
            HttpWebResponse response = null;
            try
            {
                HttpWebRequest request = WebRequest.Create(url+_pageindex) as HttpWebRequest;           
                if (is_dairucookie && ck != null)
                {
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(new Uri(url+_pageindex, UriKind.Absolute), ck);
                }

                response = (HttpWebResponse)await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, TaskCreationOptions.None);               
                using (Stream stream = response.GetResponseStream())
                {                  
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        json = await reader.ReadToEndAsync();
                    }
                  
                }

                if (response != null)
                {
                    response.Dispose();
                }
            }
            catch (WebException)
            {
                error_msg = "请检查您的网络连接";
            }
            catch (Exception ex)
            {
                error_msg = "其他错误" + ex.Message;
                json = null;
            }

            if (string.IsNullOrEmpty(json) && i > 0)
            {
                i--;
                json = await RequestAwait(url,_pageindex, is_getcookit, is_dairucookie);
                if (!string.IsNullOrEmpty(error_msg))
                {
                    //CommonHelper.ShowMsg(error_msg);
                }
            }
            i = 2;
          
            return json;
        }
        public async static Task<string> RequestAwait(string url,  bool is_getcookit = false, bool is_dairucookie = false)
        {
            string json = string.Empty;
            HttpWebResponse response = null;
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                if (is_dairucookie && ck != null)
                {
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(new Uri(url, UriKind.Absolute), ck);
                }

                response = (HttpWebResponse)await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, TaskCreationOptions.None);
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        json = await reader.ReadToEndAsync();
                    }

                }

                if (response != null)
                {
                    response.Dispose();
                }
            }
            catch (WebException)
            {
                error_msg = "请检查您的网络连接";
            }
            catch (Exception ex)
            {
                error_msg = "其他错误" + ex.Message;
                json = null;
            }

            if (string.IsNullOrEmpty(json) && i > 0)
            {
                i--;
                json = await RequestAwait(url, is_getcookit, is_dairucookie);
                if (!string.IsNullOrEmpty(error_msg))
                {
                    //CommonHelper.ShowMsg(error_msg);
                }
            }
            i = 2;

            return json;
        }
        public async static Task<string> PostAwait(string url, string postData)
        {
            string json = string.Empty;
            HttpWebResponse response = null;
            try
            {
                HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(url));
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                //返回应答请求异步操作的状态
                using (Stream stream = await Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, TaskCreationOptions.None))
                {
                    byte[] bs = System.Text.Encoding.UTF8.GetBytes(postData);
                    stream.Write(bs, 0, bs.Length);
                }
                response = (HttpWebResponse)await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, TaskCreationOptions.None);
                using (var s = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(s))
                    {
                        json = await sr.ReadToEndAsync();
                    }
                }
            }
            catch (Exception ex) { }
            if (response != null)
            {
                response.Dispose();
            }
            return json;
        }

    }
}
