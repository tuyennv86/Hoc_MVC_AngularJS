using System;
using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;
using System.Linq;

namespace TeduShop.Data.Repositories
{ 
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow);
       
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Product> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow)
        {
            var query = from p in DbContext.Products
                        join pt in DbContext.ProductTags
                        on p.ID equals pt.ProductID
                        where pt.TagID == tag && p.Status
                        orderby p.CreatedDate descending
                        select p;
            
            totalRow = query.Count();

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return query;
        }
    }
}