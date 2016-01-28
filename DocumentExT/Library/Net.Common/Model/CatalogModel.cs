using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Common.Model
{
    public class CatalogModel
    {
        public string CatalogNo { get; set; }
        public string CatalogName { get; set; }
    }

    public static class CatalogManager
    {
        //这里只显示创建了三个分类作为示例，实际中AllCategories可以从数据源读取。
        public static readonly List<CatalogModel> AllCategories = new List<CatalogModel>
        {
            new CatalogModel(){ CatalogNo="001", CatalogName="Nokia"},
            new CatalogModel(){ CatalogNo="002", CatalogName="iPhone"},
            new CatalogModel(){ CatalogNo="003", CatalogName="Anycall"}
        };
    }
}
