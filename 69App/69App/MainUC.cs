using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _69App
{
    public partial class MainUC : UserControl
    {

        public Bitmap UpperImage { get { return (Bitmap)pictureBoxUp.Image; } set { pictureBoxUp.Image = value; } }
        public Bitmap LowerImage { get { return (Bitmap)pictureBoxLo.Image; } set { pictureBoxLo.Image = value; } }

        public Size UpperPicBoxSize { get { return pictureBoxUp.Size; } }

        public MainUC()
        {
            InitializeComponent();
        }

    }
}
