using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaguaroFinal.Models
{
    [Table("categories")]
    public class Category
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {

        }

        public Category(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}