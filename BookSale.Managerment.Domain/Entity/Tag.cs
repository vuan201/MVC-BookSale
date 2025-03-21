using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.Domain.Entity
{
    public class Tags : EntityBase
    {
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
        public virtual ICollection<BookTags> BookTags { get; set; }
    }
}
