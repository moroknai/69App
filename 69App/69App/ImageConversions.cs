using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Utilities.Media.Image;

namespace _69App
{
    public class ImageConversions
    {
        public const int ALPHA_IDX = 3;
        public const int RED_IDX = 2;
        public const int GREEN_IDX = 1;
        public const int BLUE_IDX = 0;

        /// <summary>
        /// Loads byte[] data into a bitmap
        /// </summary>
        /// <param name="CreateBitmapAlways">Always creates a new bitmap</param>
        /// <param name="bitmap">Ext ref to a bitmap object - can be null</param>
        /// <param name="data">input data (camera byte[] data</param>
        /// <param name="width">input data dimension - width/pitch </param>
        /// <param name="height">input data dimension - height</param>
        /// <param name="workarray">Temp work array to generate Format32bppRgb img </param>
        /// <param name="div">Scale down / divider value can be 1, 2, 4 ... </param>
        /// <returns>Original bitmap - or new one</returns>
        public static Bitmap LoadBitmapData(bool CreateBitmapAlways, ref Bitmap bitmap, byte[] data, int width, int height, ref byte[] workarray, int div = 1)
        {
            //try
            //{

                int w = width / div;
                int h = height / div;

                // Create a bitmap if needed
                if (CreateBitmapAlways)
                {
                    bitmap = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                }
                else
                {
                    if (bitmap == null)  // if null need to create
                    {
                        bitmap = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    }
                    else if ((bitmap.Width != w) || (bitmap.Height != h))  // if size is worng - need to create
                    {
                        bitmap = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    }
                }
                // temp buffer - create if null or size mismatch 
                if (workarray == null)
                {
                    workarray = new byte[w * h * 4];
                }
                else if (workarray.Length != w * h * 4)
                {
                    workarray = new byte[w * h * 4];
                }

                int byteidx = 0;

                int x = 0;  // scaled bitmap x - y coords
                int y = 0;  // scaled bitmap x - y coords
                for (int i32 = 0; (i32 < workarray.Length); i32 += 4) // step by 4 !!!
                {
                    byteidx = x * div + (width * 4) * (y * div);  // scale it back to the original 
                    //  if (byteidx < data.Length)
                    {
                        workarray[i32 + RED_IDX] = data[byteidx + RED_IDX];
                        workarray[i32 + GREEN_IDX] = data[byteidx + GREEN_IDX];
                        workarray[i32 + BLUE_IDX] = data[byteidx + BLUE_IDX];
                        workarray[i32 + ALPHA_IDX] = data[byteidx + ALPHA_IDX];
                    }
                    // update pixel pos
                    x += 4;
                    if (x >= (w * 4))   // new line
                    {
                        x = 0;
                        y++;
                    }
                }

                // Do the windows thing - copy bitmap
                Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

                System.Drawing.Imaging.BitmapData bmpData =
                                 bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                                 bitmap.PixelFormat);

                int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
                IntPtr ptr = bmpData.Scan0;
                System.Runtime.InteropServices.Marshal.Copy(workarray, 0, ptr, bytes);

                bitmap.UnlockBits(bmpData);
            //}
            //catch
            //{
            //    ;
            //}
            return bitmap;
        }


        /// <summary>
        /// Copies bitmap bytes from a Bitmap to a byteData[] array.
        /// </summary>
        /// <param name="inputBmp">Input Bitmap</param>
        /// <param name="byteData">Should be supplied by the caller - byte data copied out from the bmp object</param>
        public static void BmpToData(Bitmap inputBmp, ref byte[] byteData)
        {
            //if (inputBmp.PixelFormat == PixelFormat.Format24bppRgb) byteData = new byte[inputBmp.Width * inputBmp.Height * 3];
            //else if (inputBmp.PixelFormat == PixelFormat.Format32bppPArgb) byteData = new byte[inputBmp.Width * inputBmp.Height * 4];
            //else if (inputBmp.PixelFormat == PixelFormat.Format32bppArgb) byteData = new byte[inputBmp.Width * inputBmp.Height * 4];
            //else if (inputBmp.PixelFormat == PixelFormat.Format32bppRgb) byteData = new byte[inputBmp.Width * inputBmp.Height * 4];
            //else if (inputBmp.PixelFormat == PixelFormat.Format8bppIndexed) byteData = new byte[inputBmp.Width * inputBmp.Height * 1];

            Rectangle rect = new Rectangle(0, 0, inputBmp.Width, inputBmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                        inputBmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                         inputBmp.PixelFormat);
            int bytes = Math.Abs(bmpData.Stride) * inputBmp.Height;
            byteData = new byte[bytes];
            IntPtr ptr = bmpData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(ptr, byteData, 0, bytes);
            inputBmp.UnlockBits(bmpData);
        }

