using AutoMapper;
using TeduShop.Model.Models;
using TeduShop.Web.Models;

namespace TeduShop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            //Mapper.CreateMap<Post, PostViewModel>();
            //Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
            //Mapper.CreateMap<Tag, TagViewModel>();
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Post, PostViewModel>();
                cfg.CreateMap<PostCategory, PostCategoryViewModel>();
                cfg.CreateMap<PostTag, PostTagViewModel>();
                cfg.CreateMap<Tag, TagViewModel>();
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<ProductCategory, ProductCategoryViewModel>();
                cfg.CreateMap<ProductTag, ProductTagViewModel>();
            });
        }
    }
}
