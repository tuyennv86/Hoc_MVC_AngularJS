using System.Collections.Generic;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        IEnumerable<Tag> GetAllByProductId(int Id);
    }

    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Tag> GetAllByProductId(int Id)
        {
            var query = from t in DbContext.Tags
                        join tp in DbContext.ProductTags
                        on t.ID equals tp.TagID
                        where tp.ProductID == Id                       
                        select t;
            return query;
        }
    }
}