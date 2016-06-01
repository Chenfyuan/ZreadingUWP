using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.Xaml.Data;

namespace ZreadingUWP.Model
{
    public class ZreadingList : ObservableCollection<Zreading>, ISupportIncrementalLoading
    {
        private bool _busy = false;
        private bool _has_more_items = false;
        private int _current_page = 1;
        string url = string.Empty;
        public int TotalCount
        {
            get; set;
        }
       
        public void DoRefresh()
            {
            _current_page = 1;
            TotalCount = 0;
            Clear();
            HasMoreItems = true;
        }
        public bool HasMoreItems
        {
            get
            {
                if (_busy)
                    return false;
                else
                    return _has_more_items;
            }
            private set
            {
                _has_more_items = value;
            }
        }
        public ZreadingList( string url)
        {
            HasMoreItems = true;
            this.url = url;
        }
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return InnerLoadMoreItemsAsync(count).AsAsyncOperation();
        }

        private async Task<LoadMoreItemsResult> InnerLoadMoreItemsAsync(uint count)
        {
            _busy = true;
            var actualCount = 0;
            this.LoadMoreStarted?.Invoke(this, EventArgs.Empty);
           List<Zreading> list = null;
            try
            {
             list = await ZreadingService.GetArticleAsync(url, _current_page);
            }
            catch(Exception e)
            {
                await new MessageDialog(e.Message).ShowAsync();
                HasMoreItems = false;
            }
            if(list!=null&&list.Any())
            {
                actualCount = list.Count;
                TotalCount += actualCount;
                _current_page++;
                HasMoreItems = true;
                list.ForEach((c) => { this.Add(c); });
            }
            else
            {
                HasMoreItems = false;
            }
            _busy = false;
            this.LoadMoreEnd?.Invoke(this, EventArgs.Empty);
            return new LoadMoreItemsResult
            {
                Count = (uint)actualCount
            };
        }
        #region 公共事件
        /// <summary>
        /// 该事件在开始加载时发生。
        /// </summary>
        public event EventHandler LoadMoreStarted;
        /// <summary>
        /// 该事件在加载完成后发生。
        /// </summary>
        public event EventHandler LoadMoreEnd;
        #endregion
    }
}
