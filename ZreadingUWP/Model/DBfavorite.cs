

using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZreadingUWP.Model
{
   public  class DBfavorite
    {
        [PrimaryKey]// 主键。
        [AutoIncrement]// 自动增长。
        public int id
        {
            get;
            set;
        }

        [MaxLength(20)]// 对应到数据库 varchar 的大小。
        public string url
        {
            get;
            set;
        }
        public string title
        {
            get;
            set;
        }
    }
}
