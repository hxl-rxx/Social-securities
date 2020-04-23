using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Social_security.MVC.Controllers
{
    public class LoginController : Controller
    {
        //HttpClientHelper client = new HttpClientHelper("http://localhost:44319/");
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult CreateValidCodeImage()
        {
            //生成长度为5的随机验证码字符串
            var strRandom = ValidCodeUtils.GetRandomCode(5);
            //根据生成的验证码字符串生成图片
            byte[] byteImg = ValidCodeUtils.CreateImage(strRandom);
            //将验证码字符串存入session
            Session["validCode"] = strRandom;
            //把图片返回视图
            return File(byteImg, @"image/jepg");
        }
    }
}