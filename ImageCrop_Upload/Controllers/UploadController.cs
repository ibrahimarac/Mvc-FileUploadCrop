using ImageCrop_Upload.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageCrop_Upload.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(PostedFile model)
        {
            Image bmpOrj=Bitmap.FromStream(model.File.InputStream);
            
            //Kırpma alanından gelen ölçüler gerçek resim boyutlarına uyarlanıyor
            double ratioX = bmpOrj.Width/Convert.ToDouble(model.CropAreaWidth);
            double ratioY= bmpOrj.Height/Convert.ToDouble(model.CropAreaHeight) ;
            int realWidth = (bmpOrj.Width * model.Width) / model.CropAreaWidth;
            int realHeight = (bmpOrj.Height * model.Height) / model.CropAreaHeight;

            //Kırpılmış resim orjinal resim üzerinden belirlenen ölçülere göre kopyalanıyor.
            Bitmap bmpCropped = new Bitmap(realWidth, realHeight);
            Graphics gr = Graphics.FromImage(bmpCropped);
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            gr.PageUnit = GraphicsUnit.Pixel;
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            
            gr.DrawImage(bmpOrj,new Rectangle(0,0,bmpCropped.Width,bmpCropped.Height),new Rectangle((int)(model.X*ratioX),(int)(model.Y*ratioY),realWidth,realHeight),GraphicsUnit.Pixel);
            bmpCropped.Save(Server.MapPath("~/Content/images/cropped.jpg"));
            //Kırpılmış ve kaydedilmiş resmi görüntüle
            return Content("<img src='/Content/images/cropped.jpg'/>");
        }
    }
}