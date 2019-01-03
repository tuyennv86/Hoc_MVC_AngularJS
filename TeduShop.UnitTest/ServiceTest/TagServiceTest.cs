using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using TeduShop.Service;

namespace TeduShop.UnitTest.ServiceTest
{
    [TestClass]
    public class TagServiceTest
    {
        private Mock<ITagRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private ITagService _tagService;        

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<ITagRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _tagService = new TagService(_mockRepository.Object, _mockUnitOfWork.Object);           
        }

        [TestMethod]
        public void Tag_Service_GetAllByProductId()
        {
            //call action
            var result = _tagService.GetAllByProductId(1);

            //compare
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

    }
}