        /// <summary>
        /// Loads Bitmap data into byte-array, and converts byte-array to grayscale.
        /// Only a few PixelFormats are supported for now.
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="frame_byte"></param>
        /// <param name="DebugMessage"></param>
        //public static void LoadByteData(Bitmap frame, ref byte[] frame_byte)
        //{
        //    try
        //    {
        //        frame_byte = new byte[frame.Width * frame.Height];
        //        byte[] temp = new byte[frame.Width * frame.Height * 3];

        //        BmpToData(frame, ref temp);
                
        //        switch (frame.PixelFormat)
        //        {
        //            case PixelFormat.Format8bppIndexed:
        //                frame_byte = temp;
        //                break;
        //            case PixelFormat.Format24bppRgb:
        //                ThreeBPP(temp, ref frame_byte);
        //                break;
        //            case PixelFormat.Format32bppArgb:
        //                FourBPP(temp, ref frame_byte);
        //                break;
        //            case PixelFormat.Format32bppRgb:
        //                FourBPP(temp, ref frame_byte);
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    catch(Exception ex)
        //    {
        //        ;
        //    }
        //}

        
        //static void ThreeBPP(byte[] orig, ref byte[] oneBPP)
        //{
        //    int RED_IDX = 2;
        //    int GREEN_IDX = 1;
        //    int BLUE_IDX = 0;
        //    for (int i = 0; i < orig.Length; i += 3)
        //    {
        //        oneBPP[i / 3] = (byte)((0.0722 * orig[i + BLUE_IDX]) + (0.7152 * orig[i + GREEN_IDX]) + (0.2126 * orig[i + RED_IDX]));
        //    }
        //    return ;
        //}

        //static void FourBPP(byte[] orig, ref byte[] oneBPP)
        //{
        //    int RED_IDX = 2;
        //    int GREEN_IDX = 1;
        //    int BLUE_IDX = 0;
        //    for (int i = 0; i < orig.Length; i += 4)
        //    {
        //        oneBPP[i / 4] = (byte)((0.0722 * orig[i + BLUE_IDX]) + (0.7152 * orig[i + GREEN_IDX]) + (0.2126 * orig[i + RED_IDX]));
        //    }
        //    return ;
        //}
        
        /// <summary>
        /// Cuts a rectangle out of an image that is represented by a one-dimensional byte-array
        /// </summary>
        /// <param name="image"></param>
        /// <param name="im_h"></param>
        /// <param name="im_w"></param>
        /// <param name="rect"></param>
        /// <param name="output_img"></param>
        //public static void CutRectangle(byte[] image, int im_w, int im_h, Rectangle rect, ref byte[] output_img, ref int cut_w, ref int cut_h)
        //{
        //    if ((im_w * im_h) < (rect.Width * rect.Height)) throw new Exception("rectangle is greater than image");

        //    if(ByteArrayOperations.IsRectangleInside(im_w, im_h, rect))
        //    {
        //        output_img = new byte[rect.Width * rect.Height];
        //        int pixel_cntr = 0;
        //        for (int row = rect.Y; row < rect.Y + rect.Height; row++)
        //        {
        //            for (int col = rect.X; col < rect.X + rect.Width; col++)
        //            {
        //                output_img[pixel_cntr] = ByteArrayOperations.GetPixel(ref image, im_w, im_h, col, row);
        //                pixel_cntr++;
        //            }
        //        }
        //        cut_w = rect.Width; cut_h = rect.Height;
        //    }
        //    else
        //    {
        //        Rectangle rect_new = rect;

        //        if (rect.X < 0) { rect_new.X = 0; rect_new.Width -= rect.X; }
        //        if (rect.Y < 0) { rect_new.Y = 0; rect_new.Height -= rect.Y; }
        //        if ((rect.X + rect.Width) > im_w) rect_new.Width = im_w - rect.X;
        //        if ((rect.Y + rect.Height) > im_h) rect_new.Height = im_h - rect.Y;

