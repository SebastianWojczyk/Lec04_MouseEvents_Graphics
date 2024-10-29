using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _08_MouseEvents_Graphics
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private Pen pen;
        private Point pointTMP;
        private const string filters = "Bitmap|*.bmp|PNG File|*.png|Jpeg|*.jpg";

        public Form1()
        {
            InitializeComponent();

            newToolStripMenuItem_Click(null, null);

            pen = new Pen(Color.Black);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            pointTMP = Point.Empty;
        }

        private void pictureBoxImage_MouseMove(object sender, MouseEventArgs e)
        {   this.Text = e.Location.ToString() + " " + e.Button.ToString();
            if(e.Button == MouseButtons.Left)
            {
                //graphics.DrawEllipse(pen, e.X, e.Y, 10, 10);
                graphics.DrawLine(pen, pointTMP, e.Location);
                pointTMP = e.Location;
            }
            pictureBoxImage.Refresh();
        }
        private void pictureBoxImage_MouseDown(object sender, MouseEventArgs e)
        {   this.Text = e.Location.ToString() + " " + e.Button.ToString();
            if (e.Button == MouseButtons.Left)
            {
                pointTMP = e.Location;
            }
        }
        private void pictureBoxImage_MouseUp(object sender, MouseEventArgs e)
        {   this.Text = e.Location.ToString() + " " + e.Button.ToString();
            if (e.Button == MouseButtons.Left)
            {
                pointTMP = Point.Empty;
            }
        }

        private void buttonPenColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            cd.Color = buttonPenColor.BackColor;
            if(cd.ShowDialog() == DialogResult.OK)
            {
                buttonPenColor.BackColor = cd.Color;
                pen.Color = cd.Color;
            }
        }

        private void numericUpDownPenWidth_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = (float)numericUpDownPenWidth.Value;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBoxImage.Image = new Bitmap(pictureBoxImage.Width, pictureBoxImage.Height);
            graphics = Graphics.FromImage(pictureBoxImage.Image);
            graphics.Clear(Color.White);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = filters;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBoxImage.Image = new Bitmap(ofd.FileName);
                graphics = Graphics.FromImage(pictureBoxImage.Image);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = filters;
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                if (sfd.FileName.EndsWith("bmp"))
                {
                    pictureBoxImage.Image.Save(sfd.FileName, ImageFormat.Bmp);
                }
                else if (sfd.FileName.EndsWith("png"))
                {
                    pictureBoxImage.Image.Save(sfd.FileName, ImageFormat.Png);
                }
                else if (sfd.FileName.EndsWith("jpg"))
                {
                    pictureBoxImage.Image.Save(sfd.FileName, ImageFormat.Jpeg);
                }
            }
        }
    }
}
