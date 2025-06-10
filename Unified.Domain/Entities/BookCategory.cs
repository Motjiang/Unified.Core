using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unified.Domain.Entities
{
   public  class BookCategory
    {
        [Key]
        public int BookCategoryId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public List<Book>? Books { get; set; }
    }
}
