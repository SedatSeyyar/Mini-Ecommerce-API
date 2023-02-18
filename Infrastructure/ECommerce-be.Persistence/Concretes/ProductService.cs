using ECommerce_be.Application.Abstractions;
using ECommerce_be.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_be.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetAllProducts() => new()
        {
            new(){ Id = Guid.NewGuid(), Name="Product 1", Price = 1000, Stock = 14 },
            new(){ Id = Guid.NewGuid(), Name="Product 2", Price = 100, Stock = 13 },
            new(){ Id = Guid.NewGuid(), Name="Product 3", Price = 10, Stock = 12 },
            new(){ Id = Guid.NewGuid(), Name="Product 4", Price = 1, Stock = 11 },
        };
    }
}