        //        output_img = new byte[rect_new.Width * rect_new.Height];
        //        int pixel_cntr = 0;
        //        for (int row = rect_new.Y; row < rect_new.Y + rect_new.Height; row++)
        //        {
        //            for (int col = rect_new.X; col < rect_new.X + rect_new.Width; col++)
        //            {
        //                output_img[pixel_cntr] = ByteArrayOperations.GetPixel(ref image, im_w, im_h, col, row);
        //                pixel_cntr++;
        //            }
        //        }
        //        cut_w = rect_new.Width; cut_h = rect_new.Height;
        //    }
        //}

        public static byte[] ResizePixels(byte[] image, int w1, int h1, int w2, int h2)
        {
            byte[] temp = new byte[w2 * h2];
            // EDIT: added +1 to account for an early rounding problem
            int x_ratio = (int)((w1 << 16) / w2) + 1;
            int y_ratio = (int)((h1 << 16) / h2) + 1;
            //int x_ratio = (int)((w1<<16)/w2) ;
            //int y_ratio = (int)((h1<<16)/h2) ;
            int x2, y2;
            for (int i = 0; i < h2; i++)
            {
                for (int j = 0; j < w2; j++)
                {
                    x2 = ((j * x_ratio) >> 16);
                    y2 = ((i * y_ratio) >> 16);
                    temp[(i * w2) + j] = image[(y2 * w1) + x2];
                }
            }
            return temp;
        }
        /// <summary>
        /// Cuts a rectangle shape out of a 2D image represented by a 1D byte-array. The rectangle contains the marker (m) in the middle.
        /// The sides of the rectangle are 4 * m.Radius, so it's a square.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="im_w"></param>
        /// <param name="im_h"></param>
        /// <param name="m"></param>
        /// <param name="w_new"></param>
        /// <param name="h_new"></param>
        /// <returns>image in byte array format containing the marker</returns>
        //public static byte[] CutAroundMarker(byte[] image, int im_w, int im_h, Marker m, ref int w_new, ref int h_new, ref Marker m_shifted)
        //{
        //    float xf = m.Center.X - (m.Radius * 2);
        //    float yf = m.Center.Y - (m.Radius * 2);
        //    int x = (int)Math.Round(xf, 0);
        //    int y = (int)Math.Round(yf, 0);

        //    Point top_left = new Point(x, y);
            
        //    m_shifted = new Marker(m.Radius, m.Center.X - x, m.Center.Y - y);

        //    Rectangle rect = new Rectangle(top_left, new Size((int)m.Radius * 4, (int)m.Radius * 4));
        //    byte[] cut = new byte[rect.Width * rect.Height];
        //    int cut_w = 0, cut_h = 0;

        //    CutRectangle(image, im_w, im_h, rect, ref cut, ref cut_w, ref cut_h);
        //    w_new = cut_w; h_new = cut_h;

        //    return cut;
        //}

        /// <summary>
        /// Adjusts the brightness
        /// </summary>
        /// <param name="Image">Image to change</param>
        /// <param name="Value">-255 to 255</param>
        /// <returns>A bitmap object</returns>
        //public static Bitmap AdjustBrightness(Bitmap Image, int Value)
        //{
        //    float FinalValue = (float)Value / 255.0f;
        //    Utilities.Media.Image.ColorMatrix TempMatrix = new Utilities.Media.Image.ColorMatrix
        //    {
        //        Matrix = new float[][]{
        //            new float[] {1, 0, 0, 0, 0},
        //            new float[] {0, 1, 0, 0, 0},
        //            new float[] {0, 0, 1, 0, 0},
        //            new float[] {0, 0, 0, 1, 0},
        //            new float[] {FinalValue, FinalValue, FinalValue, 1, 1}
        //        }
        //    };
        //    return TempMatrix.Apply(Image);
        //}
    }

    //public static class ImageExtensions
    //{
    //    public static byte[] ToByteArray(this Image image, ImageFormat format)
    //    {
    //        using (MemoryStream ms = new MemoryStream())
    //        {
    //            image.Save(ms, format);
    //            return ms.ToArray();
    //        }
    //    }
    //}
}
