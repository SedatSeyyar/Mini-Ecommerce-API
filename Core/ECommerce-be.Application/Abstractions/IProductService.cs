﻿using ECommerce_be.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_be.Application.Abstractions
{
    public interface IProductService
    {
        public List<Product> GetAllProducts();
    }
}
