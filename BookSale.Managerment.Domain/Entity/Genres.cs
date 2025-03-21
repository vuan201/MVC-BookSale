using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.Domain.Entity
{
    public class Genres : EntityBase
    {
        [StringLength(250)]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Books> Books { get; set; }   
    }
}
