using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Test.Contexts;
using WebApi.Test.Models;

namespace WebApi.Test.Services
{
    public class ItemsSearch
    {
        private ItemDbContext _itemDb;
        public ItemsSearch(ItemDbContext itemDb)
        {
            _itemDb = itemDb;
        }
        public List<Items> GetResultAsync(string query)
        {
            query = query.ToLower();
           
            var items = _itemDb.Items.Where(x =>
            x.Name.ToLower().Contains(query)).ToList();
           
            return items;
        }
    }
}
