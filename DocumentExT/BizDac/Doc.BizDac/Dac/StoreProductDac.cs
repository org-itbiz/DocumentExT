using Doc.Data;
using Doc.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.BizDac.Dac
{
    public class StoreProductDac
    {
        internal IList<StoreProductT> SelectAllStoreProducts()
        {
            using (AppDbContext ctx = new AppDbContext())
            {
                List<StoreProductT> products = ctx.StoreProduct.ToList();
                return products;
            }
        }
    }
}
