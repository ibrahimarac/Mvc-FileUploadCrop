using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageCrop_Upload.Models
{
    public class PostedFile
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int CropAreaWidth { get; set; }
        public int CropAreaHeight { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}