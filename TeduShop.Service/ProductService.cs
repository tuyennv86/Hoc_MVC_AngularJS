using System;
using System.Collections.Generic;
using System.Linq;
using TeduShop.Common;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IProductService
    {
        Product Add(Product product);

        void Update(Product product);

        void Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(string keyword);

        IEnumerable<Product> GetHome(bool HomeFlag);

        IEnumerable<Product> GetHot(bool HotFlag);

        IEnumerable<Product> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<Product> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow);

        IEnumerable<Product> GetListProductByCategoryPageing(int categoryId, int PageIndex, int pageSize, out int totalRow);

        Product GetById(int id);

        IEnumerable<Product> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        IEnumerable<Product> GetRelate(int id, int sl);
             
        void InCreateView(int id);
        IEnumerable<Product> GetListProductByTag(string tagId, int pageIndex, int pageSize, out int totalRow);

        IEnumerable<string> GetListProductByName(string keyword);
        void SaveChanges();
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private ITagRepository _tagRepository;
        private IProductTagRepository _productTagRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, ITagRepository tagRepository, IProductTagRepository productTagRepository)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
            this._tagRepository = tagRepository;
            this._productTagRepository = productTagRepository;
        }

        public Product Add(Product product)
        {
            var productet = _productRepository.Add(product);
            _unitOfWork.Commit();

            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                foreach(string tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    if(_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tage = new Tag();
                        tage.ID = tagId;
                        tage.Name = tag;
                        tage.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tage);
                    }
                    ProductTag productTag = new ProductTag();
                    productTag.TagID = tagId;
                    productTag.ProductID = product.ID;
                    _productTagRepository.Add(productTag);
                }
                _unitOfWork.Commit();
            }
            return productet;
        }

        public void Delete(int id)
        {
            _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll(new string[] { "ProductCategory" });
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
            else
            {
                return _productRepository.GetAll();
            }
        }

        public IEnumerable<Product> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow)
        {
            return _productRepository.GetMultiPaging(x => x.Status && x.CategoryID == categoryId, out totalRow, page, pageSize, new string[] { "ProductCategory" });
        }

        public IEnumerable<Product> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            return _productRepository.GetAllByTag(tag, page, pageSize, out totalRow);
        }

        public IEnumerable<Product> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _productRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);            
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                foreach (string tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tage = new Tag();
                        tage.ID = tagId;
                        tage.Name = tag;
                        tage.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tage);
                    }
                    _productTagRepository.DeleteMulti(x => x.ProductID == product.ID);
                    ProductTag productTag = new ProductTag();
                    productTag.TagID = tagId;
                    productTag.ProductID = product.ID;
                    _productTagRepository.Add(productTag);
                }                
            }
            _unitOfWork.Commit();
        }

        public IEnumerable<Product> GetHome(bool HomeFlag)
        {
            return _productRepository.GetMulti(x => x.HomeFlag == HomeFlag && x.Status);
        }

        public IEnumerable<Product> GetHot(bool HotFlag)
        {
            return _productRepository.GetMulti(x => x.HotFlag == HotFlag && x.Status);
        }

        public IEnumerable<Product> GetListProductByCategoryPageing(int categoryId, int PageIndex, int pageSize, out int totalRow)
        {
            var listProduct = _productRepository.GetMulti(x => x.Status && x.CategoryID == categoryId);
            totalRow = listProduct.Count();
            return listProduct.Skip((PageIndex - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetRelate(int id, int sl)
        {
            var product = _productRepository.GetSingleById(id);
            return _productRepository.GetMulti(x => x.ID != id && x.Status && x.CategoryID == product.CategoryID).OrderByDescending(x =>x.Description).Take(sl);
        }        

        public void InCreateView(int id)
        {
            var product = _productRepository.GetSingleById(id);
            if (product.ViewCount.HasValue)
            {
                product.ViewCount += 1;
            }
            else
            {
                product.ViewCount = 1;
            }
            _productRepository.Update(product);
            _unitOfWork.Commit();
        }

        public IEnumerable<Product> GetListProductByTag(string tagId, int pageIndex, int pageSize, out int totalRow)
        {
            return _productRepository.GetAllByTag(tagId, pageIndex, pageSize, out totalRow);
        }

        public IEnumerable<string> GetListProductByName(string keyword)
        {
            return _productRepository.GetMulti(x => x.Status && x.Name.Contains(keyword)).Select(a => a.Name);
        }
    }
}