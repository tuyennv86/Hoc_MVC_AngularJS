using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface ISlideService
    {
        Slide Add(Slide slide);
        void Update(Slide slide);
        void Delete(int Id);
        IEnumerable<Slide> GetAll();
        IEnumerable<Slide> GetAll(string keyword);
        IEnumerable<Slide> GetAllPaging(int page, int pageSize, out int totalRow);
        Slide GetById(int id);
        void SaveChanges();
    }
    public class SlideService : ISlideService
    {
        ISlideRepository _slideRepository;
        IUnitOfWork _unitOfWork;

        public SlideService(ISlideRepository slideRepository, IUnitOfWork unitOfOwrk)
        {
            this._slideRepository = slideRepository;
            this._unitOfWork = unitOfOwrk;
        }
        public Slide Add(Slide slide)
        {
            var listslide = _slideRepository.Add(slide);
            _unitOfWork.Commit();
            return listslide;
        }

        public void Delete(int Id)
        {
            _slideRepository.Delete(Id);
        }

        public IEnumerable<Slide> GetAll()
        {
            return _slideRepository.GetAll();
        }

        public IEnumerable<Slide> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _slideRepository.GetMulti(x => x.Name.Contains(keyword));
            }
            else
            {
                return _slideRepository.GetAll();
            }
        }        
        public IEnumerable<Slide> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _slideRepository.GetMultiPaging(x => x.Status == true, out totalRow, page, pageSize, new string[] {"Slide"});
        }

        public Slide GetById(int id)
        {
            return _slideRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Slide slide)
        {
            _slideRepository.Update(slide);
            _unitOfWork.Commit();
        }
    }
}
