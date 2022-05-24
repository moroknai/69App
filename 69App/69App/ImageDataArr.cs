using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace _69App
{
    class ImageDataArr
    {
        public PixelFormat PixelFormat { get; }

        int width, height;
        public int Width { get { return width; } }
        public int Height { get { return height; } }

        byte ravg, gavg, bavg;
        public byte Ravg { get { return ravg; } }
        public byte Gavg { get { return gavg; } }
        public byte Bavg { get { return bavg; } }

        byte[] imageData;
        public byte[] Data { get { return imageData; } }

        public ImageDataArr(Bitmap image, bool calc_avgs = true)
        {
            this.width = image.Width;
            this.height = image.Height;
            this.PixelFormat = image.PixelFormat;
            byte[] data = new byte[0];
            ImageConversions.BmpToData(image, ref data);
            this.imageData = data;
            if(calc_avgs) CalcAverages();
        }
        public ImageDataArr(byte[] data, int width, int height, PixelFormat pixelFormat, bool calc_avgs = true)
        {
            this.width = width;
            this.height = height;
            PixelFormat = pixelFormat;
            imageData = data;
            if(calc_avgs) CalcAverages();
        }
        public ImageDataArr(byte[] data, int width, int height, PixelFormat pixelFormat, byte ravg, byte gavg, byte bavg)
        {
            this.width = width;
            this.height = height;
            PixelFormat = pixelFormat;
            imageData = data;
            this.ravg = ravg;
            this.gavg = gavg;
            this.bavg = bavg;
        }

        //public void CalcRgbAvg(out int ravg, out int gavg, out int bavg)
        //{
        //    if (PixelFormat == PixelFormat.Format32bppRgb)
        //    {
        //        double rc = 0, gc = 0, bc = 0;
        //        for (int y = 0; y < Height; y++)
        //        {
        //            for (int x = 0; x < Width; x++)
        //            {
        //                rc += GetRedPixel(x, y);
        //                gc += GetGreenPixel(x, y);
        //                bc += GetBluePixel(x, y);
        //            }
        //        }
        //        double num_pix = Width * Height;

        //        ravg = (int)Math.Round(rc / num_pix);
        //        gavg = (int)Math.Round(gc / num_pix);
        //        bavg = (int)Math.Round(bc / num_pix);

        //    }
        //    else throw new NotImplementedException();
        //}


        public byte GetRedPixel(int x, int y)
        {
            return imageData[(y * Width * 4) + (x * 4) + ImageConversions.RED_IDX];
        }
        public void SetRedPixel(int x, int y, byte value)
        {
            imageData[(y * Width * 4) + (x * 4) + ImageConversions.RED_IDX] = value;
        }

        public byte GetGreenPixel(int x, int y)
        {
            return imageData[(y * Width * 4) + (x * 4) + ImageConversions.GREEN_IDX];
        }
        public void SetGreenPixel(int x, int y, byte value)
        {
            imageData[(y * Width * 4) + (x * 4) + ImageConversions.GREEN_IDX] = value;
        }
        public byte GetBluePixel(int x, int y)
        {
            return imageData[(y * Width * 4) + (x * 4) + ImageConversions.BLUE_IDX];
        }
        public void SetBluePixel(int x, int y, byte value)
        {
            imageData[(y * Width * 4) + (x * 4) + ImageConversions.BLUE_IDX] = value;
        }

        public void GetPixel(int x, int y, out byte r, out byte g, out byte b, out byte a)
        {
            int idx = (y * Width * 4) + (x * 4);
            r = imageData[idx + ImageConversions.RED_IDX];
            g = imageData[idx + ImageConversions.GREEN_IDX];
            b = imageData[idx + ImageConversions.BLUE_IDX];
            a = imageData[idx + ImageConversions.ALPHA_IDX];
        }

        public void SetPixel(int x, int y, byte r, byte g, byte b, byte a = 255)
        {
            int idx = (y * Width * 4) + (x * 4);
            imageData[idx + ImageConversions.RED_IDX] = r;
            imageData[idx + ImageConversions.GREEN_IDX] = g;
            imageData[idx + ImageConversions.BLUE_IDX] = b;
            imageData[idx + ImageConversions.ALPHA_IDX] = a;
        }

        public static void GetPixel(byte[] data, int width_, int height_, int x, int y, out byte r, out byte g, out byte b, out byte a)
        {
            int idx = (y * width_ * 4) + (x * 4);
            if(idx + ImageConversions.ALPHA_IDX < (width_ * height_ * 4))
            {
                r = data[idx + ImageConversions.RED_IDX];
                g = data[idx + ImageConversions.GREEN_IDX];
                b = data[idx + ImageConversions.BLUE_IDX];
                a = data[idx + ImageConversions.ALPHA_IDX];
            }
            else
            {
                r = 0; g = 0; b = 0; a = 0;
            }
        }
        public static void SetPixel(byte[] data, int width_, int height_, int x, int y, byte r, byte g, byte b, byte a = 255)
        {
            int idx = (y * width_ * 4) + (x * 4);
            if (idx + ImageConversions.ALPHA_IDX < (width_ * height_ * 4))
            {
                data[idx + ImageConversions.RED_IDX] = r;
                data[idx + ImageConversions.GREEN_IDX] = g;
                data[idx + ImageConversions.BLUE_IDX] = b;
                data[idx + ImageConversions.ALPHA_IDX] = a;
            }
        }

        public void SetAllRed(byte value)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    SetRedPixel(x, y, value);
                }
            }
        }
        public void SetAllGreen(byte value)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    SetGreenPixel(x, y, value);
                }
            }
        }
        public void SetAllBlue(byte value)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    SetBluePixel(x, y, value);
                }
            }
        }

        public ImageDataArr GetSquare(int posX, int posY, int squareW, int squareH)
        {
            if (PixelFormat == PixelFormat.Format32bppArgb)
            {
                byte[] square = new byte[squareW * squareH * 4];
                int idx = 0;
                int rc = 0, gc = 0, bc = 0;
                int num = 0;
                for (int y = posY; y < posY + squareH; y++)
                {
                    for (int x = posX; x < posX + squareW; x++)
                    {
                        if (y < height && x < width)
                        {
                            int dataIdx = (y * Width * 4) + (x * 4);
                            byte b = imageData[dataIdx + ImageConversions.BLUE_IDX];
                            byte g = imageData[dataIdx + ImageConversions.GREEN_IDX];
                            byte r = imageData[dataIdx + ImageConversions.RED_IDX];
                            bc += b;
                            gc += g;
                            rc += r;
                            square[idx++] = b;
                            square[idx++] = g;
                            square[idx++] = r;
                            square[idx++] = imageData[dataIdx + ImageConversions.ALPHA_IDX];
                            num++;
                        }
                        else
                        {
                            square[idx++] = 127;
                            square[idx++] = 127;
                            square[idx++] = 127;
                            square[idx++] = 255;
                        }
                    }
                }
                rc /= num; gc /= num; bc /= num;
                return new ImageDataArr(square, squareW, squareH, PixelFormat, (byte)rc, (byte)gc, (byte)bc);
            }
            else throw new NotImplementedException();
        }

        public void SetSquare(ImageDataArr square, int posX, int posY)
        {
            if (PixelFormat == PixelFormat.Format32bppArgb)
            {
                int idx = 0;
                for (int y = posY; y < posY + square.Height; y++)
                {
                    for (int x = posX; x < posX + square.Width; x++)
                    {
                        if (y < height && x < width)
                        {
                            int dataIdx = (y * Width * 4) + (x * 4);
                            imageData[dataIdx + ImageConversions.BLUE_IDX] = square.imageData[idx++];
                            imageData[dataIdx + ImageConversions.GREEN_IDX] = square.imageData[idx++];
                            imageData[dataIdx + ImageConversions.RED_IDX] = square.imageData[idx++];
                            imageData[dataIdx + ImageConversions.ALPHA_IDX] = square.imageData[idx++];
                        }
                        else
                        {
                            idx += 4;
                        }
                    }
                }
            }
            else throw new NotImplementedException();
        }

        void CalcAverages()
        {
            double rc = 0, gc = 0, bc = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    GetPixel(x, y, out byte r, out byte g, out byte b, out byte a);
                    rc += r;
                    gc += g;
                    bc += b;
                }
            }
            double num_pix = Width * Height;

            this.ravg = (byte)Math.Round(rc / num_pix);
            this.gavg = (byte)Math.Round(gc / num_pix);
            this.bavg = (byte)Math.Round(bc / num_pix);
        }

        public Bitmap ToBitmap()
        {
            Bitmap image = new Bitmap(Width, Height);
            byte[] warr = new byte[0];
            return ImageConversions.LoadBitmapData(true, ref image, imageData, image.Width, image.Height, ref warr, 1);
        }

        public void Scale(double scale_factor)
        {
            Resize((int)Math.Round(Width * scale_factor), (int)Math.Round(Height * scale_factor));
        }

        public void Resize(int newW, int newH)
        {

            double divx = (double)Width / newW;
            double divy = (double)Height / newH;

            byte[] workarray = new byte[newW * newH * 4];

            for(int y = 0; y < newH; y++)
            {
                for(int x = 0; x < newW; x++)
                {
                    int xo = (int)Math.Round(x * divx);
                    if (xo >= Width) xo--;
                    int yo = (int)Math.Round(y * divy);
                    if (yo >= Height) yo--;

                    GetPixel(xo, yo, out byte r, out byte g, out byte b, out byte a);
                    SetPixel(workarray, newW, newH, x, y, r, g, b, a);
                }
            }


            width = newW;
            height = newH;

            imageData = workarray;
        }

        public void ResetImage(byte[] newArr, int w, int h, int newW, int newH)
        {

            double divx = (double)w / newW;
            double divy = (double)h / newH;

            byte[] workarray = new byte[newW * newH * 4];

            for (int y = 0; y < newH; y++)
            {
                for (int x = 0; x < newW; x++)
                {
                    int xo = (int)Math.Round(x * divx);
                    if (xo >= Width) xo--;
                    int yo = (int)Math.Round(y * divy);
                    if (yo >= Height) yo--;

                    GetPixel(newArr, w, h, xo, yo, out byte r, out byte g, out byte b, out byte a);
                    SetPixel(workarray, newW, newH, x, y, r, g, b, a);
                }
            }


            width = newW;
            height = newH;

            imageData = workarray;
        }
        public static void Resize(byte[] data, ref byte[] newData, int w, int h , int newW, int newH)
        {

            double divx = (double)w / newW;
            double divy = (double)h / newH;

            newData = new byte[newW * newH * 4];

            for (int y = 0; y < newH; y++)
            {
                for (int x = 0; x < newW; x++)
                {
                    int xo = (int)Math.Round(x * divx);
                    if (xo >= w) xo--;
                    int yo = (int)Math.Round(y * divy);
                    if (yo >= h) yo--;

                    GetPixel(data, w, h, xo, yo, out byte r, out byte g, out byte b, out byte a);
                    SetPixel(newData, newW, newH, x, y, r, g, b, a);
                }
            }
        }
        public ImageDataArr Clone(bool calc_averages)
        {
            byte[] cloneArr = new byte[Width * Height * 4];
            for (int i = 0; i < imageData.Length; i++)
            {
                cloneArr[i] = imageData[i];
            }

            return new ImageDataArr(cloneArr, Width, Height, PixelFormat, calc_averages);
            //return new ImageDataArr(imageData, Width, Height, PixelFormat, calc_averages);
        }
    }
}
