using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.Domain.Entity
{
    public class BookTags 
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int TagId { get; set; }

        [ForeignKey(nameof(BookId))]
        public virtual Books Books { get; set; }

        [ForeignKey(nameof(TagId))]
        public virtual Tags Tags { get; set; }
    }
}
