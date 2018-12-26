using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<ProductViewModel> ProductHome { get; set; }
        public IEnumerable<ProductViewModel> ProductHot { get; set; }
    }
}