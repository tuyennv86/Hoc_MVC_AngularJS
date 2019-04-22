﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    [Serializable]
    public class ShoppingCartViewModel
    {
        public int ProductId { get; set; }
        public ProductViewModel product { get; set; }
        public int Quantity { get; set; }        
    }
}