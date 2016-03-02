using CustomCaptcha.Models;
using SRVTextToImage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomCaptcha.Controllers
{
    public class CaptchaController : Controller
    {
        // GET: Captcha
        public ActionResult FeedbackForm()
        {
            ViewBag.Flag = "hidden";
            return View();
        }

         [HttpGet]
         [OutputCache(NoStore =true ,Duration =0,VaryByParam ="None")] // this is output cache false
         public FileResult GetCaptchaImage()
        {
           
            CaptchaRandomImage cr = new CaptchaRandomImage();
            this.Session["CaptchaImageText"] = cr.GetRandomString(5);
            cr.GenerateImage(this.Session["CaptchaImageText"].ToString(), 300, 80, Color.DarkGray, Color.White);
            MemoryStream ms = new MemoryStream();
            cr.Image.Save(ms, ImageFormat.Png);
            ms.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(ms, "image/png");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FeedbackForm(FeedbackModel Fm ,string Captchatxt)
        {
            if (Session["count"]==null)
            {
                Session["count"] = 0;
                ViewBag.Flag = "hidden";
            }
            else
            {
                int cnt = Convert.ToInt16(Session["count"]);

                cnt++;
                Session["count"] = cnt;
                if (cnt >= 3)
                {
                    ViewBag.Flag = "show";
                }
                else
                    ViewBag.Flag = "hidden";


            }
           

            if (this.Session["CaptchaImageText"].ToString() == Captchatxt)
            {
                ViewBag.Message = "Captcha Validation Success";
            }
            else
            {
                ViewBag.Message = "Captcha Validation Failed";
            }
            return View();
        }
    }
}