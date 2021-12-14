using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;


namespace OOP4._1
{
    public class Base
    {
		protected char code = 'B';
		public virtual char getCode()
        {
            return code;
        }
	    public virtual void print()
        {

        }
    }
    class MyBaseFactory
	{
        private const char V = 'C';

        public MyBaseFactory() { }
		public Base createBase(char code, Base p)
		{
			Base _base = null;
			switch (code)
			{
                case 'C':
					_base = new CCircle((CCircle)p);
					break;
				default:
					break;
			}
			return _base;
		} 
	}



    public class Mylist
    {

        public class Node
        {
            public Base base_=null;
            public Node next=null; //указатель на следующую ячейку списка

            public Node(Base _base)
            {
                MyBaseFactory factory = new MyBaseFactory();
                base_ = factory.createBase(_base.getCode(), _base);
            }

            public bool isEOL() { return Convert.ToBoolean(this == null ? 1 : 0); }
        };

        public void delete_first()
        {
            if (isEmpty()) return;

            Node temp = first;
            first =temp.next;
        }
        public void delete_last()
        {
            if (isEmpty()) return;
            if (last == first)
            {
                delete_first();
                return;
            }

            Node temp =first;
            while (temp.next != last)
            {
                temp = temp.next;
            }
            temp.next = null;
            last = temp;
        }

        public Node first=null;

        public Node last=null;

        public void add(Base _base)
        {
            Node another = new Node(_base);
            //("\tЭлемент добавлен в хранилище\n");

            if (isEmpty())
            {
                first = another;
                last = another;
                return;
            }
            last.next = another;
            last = another;
        }
        public bool isEmpty()
        {
            return first == null;
        }
        public void deleteObj(Base _base)
        {
            if (isEmpty())
            {
                Console.WriteLine("\tХранилище пусто, удалить не удалось\n");
                return;
            }
            if (last.base_ == _base) {
                delete_last();
                Console.WriteLine("\tЭлемент удален\n");
                return;
            }
            if (first.base_ == _base) {
                delete_first();
                Console.WriteLine("\tЭлемент удален\n");
                return;
            }

            Node current = first;
            while (current.next != null && current.next.base_ != _base) {
                current = current.next;
            }
            if (current.next == null)
            {
                Console.WriteLine("\tТакого элемента нет в списке\n");
                return;
            }
            Node tmp_next = current.next;
            current.next =
                current.next.next;
                
            Console.WriteLine("\tЭлемент удален\n");
        }
        public void print()
        {
            Console.WriteLine("Вывод хранилища:\n[\n");
            Node current = first;
            while (!(current.isEOL()))
            {
                current.base_.print();
                current = current.next;
            }
            Console.WriteLine("]\n");
        }
        public int getSize()
        {
            if (isEmpty()) return 0;
            Node node = first;
            int i = 1;
            while (!node.next.isEOL())
            {
                i++;
                node = node.next;
            }
            return i;
        }

        public Base getObj(int i)
        {
            if (isEmpty())
            {
                Console.WriteLine("Хранилище пусто, возвращать нечего\n");
                return null;//исправить на исключение
            }
            int j = 2;
            Node current = first;
            //while (j < (i + 1) && !(current.isEOL())) {
            while (j < i && !(first.isEOL()))
            {

                current = current.next;
                j++;
            }
            Console.WriteLine("\tОбъект передан\n");
            return (current.base_);
        }
        public Base getObjAndDelete(int i)
        {
            if (isEmpty())
            {
                Console.WriteLine("\tХранилище пусто, возвращать нечего\n");
                return null;//исправить на исключение
            }
            Base ret = getObj(i);
            Base tmp;
            MyBaseFactory factory = new MyBaseFactory();
            tmp = factory.createBase(ret.getCode(), ret);
            deleteObj(ret);
            Console.WriteLine("\tОбъект передан\n");
            return tmp;
        }
    };


    public class CCircle : Base
    {
        private int x, y;
        private int R = 10;
        private new char code = 'C';

        public CCircle(int x, int y)
        {

            this.x = x;
            this.y = y;
        }
        public CCircle(CCircle copy)
        {
            x = copy.GetX();
            y = copy.GetY();
        }
        public int GetX()
        {
            return x;
        }
        public int GetY()
        {
            return y;
        }
        public int GetR()
        {
            return R;
        }
        public void print(int x, int y)
        {

        }

    }

    class DrawGraph
    {
        Bitmap bitmap;
        Pen blackpen;
        Pen redpen;
        Pen darkGoldpen;
        Graphics gr;
        Font font_;
        Brush br;
        PointF point;

        public int R = 20;
        
        public DrawGraph(int width, int height)
        {
            bitmap = new Bitmap(width, height);
            gr = Graphics.FromImage(bitmap);
            clearSheet();
            blackpen = new Pen(Color.Black);
            blackpen.Width = 2;
            redpen = new Pen(Color.Red);
            redpen.Width = 2;
            darkGoldpen = new Pen(Color.DarkGoldenrod);
            darkGoldpen.Width = 2;
            font_ = new Font("Arial", 15);
            br = Brushes.Black;
        }

        public void clearSheet()
        {
            gr.Clear(Color.White);
        }

        public Bitmap GetBitmap()
        {
            return bitmap;
        }

        public void drawCircle(int x, int y, string Num)
        {
            gr.FillEllipse(Brushes.White, (x - R), (y - R), 2 * R, 2 * R);
            gr.DrawEllipse(blackpen, (x - R), (y - R), 2 * R, 2 * R);
            point = new PointF(x - (R / 2), y - (R / 2));
            gr.DrawString(Num, font_, br, point);
        }

        public void drawSelectedVert(int x, int y)
        {
            gr.DrawEllipse(redpen, (x - R), (y - R), 2 * R, 2 * R);
        }

        public void PaintDraw(Mylist mylist)
        {
            //отрисовка рёбер
            for (int i = 0; i < mylist.getSize(); i++)
            {
                drawCircle(((CCircle)mylist.getObj(i)).GetX(),((CCircle)mylist.getObj(i)).GetY(),i.ToString());
            }
            
        }

    }
};
