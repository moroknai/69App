using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace _69App
{
    static class ImageProc
    {
        public static Bitmap ResizeImage(Bitmap image, int newW, int newH)
        {
            return image;
        }

        public static List<Bitmap> ResizeImagesToSmallest(List<Bitmap> images)
        {
            FindMinWMinH(images, out int minW, out int minH);

            for(int i = 0; i < images.Count; i++)
            {
                images[i] = new Bitmap(images[i], new Size(minW, minH));
            }

            return images;
        }
        public static List<Bitmap> ResizeImages(List<Bitmap> images, int newW, int newH)
        {
            for (int i = 0; i < images.Count; i++)
            {
                images[i] = new Bitmap(images[i], new Size(newW, newH));
            }

            return images;
        }

        public static void FindMinWMinH(List<Bitmap> images, out int minW, out int minH)
        {
            minW = int.MaxValue;
            minH = int.MaxValue;
            foreach (var im in images)
            {
                if (im.Width < minW)
                {
                    minW = im.Width;
                }
                if(im.Height < minH)
                {
                    minH = im.Height;
                }
            }
        }

        public static byte[] GetByteArr(Bitmap image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        public static float CalcRavg(Bitmap image)
        {
            byte[] arr = image.ToByteArray(ImageFormat.Png);

            return 0f;
        }
    }
    public static class ImageExtensions
    {
        public static byte[] ToByteArray(this Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }
    }
}
