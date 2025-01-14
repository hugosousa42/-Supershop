﻿using System.Linq;
using System.Threading.Tasks;
using Supershop.Data.Entities;
using Supershop.Models;
using Supershop.Helpers;
using Microsoft.EntityFrameworkCore;
using System;

namespace Supershop.Data
{
    public class OrderRepository : GenericRepository<Order> , IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public OrderRepository(DataContext context, IUserHelper userHelper): base(context) 
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task AddItemToOrderAsync(AddItemViewModel model, string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if(user == null)
            {
                return;
            }

            var product = await _context.Products.FindAsync(model.ProductId);
            if (product == null)
            {
                return;
            }

            var orderDetailTemp = await _context.OrderDetailsTemp
                .Where(odt => odt.User == user && odt.Product == product)
                .FirstOrDefaultAsync();

            if (orderDetailTemp == null)
            {
                orderDetailTemp = new OrderDetailTemp
                {
                    Price = product.Price,
                    Product = product,  
                    Quantity = model.Quantity,
                    User = user,
                };

                _context.OrderDetailsTemp.Add(orderDetailTemp);
            }
            else
            {
                orderDetailTemp.Quantity += model.Quantity;
                _context.OrderDetailsTemp.Update(orderDetailTemp);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ConfirmOrderAsync(string usarName)
        {
            var user = await _userHelper.GetUserByEmailAsync(usarName);
            if (user == null) 
            { 
                return false;
            }

            var ordTmps = await _context.OrderDetailsTemp
                .Include(o => o.Product)
                .Where(o =>o.User == user)
                .ToListAsync();
            
            if (ordTmps == null || ordTmps.Count == 0)
            {
                return false;
            }

            var detail = ordTmps.Select(o => new OrderDetail 
            {
                Price = o.Price,
                Product = o.Product,
                Quantity = o.Quantity
            }).ToList();

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                User = user,
                Items = detail
            };
            await CreateAsync(order);
            _context.OrderDetailsTemp.RemoveRange(ordTmps);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteDetailTempAsync(int id)
        {
            var orderDetailTemp = await _context.OrderDetailsTemp.FindAsync(id);
            if (orderDetailTemp == null)
            {
                return;
            }

            _context.OrderDetailsTemp.Remove(orderDetailTemp);
            await _context.SaveChangesAsync();
        }

        public async Task DeliveryOrder(DeliveryViewModel model)
        {
            var order = await _context.Orders.FindAsync(model.Id);
            if(order==null)
            {
                return;
            }

            order.DeliveryDate = model.DeliveryDate;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<OrderDetailTemp>> GetDetailTempsAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            return _context.OrderDetailsTemp
                .Include(p => p.Product)
                .Where(o => o.User == user)
                .OrderBy(o => o.Product.Name);
        }

        public async Task<IQueryable<Order>> GetOrderAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if(user == null)
            {
                return null;
            }

            if (await _userHelper.IsUserInRoleAsyc (user, "Admin"))
            {
                return _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .OrderByDescending(o => o.OrderDate);
            }

            return _context.Orders
                .Include(o => o.Items)
                .ThenInclude(p => p.Product)
                .Where(o => o.User == user)
                .OrderByDescending (o => o.OrderDate);
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            return await _context.Orders.FindAsync(id); 
        }

        public async Task ModifyOrderDetailTempQuantityAsync(int id, double quantity)
        {
            var orderDetailTemp = await _context.OrderDetailsTemp.FindAsync(id);
            if (orderDetailTemp == null)
            {
                return;
            }

            orderDetailTemp.Quantity += quantity;
            if(orderDetailTemp.Quantity >0)
            {
                _context.OrderDetailsTemp.Update(orderDetailTemp);
                await _context.SaveChangesAsync();
            }
        }
    }
}
