using ECommerce_be.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_be.Domain.Entities
{
    public class File : BaseEntity
    {
        // A file cannot be updated. Its only created or deleted.
        [NotMapped] // => This property will not be created in the database because of NotMapped.
        public override DateTime UpdatedTime { get => base.UpdatedTime; set => base.UpdatedTime = value; }

        public string Name { get; set; }
        public string Path { get; set; }
    }
}
