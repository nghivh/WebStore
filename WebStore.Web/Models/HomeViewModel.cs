﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<SlideViewModel> Slides { get; set; }
        public IEnumerable<ProductViewModel> LastestProducts { get; set; }
        public IEnumerable<ProductViewModel> TopProducts { get; set; }
    }
}