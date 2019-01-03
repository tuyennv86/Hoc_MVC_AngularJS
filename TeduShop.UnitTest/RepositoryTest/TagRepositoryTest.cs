using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.UnitTest.RepositoryTest
{
    [TestClass]
    public class TagRepositoryTest
    {
        IDbFactory dbFactory;
        TagRepository objRepository;
        IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new TagRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }
       
        [TestMethod]
        public void Tag_Repository_GetAllByProductID()
        {
            var list = objRepository.GetAllByProductId(1).ToList();
            Assert.AreEqual(2, list.Count);
        }

    }
}