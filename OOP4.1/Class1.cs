using System;
using System.Drawing;


namespace OOP4._1
{
    public class Base
    {
		public virtual char getCode()
        {
            return 'B';
        }
	    public virtual void print()
        {

        }
    }
    class MyBaseFactory
	{
        private const char V = 'C';

        public MyBaseFactory() { }
		public Base createBase(Base p)
		{
			Base _base = null;
			switch (p.getCode())
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
                base_ = factory.createBase(_base);
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
            if (first == null) return 0;
            Node node = first;
            int i = 1;
            while (node!=null)
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
            tmp = factory.createBase(ret);
            deleteObj(ret);
            Console.WriteLine("\tОбъект передан\n");
            return tmp;
        }
    };


    public class CCircle : Base
    {
        private int x, y;
        private int R = 15;
        public bool Selected = false;
        public override char getCode()
        {
            return 'C';
        }

        public CCircle(int x, int y,Mylist mylist)
        {
            bool flag = true;
            int i;
            double tmp = 0 ;
            for ( i=1; i < mylist.getSize()+1; i++)
            {
                tmp = Math.Pow((((CCircle)mylist.getObj(i)).getX() - x), 2) + Math.Pow(((CCircle)mylist.getObj(i)).getY() - y, 2);
                if (tmp <= (4*R*R))
                {
                    flag = false;
                    
                    break;
                } 
            }
            if (flag)
            {
                this.x = x;
                this.y = y;
                mylist.add(this);
            }
            else
            {
                if (tmp < R * R)
                {
                    ((CCircle)mylist.getObj(i)).Selected = true;
                }
            }
        }
        public CCircle(CCircle copy)
        {
            x = copy.getX();
            y = copy.getY();
        }
        public int getX()
        {
            return x;
        }
        public int getY()
        {
            return y;
        }
        public int getR()
        {
            return R;
        }
    }

    class Paint
    {
        Bitmap bitmap;
        Pen blackpen;
        Pen redpen;
        Pen darkGoldpen;
        Graphics gr;
        Font font_;
        Brush br;
        PointF point;

        public int R = 15;
        
        public Paint(int width, int height)
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
            font_ = new Font("Arial", 10);
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
        //отрисовка всех объектов
        {
            if (mylist.getSize() == 0) 
                return;
            for (int i = 1; i <=mylist.getSize(); i++)
            {
                R = ((CCircle)mylist.getObj(i)).getR();
                drawCircle(((CCircle)mylist.getObj(i)).getX(),((CCircle)mylist.getObj(i)).getY(),(i-1).ToString());
                if (((CCircle)mylist.getObj(i)).Selected)
                {
                    drawSelectedVert(((CCircle)mylist.getObj(i)).getX(), ((CCircle)mylist.getObj(i)).getY());
                }
            }
            
        }

    }
};
