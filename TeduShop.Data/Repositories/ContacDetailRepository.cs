using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IContacDetailRepository : IRepository<ContacDetail>
    {

    }
    public class ContacDetailRepository : RepositoryBase<ContacDetail>, IContacDetailRepository
    {
        public ContacDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
