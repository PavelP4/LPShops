using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LPShops.DataAccess;
using LPShops.Models;

namespace LPShops.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private Repository<Shop> _shopRepository;

        public HomeController() : this(new UnitOfWork()) {}

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _shopRepository = _unitOfWork.Repository<Shop>();
        }

        public ActionResult Index()
        {     
            return View(_shopRepository.Table.ToList());
        }

        [HttpGet]
        public ActionResult CreateEdit(int? id)
        {
            Shop model = null;

            if (!id.HasValue)
            {
                model = new Shop();
                ViewBag.Title = "Create";                
            }
            else
            {
                model = _shopRepository.GetById(id);
                ViewBag.Title = "Edit";
            }          
                       
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateEdit(Shop model)
        {
            if (ModelState.IsValid)
            {
                if (model.ShopId == 0)
                {
                    _shopRepository.Insert(model);
                }
                else
                {
                    Shop entity = _shopRepository.GetById(model.ShopId);

                    entity.Name = model.Name;
                    entity.Address = model.Address;
                    entity.Mode = model.Mode;

                    _shopRepository.Update(entity);
                }
                        
                _unitOfWork.Save();

                if (model.ShopId > 0)
                {
                    return RedirectToAction("Index");
                }    
            }
            
            return View(model);            
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}