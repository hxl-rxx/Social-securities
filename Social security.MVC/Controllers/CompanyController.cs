using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Social_security.MVC.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        //public ActionResult Del()
        //{
        //    return View();
        //}
        public ActionResult Update(int Id)
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult Update(int id, Company company)
        //{
        //    return View();
        //}
    }
}