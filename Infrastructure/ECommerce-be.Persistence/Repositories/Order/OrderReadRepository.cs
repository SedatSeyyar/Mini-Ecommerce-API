using ECommerce_be.Application.Repositories;
using ECommerce_be.Domain.Entities;
using ECommerce_be.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_be.Persistence.Repositories
{
    public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
    {
        public OrderReadRepository(ECommerce_beDbContext context) : base(context)
        {
        }
    }
}
