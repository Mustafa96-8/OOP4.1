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
        Paint p;
        
        public Form1()
        {
            InitializeComponent();
            lists = new Mylist();
            p = new Paint(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = p.GetBitmap();
        }
        public void CreateCCircle(object sender,MouseEventArgs e)
        {
            CCircle circle = new CCircle(e.X,e.Y,lists);
            p.PaintDraw(lists);
            pictureBox1.Image = p.GetBitmap();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            p.PaintDraw(lists);
            pictureBox1.Image = p.GetBitmap();
            
        }
    }
}
