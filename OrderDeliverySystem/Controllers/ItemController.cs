﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderDeliverySystem.Share.Data;
using OrderDeliverySystem.Share.Data.Models;
using System.Security.Claims;
using OrderDeliverySystem.Share.DTOs;

namespace OrderDeliverySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
       /* public class ItemsController(AppDbContext context, ILogger logger) : ControllerBase*/
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ItemsController> _logger;

        public  ItemsController(AppDbContext context, ILogger<ItemsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewItemDTO>>> GetItems(int pageNumber = 1, int pageSize = 10)
        {
            var items = await _context.Items
                .Include(i => i.Merchant)
                .Where(i => i.IsAvailable == 1)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (items == null || !items.Any())
            {
                _logger.LogInformation("No items found.");
                return NotFound("No available items found.");
            }

            // 将 Item 转换为 ViewItemDTO
            var viewItems = items.Select(item => new ViewItemDTO
            {
                MerchantId = item.MerchantId,
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                ItemDescription = item.ItemDescription,
                ItemPrice = item.ItemPrice,
                ItemPic = item.ItemPic,
                ItemIsAvailable = item.ItemIsAvailable
            }).ToList();

            return Ok(viewItems);
        }

        // GET: api/items/merchant/{merchantId}
        [HttpGet("merchant/{merchantId}")]
        public async Task<ActionResult<IEnumerable<ViewItemDTO>>> GetItemsByMerchant(int merchantId, int pageNumber = 1, int pageSize = 10)
        {
            var items = await _context.Items
                .Include(i => i.Merchant)
                .Where(i => i.Merchant.UserId == merchantId && i.IsAvailable == 1)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (items == null || items.Count == 0)
            {
                _logger.LogInformation($"No items found for merchant {merchantId}.");
                return NotFound($"No available items found for merchant {merchantId}.");
            }

            // 将 Item 实体转换为 ViewItemDTO
            var viewItems = items.Select(item => new ViewItemDTO
            {
                MerchantId = item.MerchantId,
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                ItemDescription = item.ItemDescription,
                ItemPrice = item.ItemPrice,
                ItemPic = item.ItemPic,
                ItemIsAvailable = item.ItemIsAvailable
            }).ToList();

            return Ok(viewItems);
        }

        // GET: api/items/merchant/{merchantId}
        [HttpGet("merchant")]
        [Authorize(Roles = "Merchant")]
        public async Task<ActionResult<IEnumerable<ViewItemDTO>>> GetItems()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var merchant = await _context.Merchants.FirstOrDefaultAsync(m => m.UserId == userId);
            if (merchant == null)
            {
                return NotFound(new { Error = "Merchant not found." });
            }

            var items = await _context.Items
               .Where(i => i.MerchantId == merchant.MerchantId)
               .ToListAsync();

            // 将 Item 实体转换为 ViewItemDTO
            var viewItems = items.Select(item => new ViewItemDTO
            {
                MerchantId = item.MerchantId,
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                ItemDescription = item.ItemDescription,
                ItemPrice = item.ItemPrice,
                ItemPic = item.ItemPic,
                ItemIsAvailable = item.ItemIsAvailable
            }).ToList();

            return Ok(viewItems);
        }

        // GET: api/items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewItemDTO>> GetItem(int id)
        {
            var item = await _context.Items
                .Include(i => i.Merchant) // Include merchant details
                .FirstOrDefaultAsync(i => i.ItemId == id);

            if (item == null)
            {
                _logger.LogInformation($"Item with id {id} not found.");
                return NotFound($"Item with id {id} not found.");
            }

            // 将 Item 实体转换为 ViewItemDTO
            var viewItem = new ViewItemDTO
            {
                MerchantId = item.MerchantId,
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                ItemDescription = item.ItemDescription,
                ItemPrice = item.ItemPrice,
                ItemPic = item.ItemPic,
                ItemIsAvailable = item.ItemIsAvailable
            };

            return Ok(viewItem);
        }

        // Merchant creates an item (requires login and merchant role)
        //[HttpPost]
        // [Authorize(Roles = "Merchant")]
        /* public async Task<ActionResult<Item>> CreateItem(CreateItemDTO newItemDto)
         {
             // 获取当前登录用户的 ID
             //  var merchantId = User.FindFirstValue(ClaimTypes.NameIdentifier);
             var merchantId = 2;
             if (merchantId == null)
             {
                 _logger.LogWarning("Unauthorized access to item creation");
                 return Unauthorized("You must be logged in as a merchant to create items.");
             }

             // 创建新Item对象并设置属性
             var newItem = new Item
             {
                 MerchantId = newItemDto.MerchantId,
                 ItemName = newItemDto.ItemName,
                 ItemDescription = newItemDto.ItemDescription,
                 ItemPrice = newItemDto.ItemPrice,
                 ItemPic = newItemDto.ItemPic,
                 ItemIsAvailable = newItemDto.ItemIsAvailable,
                 Merchant = null // 或者提供适当的 Merchant 对象
             };

             // 验证 Item 信息
             if (!ModelState.IsValid)
             {
                 _logger.LogError("Invalid item data");
                 return BadRequest(ModelState);
             }

             // 添加 Item 到数据库并保存
             _context.Items.Add(newItem);
             await _context.SaveChangesAsync();

             // _logger.LogInformation($"Item '{newItem.ItemName}' created by merchant {merchantId}.");

             // 返回新创建的 Item 和 201 状态码
             return CreatedAtAction(nameof(GetItem), new { id = newItem.ItemId }, newItem);
         }
 */

        [HttpPost]
        [Authorize(Roles = "Merchant")]
        public async Task<IActionResult> CreateItem(CreateItemDTO newItemDto)
        {
            // 获取当前登录用户的 ID
            
            var loggedInUserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var merchant = await _context.Merchants.FirstOrDefaultAsync(m => m.UserId == loggedInUserId);


            if (merchant == null)
                return NotFound(new { Error = "Merchant not found" });

            using var transaction = await _context.Database.BeginTransactionAsync();
            var newItem = new Item
            {
                // MerchantId = merchantId, // Use the hardcoded merchant ID
                MerchantId = newItemDto.MerchantId,
                ItemName = newItemDto.ItemName,
                ItemDescription = newItemDto.ItemDescription,
                ItemPrice = newItemDto.ItemPrice,
                ItemPic = newItemDto.ItemPic,
                ItemIsAvailable = newItemDto.ItemIsAvailable,
                Merchant = merchant // You can assign a merchant if necessary
            };


            _context.Items.Add(newItem);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return Ok("MenuItem has been successfully updated");
        }
        


        // PUT: api/items/{id}
        [HttpPut("{itemId}")]
        [Authorize(Roles = "Merchant")]
        public async Task<ActionResult<Item>> UpdateItem(int itemId, UpdateItemDTO updatedItemDto)
        {
            // 获取当前用户的 ID
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // 查找数据库中的 Item
            var existingItem = await _context.Items.FindAsync(itemId);
            if (existingItem == null)
            {
                return NotFound($"Item not found.");
            }

            // 更新属性
            existingItem.ItemName = updatedItemDto.ItemName;
            existingItem.ItemDescription = updatedItemDto.ItemDescription;
            existingItem.ItemPrice = updatedItemDto.ItemPrice;
            existingItem.ItemPic = updatedItemDto.ItemPic;
            existingItem.ItemIsAvailable = updatedItemDto.ItemIsAvailable;

            // 保存更改到数据库
            await _context.SaveChangesAsync();
           

            // 返回更新后的 Item 和 200 状态码
            return Ok(existingItem);
        }

        // DELETE: api/items/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Merchant")] // Ensure that only merchants can access this endpoint
        public async Task<IActionResult> DeleteItem(int id)
        {
            // Find the item by its ID
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                _logger.LogInformation($"Item with id {id} not found.");
                return NotFound($"Item with id {id} not found.");
            }



            // Remove the item from the database
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 No Content status
        }
    }
}
