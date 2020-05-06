using Newtonsoft.Json;
using Social_security.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Social_security.MVC.Controllers
{
    public class LoginController : Controller
    {
        readonly HttpClientHelper client = new HttpClientHelper("http://localhost:44319/");
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public void Login(string name,string pwd ,string validCode)
        {
            
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(pwd) && !string.IsNullOrEmpty(validCode))
            {
                var sessionValidCode = Session["validCode"].ToString();
                if (sessionValidCode.Equals(validCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
                {
                    int i = int.Parse(client.Get("api/Social/Login?name=" + name + "&pwd=" + pwd));
                    if (i>0)
                    {
                        Session["name"] = name;
                        Cookie cookie = new Cookie("uname", name);
                        Response.Write("<script>alert('登录成功！');location.href='/Index.html'</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('用户名或码错误！');location.href='/Login/Login'</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('登录失败,验证码错误！');location.href='/Login/Login'</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('账号、密码、验证码不能为空！');location.href='/Login/Login'</script>");
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public void Register(string name, string pwd, string pwd2, string validCode)
        {

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(pwd) && !string.IsNullOrEmpty(validCode))
            {
                var sessionValidCode = Session["validCode"].ToString();
                if (sessionValidCode.Equals(validCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
                {
                    if (pwd != pwd2)
                    {
                        Response.Write("<script>alert('两次密码不一致！');location.href='/Login/Register'</script>");
                        return;
                    }
                    int i = int.Parse(client.Get("api/Social/Login?name=" + name + "&pwd=" + pwd));
                    if (i > 0)
                    {
                        Response.Write("<script>alert('该用户已存在！');location.href='/Login/Register'</script>");
                    }
                    else
                    {
                        UserLogin user = new UserLogin
                        {
                            Uname = name,
                            PassWord = pwd,
                            Userlevel = 1
                        };
                        int r = int.Parse(client.Post("api/Social/Adduser", JsonConvert.SerializeObject(user)));
                        if (r > 0)
                        {
                            Response.Write("<script>alert('注册成功！');location.href='/Login/Login'</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('注册失败！');location.href='/Login/Register'</script>");
                        }
                    }
                }
                else
                {
                    Response.Write("<script>alert('验证码错误！');location.href='/Login/Register'</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('账号、密码、验证码不能为空！');location.href='/Login/Register'</script>");
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