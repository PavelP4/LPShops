using LPShops.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LPShops.DataAccess
{
    public class ShopDBInitializer : CreateDatabaseIfNotExists<ShopDBContext>
    {
        protected override void Seed(ShopDBContext context)
        {
            base.Seed(context);

            UnitOfWork unitOfWork = new UnitOfWork(context);
            Repository<Shop> shops = unitOfWork.Repository<Shop>();            
                        
            shops.Insert(new Shop()
            {
                Name = "Мономах",
                Address = "ул.Кальварийская 4-1",
                Mode = "9:00 - 21:00, понедельник выходной",
                Products = new HashSet<Product>(){
                    new Product(){ Name = "Цепочка золотая К-4", Description = "Длинна 40см, проба 587"},
                    new Product(){ Name = "Кольцо серебрянное М", Description = "Проба 887, вес 40г"},
                    new Product(){ Name = "Серьги серебрянные", Description = "Проба 889"}}
            });

            shops.Insert(new Shop()
            {
                Name = "ЗооМагазин",
                Address = "ул.Козлова 44",
                Mode = "9:00 - 21:00, суббота 9:00 - 20:00",
                Products = new HashSet<Product>(){
                    new Product(){ Name = "Корм кошачии", Description = "Вес 5кг"},
                    new Product(){ Name = "Корм собачий", Description = "Вес 25кг"},}
            });

            shops.Insert(new Shop()
            {
                Name = "АвтоМир",
                Address = "ул.Голубева 2",
                Mode = "9:00 - 21:00, без выходных",
                Products = new HashSet<Product>(){
                    new Product(){ Name = "Пружина задняя", Description = "Внутренний диаметр 255мм, высота 440мм"},
                    new Product(){ Name = "Ролик натяжителя", Description = "Skoda, Audi"},
                    new Product(){ Name = "Масло моторное Mobil1 5W30", Description = "ESP formula 505/507"},
                    new Product(){ Name = "Лампочка ПТФ Philips", Description = "H8 35W"}}
            });

            unitOfWork.Save();
        }
    }
}