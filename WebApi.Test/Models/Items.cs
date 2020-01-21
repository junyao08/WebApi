using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Test.Models
{
    [Table("Items")]
    public class Items
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string PerPrice { get; set; }
        public string Descriptions { get; set; }
        public string Quantity { get; set; }

        public Items()
        {

        }
        public Items(Items items)
        {
            this.ID = items.ID;
            this.Name = items.Name;
            this.PerPrice = items.PerPrice;
            this.Descriptions = items.Descriptions;
            this.Quantity = items.Quantity;
        }
    }
}
