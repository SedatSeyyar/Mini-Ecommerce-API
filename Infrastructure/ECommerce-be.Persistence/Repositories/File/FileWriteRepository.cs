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
    internal class FileWriteRepository : WriteRepository<Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(ECommerce_beDbContext context) : base(context)
        {
        }
    }
}
