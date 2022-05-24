using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace _69App
{
    class Pixel69
    {
        //Bitmap image;
        //public Bitmap Image { get { return image; } }

        ImageDataArr originalArr;

        ImageDataArr imageArr;
        public ImageDataArr ImageArr { get { return imageArr; } }


        public Pixel69(string filename)
        {
            Bitmap image = new Bitmap(filename);
            Bitmap clone = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);

            using (Graphics gr = Graphics.FromImage(clone))
            {
                gr.DrawImage(image, new Rectangle(0, 0, clone.Width, clone.Height));
            }

            image = new Bitmap(clone);

            originalArr = new ImageDataArr(image);
            imageArr = new ImageDataArr(image);
        }

        public Pixel69(string filename, int sizeToW, int sizeToH)
        {
            Bitmap image = new Bitmap(filename);
            Bitmap clone = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);

            using (Graphics gr = Graphics.FromImage(clone))
            {
                gr.DrawImage(image, new Rectangle(0, 0, clone.Width, clone.Height));
            }

            image = new Bitmap(clone, sizeToW, sizeToH);

            imageArr = new ImageDataArr(image);
        }

        public Pixel69(Bitmap img, int sizeToW, int sizeToH)
        {
            Bitmap image = img;
            Bitmap clone = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);

            using (Graphics gr = Graphics.FromImage(clone))
            {
                gr.DrawImage(image, new Rectangle(0, 0, clone.Width, clone.Height));
            }

            image = new Bitmap(clone, sizeToW, sizeToH);

            imageArr = new ImageDataArr(image);
        }

        public void Resize(int newW, int newH)
        {
            //byte[] data = imageArr.Data;
            imageArr.ResetImage(originalArr.Data, originalArr.Width, originalArr.Height, newW, newH);
        }

        public void Save(string filename, ImageFormat imageFormat)
        {
            //UpdateBitmapBasedOnByteArray();
            imageArr.ToBitmap().Save(filename, imageFormat);
        }

        //public void UpdateBitmapBasedOnByteArray()
        //{
        //    Bitmap img = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);

        //    byte[] warr = new byte[0];

        //    ImageConversions.LoadBitmapData(true, ref img, imageArr.Data, img.Width, img.Height, ref warr, 1);

        //    image = img;
        //}

        //public void UpdateByteArrayBasedOnBitmap()
        //{
        //    byte[] data = imageArr.Data;
        //    ImageConversions.BmpToData(Image, ref data);
        //}

    }
}
