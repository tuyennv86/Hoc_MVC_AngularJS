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
    public interface ITagService
    {       
        IEnumerable<Tag> GetAllByProductId(int id);       
    }
    public class TagService : ITagService
    {
        ITagRepository _tagRepository;
        IUnitOfWork _unitOfWork;

        public TagService(ITagRepository tagRepository, IUnitOfWork unitOfOwrk)
        {
            this._tagRepository = tagRepository;
            this._unitOfWork = unitOfOwrk;
        }       

        public IEnumerable<Tag> GetAllByProductId(int id)
        {
            return _tagRepository.GetAllByProductId(id);
        }
       
    }    
}
