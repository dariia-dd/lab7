using System;
using System.Collections;
using System.Threading;

namespace L71
{
    class Program
    {
        public static void Main(string[] args)
        {

            Goods a1 = new Goods(355, "Пральна машина", 200, 32, 500);
            Goods a2 = new Goods(3499, "Ванна", 100, 12, 677);
            Goods a3 = new Goods(5335, "Плойка", 230, 2, 434);
            Goods a4 = new Goods(3455, "Фен", 400, 3, 346);
            Goods a5 = new Goods(7351, "Стiл", 200, 10, 134);
            Goods a6 = new Goods(2544, "Дзеркало", 500, 1, 653);
            Goods a7 = new Goods(3145, "Ковдра", 206, 24, 732);
            Goods a8 = new Goods(1735, "Диван", 350, 35,137);
            Goods a9 = new Goods(3445, "Крiсло", 200, 2, 316);
            Goods a10 = new Goods(1035, "Доска", 250, 17, 724);
            Goods[] n = new Goods[10];
            n[0] = a1;
            n[1] = a2;
            n[2] = a3;
            n[3] = a4;
            n[4] = a5;
            n[5] = a6;
            n[6] = a7;
            n[7] = a8;
            n[8] = a9;
            n[9] = a10;
            Array.Sort(n);
            Console.WriteLine("Сортування за цiною:\n");
            Console.WriteLine("{0, -15}{1, -10}{2, -15}{3, -15}{4, -10}", "Товар", "Цiна", "Площа", "Гарантiя", "Розмiр");
            for (int i = 0; i < n.Length; i++)
            {
                Console.WriteLine("{0, -15}{1, -10}{2, -15}{3, -15}{4, -10}", n[i].Name, n[i].Price, n[i].Area, n[i].Age, n[i].Size);
            }
            Console.WriteLine("Сортування за розмiром:\n");
            Console.WriteLine("{0, -15}{1, -10}{2, -15}{3, -15}{4, -10}", "Товар", "Цiна", "Площа", "Гарантiя", "Розмiр");
            Array.Sort(n, new Goods.SortBySize());
            foreach (Goods elem in n) elem.Info1();

        }
    }


    public class Goods : IComparable
    {
        public string Name;
        public int Price;
        public int Age;
        public int Area;
        public int Size;

        public Goods(int P, string N, int H, int A, int S)
        {
            Name = N;
            Price = P;
            Area = H;
            Age = A;
            Size = S;
        }


        public class SortBySize : IComparer
        {

            int IComparer.Compare(object ob1, object ob2)
            {
                Goods p1 = (Goods)ob1;
                Goods p2 = (Goods)ob2;
                if (p1.Size > p2.Size) return 1;
                if (p1.Size < p2.Size) return -1;
                return 0;
            }
        }
        public class SortByPrice : IComparer
        {

            int IComparer.Compare(object ob1, object ob2)
            {
                Goods p1 = (Goods)ob1;
                Goods p2 = (Goods)ob2;
                if (p1.Price > p2.Price) return 1;
                if (p1.Price < p2.Price) return -1;
                return 0;
            }
        }
        public int CompareTo(object obj)
        {
            Goods a = obj as Goods;
            if (a != null)
            {
                if (this.Price < a.Price)
                    return -1;
                else if (this.Price > a.Price)
                    return 1;
                else
                    return 0;
            }
            else
            {
                throw new Exception("Параметр повинен бути типу Goods!");
            }

        }

        public void Info1()
        {
            Console.WriteLine("{0, -15}{1, -10}{2, -15}{3, -15}{4, -10}", Name, Price, Area, Age, Size);
        }

        public class Good : IEnumerable
        {
            public Goods[] an;
            public Good(Goods[] array)
            {
                an = new Goods[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    an[i] = array[i];
                }
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)GetEnumerator();
            }
            public ANNN GetEnumerator()
            {
                return new ANNN(an);
            }
        }
        public class ANNN : IEnumerator
        {
            public Goods[] an;
            int pos = -1;
            public ANNN(Goods[] list)
            {
                an = list;
            }
            public bool MoveNext()
            {
                pos++;
                return (pos < an.Length);
            }
            public void Reset()
            {
                pos = -1;
            }
            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }
            public Goods Current
            {
                get
                {
                    try
                    {
                        return an[pos];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }
}
