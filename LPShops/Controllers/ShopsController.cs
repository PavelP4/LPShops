using LPShops.DataAccess;
using LPShops.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LPShops.Controllers
{
    public class ShopsController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        private Repository<Shop> _shopRepository;       

        public ShopsController(): this(new UnitOfWork()) {}

        public ShopsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _shopRepository = _unitOfWork.Repository<Shop>();            
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts(int id)
        {
            Shop shop = _shopRepository.GetById(id);
            if (shop != null)
            {
                return shop.Products.Select(p => new Product()
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    ShopId = p.ShopId
                });
            }
            return null;
        }

        [HttpPost]
        public HttpResponseMessage DeleteShop(int id)
        {
            Shop shop = _shopRepository.GetById(id);
            if (shop != null)
            {
                _shopRepository.Delete(shop);
                _unitOfWork.Save();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }


        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
