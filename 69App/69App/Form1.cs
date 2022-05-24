using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _69App
{
    public partial class SixtyNineApp : Form
    {
        List<Pixel69> pixel69s;
        int _SIZE_ = 100;
        int size_step = 10;

        ImageTo69s imageTo69;
        Bitmap image;
        ImageDataArr imageArr;
        string filename;

        public SixtyNineApp()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.Text = "69App";

            filename = "../../../../mr_bean.jpg";
            InitStuff();
        }


        void InitStuff()
        {
            pixel69s = LoadPixel69s();

            image = GetImage(filename);
            _SIZE_ = image.Width <= image.Height ? image.Width / 20 : image.Height / 20;

            Size picBoxSize = mainUC.UpperPicBoxSize;

            // resize window to fit image more or less
            double windowPbDiff = (double)Size.Width - picBoxSize.Width;
            double imageWhRatio = (double)image.Width / image.Height;
            double PbNewW = picBoxSize.Height * imageWhRatio;
            Size = new Size((int)(PbNewW + windowPbDiff), Size.Height);
            
            mainUC.UpperImage = image;
            try
            {

                imageTo69 = new ImageTo69s(pixel69s);

                imageArr = new ImageDataArr(image, false);

                Bitmap imageConverted = imageTo69.Convert(imageArr, _SIZE_);

                mainUC.LowerImage = imageConverted;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        Bitmap GetImage(string path)
        {
            Bitmap image = new Bitmap(path);
            Bitmap clone = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);

            using (Graphics gr = Graphics.FromImage(clone))
            {
                gr.DrawImage(image, new Rectangle(0, 0, clone.Width, clone.Height));
            }

            image = clone;

            return image;
        }

        List<Pixel69> LoadPixel69s()
        {
            List<Pixel69> p69s = new List<Pixel69>();
            int toSize = _SIZE_;
            var list = Directory.EnumerateFiles("../../../../resized_pics/", "*.jpg").ToList();

            p69s.Add(new Pixel69(list[0]));
            int w = p69s[0].ImageArr.Width;
            int h = p69s[0].ImageArr.Height;

            list.RemoveAt(0);
            foreach (string file in list)
            {
                var p69 = new Pixel69(file);

                if(p69.ImageArr.Width != w || p69.ImageArr.Height != h)
                {
                    MessageBox.Show("Image skipped because it's not the same size. Check in folder 'resized_pics'!", "Error");
                }
                else
                {
                    p69s.Add(new Pixel69(file));
                }
            }
            return p69s;
        }


        void SavePixel69s(List<Pixel69> pixel69s, string path)
        {
            for (int i = 0; i < pixel69s.Count; i++)
            {
                pixel69s[i].Save(path + "/" + (i + 1) + ".jpg", ImageFormat.Jpeg);
            }
            return;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            //try
            {
                if (_SIZE_ < 40) size_step = 4;
                else size_step = 10;
                if (_SIZE_ - size_step > 4)
                {
                    _SIZE_ -= size_step;
                    Bitmap imageConverted = imageTo69.Convert(imageArr, _SIZE_);
                    mainUC.LowerImage = imageConverted;
                }
                else
                {
                    _SIZE_ = 4;
                    Bitmap imageConverted = imageTo69.Convert(imageArr, _SIZE_);
                    mainUC.LowerImage = imageConverted;
                }
            }
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            //try
            {
                if (_SIZE_ < 40) size_step = 3;
                else size_step = 10;
                _SIZE_ += size_step;

                Bitmap imageConverted = imageTo69.Convert(imageArr, _SIZE_);
                mainUC.LowerImage = imageConverted;
            }
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        int cntr = 0;
        private void btnSave_Click(object sender, EventArgs e)
        {

            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        this.mainUC.LowerImage.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        this.mainUC.LowerImage.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        this.mainUC.LowerImage.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                //pictureBoxL.Image = new Bitmap(open.FileName);
                //mainUC.UpperImage = new Bitmap(open.FileName);
                // image file path
                Text = open.FileName;
                filename = open.FileName;
                InitStuff();
            }
        }
    }
}
