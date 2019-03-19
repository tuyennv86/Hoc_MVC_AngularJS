using System.Collections.Generic;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IContacDetailService
    {
        ContacDetail Add(ContacDetail contacDetail);

        void Update(ContacDetail contacDetail);

        void Delete(int id);

        IEnumerable<ContacDetail> GetAll();

        IEnumerable<ContacDetail> GetAllPaging(bool Status, int page, int pageSize, out int totalRow);
        ContacDetail GetSingDefault(bool Status);
        ContacDetail GetById(int id);
        void SaveChanges();

    }
    public class ContacDetailService : IContacDetailService
    {
        private IContacDetailRepository _contacDetailRepository;      
        private IUnitOfWork _unitOfWork;

        public ContacDetailService(IContacDetailRepository contacDetailRyposityry, IUnitOfWork unitOfWork)
        {
            this._contacDetailRepository = contacDetailRyposityry;
            this._unitOfWork = unitOfWork;
        }

        public ContacDetail Add(ContacDetail contacDetail)
        {
            var contac = _contacDetailRepository.Add(contacDetail);
            _unitOfWork.Commit();
            return contac;
        }

        public void Delete(int id)
        {
            _contacDetailRepository.Delete(id);
        }

        public IEnumerable<ContacDetail> GetAll()
        {
            return _contacDetailRepository.GetAll();
        }

        public IEnumerable<ContacDetail> GetAllPaging(bool Status, int page, int pageSize, out int totalRow)
        {
            return _contacDetailRepository.GetMultiPaging(x => x.Status == Status, out totalRow, page, pageSize, new string[] { "ContacDetail" });
        }

        public ContacDetail GetById(int id)
        {
            return _contacDetailRepository.GetSingleById(id);
        }

        public ContacDetail GetSingDefault(bool Status)
        {
            return _contacDetailRepository.GetSingleByCondition(x => x.Status == Status);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ContacDetail contacDetail)
        {
            _contacDetailRepository.Update(contacDetail);
            _unitOfWork.Commit();
        }
    }
}
