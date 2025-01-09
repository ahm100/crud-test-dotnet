using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Domain.Entities;

namespace Vehicle.Domain.Common
{
    public abstract class EntityBase
    {
        [Key] 
        public int Id { get; set; }

        [Required] 
        public DateTime CreateDate { get; set; } = DateTime.Now; 
        public DateTime? UpdateDate { get; set; }

        [Timestamp] 
        public byte[] RowVersion { get; set; }
    }
}
