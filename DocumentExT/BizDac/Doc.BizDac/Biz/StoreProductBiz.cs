using Doc.BizDac.Dac;
using Doc.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.BizDac.Biz
{
    public class StoreProductBiz
    {
        public IList<StoreProductT> GetAllStoreProducts()
        {
            return new StoreProductDac().SelectAllStoreProducts();
        }
    }
}
