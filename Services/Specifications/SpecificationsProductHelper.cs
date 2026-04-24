using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal static class SpecificationsProductHelper
    {
        public static Expression<Func<Product, bool>> GetProductCriteria(ProductQueryParams queryParams)
        {
            return p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId.Value)
                && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId.Value)
                && (string.IsNullOrEmpty(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower()));
        }

    }
}
