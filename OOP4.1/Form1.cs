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
        public Form1()
        {
            InitializeComponent();
            lists = new Mylist();
        }
        public void CreateCCircle(object sender,MouseEventArgs e)
        {
            //CCircle circle = new CCircle(e.X,e.Y);
            //lists.add(circle);
        }






    }
}
