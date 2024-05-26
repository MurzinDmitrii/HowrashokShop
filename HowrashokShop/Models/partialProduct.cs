using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HowrashokShop.Models
{
    partial class Product
    {
        public string DescToShort
        {
            get
            {
                string desc = "";
                int i = 0; 
                while(i < Description.Length && i < 50)
                {
                    desc += Description[i];
                    i++;
                }
                if(Description.Length > 50)
                {
                    desc += "...";
                }
                return desc;
            }
        }
        public string Discount
        {
            get
            {
                var disc = Discounts.OrderByDescending(c => c.DateOfSetting).FirstOrDefault();
                if(disc != null)
                {
                    return $"Скидка {disc.Size}% до {disc.DateOfSetting.AddDays(disc.During).ToShortDateString()}";
                }
                else
                {
                    return "";
                }
            }
        }

        public string Cost
        {
            get
            {
                List<Discount> discounts = new();
                List<Cost> costs = new();
                using(HowrashokShopContext context = new())
                {
                    discounts = context.Discounts.Where(c => c.ProductId == Id).ToList();
                    costs = context.Costs.Where(c => c.ProductId == Id).ToList();
                }
                var disc = discounts.OrderByDescending(c => c.DateOfSetting).FirstOrDefault();
                if (disc != null && disc.DateOfSetting.AddDays(disc.During).Day >= DateTime.Now.Day)
                {
                    return Math.Round((costs.OrderByDescending(c => c.DateOfSetting).ToList()[0].Size) * (100 - disc.Size) / 100,2).ToString();
                }
                else
                {
                    return Math.Round(costs.OrderByDescending(c => c.DateOfSetting).ToList()[0].Size, 2).ToString();
                }
            }
        }
    }
}
