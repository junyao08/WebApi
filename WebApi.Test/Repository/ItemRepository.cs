using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Test.Contexts;
using WebApi.Test.Models;

namespace WebApi.Test.Repository
{
    public class ItemRepository
    {
        private ItemDbContext _itemDb;
        public ItemRepository(ItemDbContext itemDb)
        {
            _itemDb = itemDb;
        }
        public async Task<IEnumerable<Items>> GetAllItems()
        {
            var items = await _itemDb.Items
                .Select(x => new Items(x))
                .OrderBy(x => x.ID)
                .ToListAsync();

            return items;
        }
    }
}
