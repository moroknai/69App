using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace _69App
{
    class ImageTo69s
    {
        List<Pixel69> sixtynines;

        List<Pixel69> SixtyNines { get { return sixtynines; } }

        public ImageTo69s(List<Pixel69> _69s)
        {
            sixtynines = _69s;
        }

        public Bitmap Convert(Bitmap image)
        {

            ImageDataArr imageArr = new ImageDataArr(image);

            int w = sixtynines[0].ImageArr.Width;
            int h = sixtynines[0].ImageArr.Height;

            for(int py = 0; py < imageArr.Height; py += h)
            {
                for (int px = 0; px < imageArr.Width; px += w)
                {
                    ImageDataArr square = imageArr.GetSquare(px, py, w, h);
                    imageArr.SetSquare(FindClosest(square).ImageArr, px, py);
                }
            }


            return imageArr.ToBitmap();
        }

        public Bitmap Convert(ImageDataArr imageArr)
        {
            ImageDataArr clone = imageArr.Clone(false);

            int w = sixtynines[0].ImageArr.Width;
            int h = sixtynines[0].ImageArr.Height;

            for (int py = 0; py < clone.Height; py += h)
            {
                for (int px = 0; px < clone.Width; px += w)
                {
                    ImageDataArr square = clone.GetSquare(px, py, w, h);
                    clone.SetSquare(FindClosest(square).ImageArr, px, py);
                }
            }


            return clone.ToBitmap();
        }

        public Bitmap Convert(ImageDataArr imageArr, int toSize)
        {
            ImageDataArr clone = imageArr.Clone(false);

            //int w = sixtynines[0].ImageArr.Width;
            //int h = sixtynines[0].ImageArr.Height;

            for (int py = 0; py < clone.Height; py += toSize)
            {
                for (int px = 0; px < clone.Width; px += toSize)
                {
                    ImageDataArr square = clone.GetSquare(px, py, toSize, toSize);
                    //Pixel69 resizedPixel69 = new Pixel69(FindClosest(square).ImageArr.ToBitmap(), toSize, toSize);
                    Pixel69 resizedPixel69 = FindClosest(square);
                    resizedPixel69.Resize(toSize, toSize);
                    clone.SetSquare(resizedPixel69.ImageArr, px, py);
                }
            }


            return clone.ToBitmap();
        }
        public Bitmap Convert(Bitmap image, int toSize)
        {

            ImageDataArr imageArr = new ImageDataArr(image);

            int w = toSize;
            int h = toSize;

            for (int py = 0; py < imageArr.Height; py += h)
            {
                for (int px = 0; px < imageArr.Width; px += w)
                {
                    ImageDataArr square = imageArr.GetSquare(px, py, w, h);
                    Pixel69 resizedPixel69 = new Pixel69(FindClosest(square).ImageArr.ToBitmap(), toSize, toSize);
                    imageArr.SetSquare(resizedPixel69.ImageArr, px, py);
                }
            }

            return imageArr.ToBitmap();
        }

        Pixel69 FindClosest(ImageDataArr square)
        {
            Pixel69 closest_p69 = null;
            int min = int.MaxValue;
            foreach(var p69 in sixtynines)
            {
                int diff = p69.ImageArr.Ravg - square.Ravg;
                int rd = (int)Math.Round(Math.Sqrt(diff * diff));

                diff = p69.ImageArr.Gavg - square.Gavg;
                int gd = (int)Math.Round(Math.Sqrt(diff * diff));

                diff = p69.ImageArr.Bavg - square.Bavg;
                int bd = (int)Math.Round(Math.Sqrt(diff * diff));

                int sum = rd + gd + bd;
                if(sum < min)
                {
                    min = sum;
                    closest_p69 = p69;
                }
            }

            return closest_p69;
        }
    }
}
