using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP4._1
{
    public partial class Form1 : Form
    {
        Mylist lists;

        Bitmap bitmap;
        Graphics gr ;
        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gr = Graphics.FromImage(bitmap);
            
            lists = new Mylist();
            pictureBox1.Image = GetBitmap();
        }
        public void CreateCCircle(object sender,MouseEventArgs e)
        {
            CCircle circle = new CCircle(e.X,e.Y,lists);
            PaintDraw();
            pictureBox1.Image = GetBitmap();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            PaintDraw();
            pictureBox1.Image = GetBitmap();
            
        }

        public void PaintDraw()
        //отрисовка всех объектов
        {
            if (lists.getSize() == 0)
                return;
            for (int i = 1; i <= lists.getSize(); i++)
            {
                lists.getObj(i).print(i,gr);
            }
            
        }
        public Bitmap GetBitmap()
        {
            return bitmap;
        }

    }
}
