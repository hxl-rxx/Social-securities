using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Social_security.MVC.Controllers
{
    public class LoginController : Controller
    {
        HttpClientHelper client = new HttpClientHelper("http://localhost:44319/");
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public void Login(string name,string pwd ,string validCode)
        {
            var sessionValidCode = "";
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(pwd) && !string.IsNullOrEmpty(validCode))
            {
                sessionValidCode = Session["validCode"].ToString();
                if (sessionValidCode.Equals(validCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
                {
                    int i = int.Parse(client.Get("api/Social/Login?name=" + name + "&pwd=" + pwd));
                    if (i>0)
                    {
                        Response.Write("<script>alert('登录成功！')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('用户名或码错误！')</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('登录失败,验证码错误！')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('账号、密码、验证码不能为空！')</script>");
            }
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