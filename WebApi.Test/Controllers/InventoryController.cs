using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.Test.Contexts;
using WebApi.Test.Models;
using WebApi.Test.Repository;
using WebApi.Test.Services;

namespace WebApi.Test.Controllers
{
    [ApiController]
    [Route("api/inventory")]
    public class InventoryController : ControllerBase
    {
        private ItemDbContext _itemDb;
        public InventoryController(ItemDbContext itemDb)
        {
            _itemDb = itemDb;
        }

        [HttpPost]
        [Route("add-items")]
        public async Task<ActionResult> AddItems(Items items)
        {
            if (items == null)
                return NotFound();

            var itemsFromDb = _itemDb.Items.Any(x => x.Name == items.Name);
            var itemList = new List<Items>();
            if (!itemsFromDb)
            {
                itemList.Add(new Items
                {
                    ID = items.ID,
                    Name = items.Name,
                    PerPrice = items.PerPrice,
                    Descriptions = items.Descriptions,
                    Quantity = items.Quantity
                });

                foreach (var item in itemList)
                {
                    _itemDb.Items.Add(item);
                }

                await _itemDb.SaveChangesAsync();
                return StatusCode((int)HttpStatusCode.OK, itemList);
            }

            return StatusCode((int)HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("get-items")]
        public async Task<ActionResult> GetItems()
        {
            var itemRepository = new ItemRepository(_itemDb);

            var items = await itemRepository.GetAllItems();

            return StatusCode((int)HttpStatusCode.OK, items);
        }

        [HttpPost]
        [Route("update-items")]
        public async Task<ActionResult> UpdateItems(Items newItem)
        {
            var item = _itemDb.Items.Find(newItem.ID);

            if (newItem.ID <= 0)
                return NotFound("ID not found");

            if (newItem.Name != item.Name)
            {
                if (_itemDb.Items.Any(x => x.Name == newItem.Name))
                {
                    return NotFound("ID has been used");
                }
            }

            item.ID = newItem.ID;
            item.Name = newItem.Name;
            item.PerPrice = newItem.PerPrice;
            item.Descriptions = newItem.Descriptions;
            item.Quantity = newItem.Quantity;

            _itemDb.Items.Update(item);
            await _itemDb.SaveChangesAsync();

            return Ok(item);
        }

        [HttpPost]
        [Route("delete-items/{itemId}")]
        public async Task<ActionResult> DeleteItems(Items items)
        {
            var dbItem = _itemDb.Items.Find(items.ID);

            if (dbItem.ID <= 0)
                return NotFound("ID not found");

            dbItem.ID = items.ID;
            dbItem.Name = items.Name;
            dbItem.PerPrice = items.PerPrice;
            dbItem.Descriptions = items.Descriptions;
            dbItem.Quantity = items.Quantity;

            _itemDb.Items.Remove(dbItem);
            await _itemDb.SaveChangesAsync();

            return Ok($"This ID: {dbItem.ID} has been removed from database");
        }

        [HttpPost]
        [Route("search-items/{query}")]
        public ActionResult SearchItems(string query)
        {
            if (string.IsNullOrEmpty(query))
                return StatusCode((int)HttpStatusCode.BadRequest, "Search is empty");

            var itemSearch = new ItemsSearch(_itemDb);
            var itemName = itemSearch.GetResultAsync(query);

            return StatusCode((int)HttpStatusCode.OK, itemName);
        }
    }
}
