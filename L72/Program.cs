using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.IO;
namespace L72
{
    class Program
    {
        static void Main(string[] args)
        {
            Key.KKEY();
        }
    }
    class Patients : IComparable
    {
        public string Surname;
        public string Name;
        public int Num;
        public string Login;
        public string Sex;
        public int Year;
        public int N;
        public Patients(string sur, string name, int num, string login, string pass, int year, int d)
        {
            Surname = sur;
            Name = name;
            Num = num;
            Login = login;
            Sex = pass;
            Year = year;
            N = d;
        }
        public int CompareTo(object user)
        {
            Patients p = (Patients)user;
            if (this.Year > p.Year) return 1;
            if (this.Year < p.Year) return -1;
            return 0;
        }
        public void Studak(Patients[] a)
        {
            Console.WriteLine("\nСортування за роком:\nПрiзвище\t Рiк");
            Array.Sort(a);
            foreach (Patients elem in a) elem.Inf();
        }
        public void Inf()
        {
            Console.WriteLine("{0,-20} {1, -10} ", Surname, Year);
        }

        public class SortByNumber : IComparer
        {
            int IComparer.Compare(object ob1, object ob2)
            {
                Patients p1 = (Patients)ob1;
                Patients p2 = (Patients)ob2;
                if (p1.Num > p2.Num) return 1;
                if (p1.Num < p2.Num) return -1;
                return 0;
            }
        }
        public void First(Patients[] a)
        {
            Console.WriteLine("\nСортування за кiлькiстю дiтей:\nПрiзвище\t Кiлькiсть");
            Array.Sort(a, new Patients.SortByNumber());
            foreach (Patients elem in a) elem.Info();
        }
        public void Info()
        {
            Console.WriteLine("{0,-20} {1, -10} ", Surname, Num);
        }
        public class SortByYear : IComparer
        {
            int IComparer.Compare(object ob1, object ob2)
            {
                Patients p1 = (Patients)ob1;
                Patients p2 = (Patients)ob2;
                if (p1.Year > p2.Year) return 1;
                if (p1.Year < p2.Year) return -1;
                return 0;
            }
        }
        public void Second(Patients[] a)
        {
            Console.WriteLine("\nСортування за роком:\nПрiзвище\t Рiк");
            Array.Sort(a, new Patients.SortByYear());
            foreach (Patients elem in a) elem.Info1();
        }
        public void Info1()
        {
            Console.WriteLine("{0,-15} {1, -10} ", Surname, Year);
        }
        public void Add()
        {
            Console.WriteLine("Write data:");

            string str = Console.ReadLine();

            string[] elements = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        }
    }
    class Key
    {
        public static void KKEY()
        {
            FileStream file1 = File.OpenRead("person.txt");
            byte[] array = new byte[file1.Length];
            file1.Read(array, 0, array.Length);
            string textfromfile = System.Text.Encoding.Default.GetString(array);
            string[] s = textfromfile.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            file1.Close();
            Patients[] a = new Patients[s.Length / 7];
            int c = 0;
            while (a[c] != null)
            {
                ++c;
            }
            for (int i = 0; i < s.Length; i += 7)
            {
                a[c + i / 7] = new Patients(s[i], s[i + 1], int.Parse(s[i + 2]), s[i + 3], s[i + 4], int.Parse(s[i + 5]), int.Parse(s[i + 6]));
            }
            bool[] delete = new bool[100];
            Console.WriteLine("Add note: A");
            Console.WriteLine("Edit note: E");
            Console.WriteLine("Remove note: R");
            Console.WriteLine("Show notes: Enter");
            Console.WriteLine("Sort by number: N");
            Console.WriteLine("Sort by year: D");
            Console.WriteLine("Sort by year1: S");
            Console.WriteLine("Exit: Esc");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.E:
                    Key.Edit(a);
                    break;

                case ConsoleKey.N:
                    a[0].First(a);
                    Key.KKEY();
                    break;

                case ConsoleKey.D:
                    a[0].Second(a);
                    Key.KKEY();
                    break;

                case ConsoleKey.S:
                    a[0].Studak(a);
                    Key.KKEY();
                    break;

                case ConsoleKey.Enter:
                    Key.Show(a);
                    break;

                case ConsoleKey.A:
                    Key.Add(a, c);
                    break;

                case ConsoleKey.R:
                    Key.Remove(a, delete);
                    break;

                case ConsoleKey.Escape:
                    break;
            }

        }
        public static void Show(Patients[] a)
        {
            Console.WriteLine("{0,-15} {1, -15}\t {2, -10} {3, -20} {4,-20} {5,-20}{6,-20}", "Номер", "Прiзвище", "Iм'я", "По батьковi", "Дiти", "Стать", "Рiк народження");

            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i] != null)
                {
                    Console.WriteLine("{0,-15} {1, -10}\t {2, -10} {3, -20} {4,-20} {5,-15}{6,-20}", a[i].N, a[i].Surname, a[i].Name, a[i].Login, a[i].Num, a[i].Sex, a[i].Year);
                }
            }
            Key.KKEY();
        }
        public static void Add(Patients[] a, int c)
        {
            Console.WriteLine("\nWrite data:");

            string str = Console.ReadLine();

            string[] elements = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            Key.Parse(elements, true, a, c);
            Key.KKEY();
        }

        private static void Save(Patients m)
        {
            StreamWriter save = new StreamWriter("person.txt", true);

            save.WriteLine(m.Surname);
            save.WriteLine(m.Name);
            save.WriteLine(m.Num);
            save.WriteLine(m.Login);
            save.WriteLine(m.Sex);
            save.WriteLine(m.Year);
            save.WriteLine(m.N);
            save.Close();
        }

        public static void Parse(string[] elements, bool save, Patients[] a, int counter)
        {
            for (int i = 0; i < elements.Length; i += 7)
            {
                a[counter + i / 5] = new Patients(elements[i], elements[i + 1], int.Parse(elements[i + 2]), elements[i + 3], elements[i + 4], int.Parse(elements[i + 5]), int.Parse(elements[i + 6]));
                if (save)
                {
                    Save(a[counter + i / 5]);
                }
            }
        }
        public static void Remove(Patients[] a, bool[] delete)
        {
            Console.Write("\nSurname: ");

            string name = Console.ReadLine();

            bool[] write = new bool[a.Length];
            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i] != null)
                {
                    if (a[i].Surname == name)
                    {
                        Console.WriteLine("{0,-15} {1, -10}\t {2, -10} {3, -20} {4,-20} {5,-15}{6,-20}", a[i].N, a[i].Surname, a[i].Name, a[i].Login, a[i].Num, a[i].Sex, a[i].Year);

                        Console.WriteLine("\nDELETE? (Y/N)\n");

                        var key = Console.ReadKey().Key;

                        if (key == ConsoleKey.Y)
                        {
                            a[i] = null;
                            delete[i] = true;
                            Key.Show(a);
                        }
                        else
                        {
                            delete[i] = false;
                        }
                    }
                }
            }
            Key.KKEY();
        }
        public static void Edit(Patients[] a)
        {
            Console.WriteLine("\nWhat do you want to edit?(Surname, Name, Year, Middle, Sex, Number)");
            string what = Console.ReadLine();
            switch (what)
            {
                case "Surname":
                    Console.WriteLine("What surname: ");
                    string name1 = Console.ReadLine();
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Surname == name1)
                            {
                                Console.WriteLine("{0,-15} {1, -10}\t {2, -10} {3, -20} {4,-20} {5,-15}{6,-20}", a[i].N, a[i].Surname, a[i].Name, a[i].Login, a[i].Num, a[i].Sex, a[i].Year);

                                Console.WriteLine("New surname: ");

                                string str = Console.ReadLine();

                                a[i].Surname = str;

                                Key.Show(a);
                            }
                        }
                    }
                    break;

                case "Name":
                    Console.WriteLine("What name: ");
                    string name2 = Console.ReadLine();
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Name == name2)
                            {
                                Console.WriteLine("{0,-15} {1, -10}\t {2, -10} {3, -20} {4,-20} {5,-15}{6,-20}", a[i].N, a[i].Surname, a[i].Name, a[i].Login, a[i].Num, a[i].Sex, a[i].Year);

                                Console.WriteLine("New group: ");
                                string str = Console.ReadLine();
                                a[i].Name = str;
                                Key.Show(a);
                            }
                        }
                    }
                    break;
                case "Middle":
                    Console.WriteLine("What middle name: ");
                    string name3 = Console.ReadLine();
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Login == name3)
                            {
                                Console.WriteLine("{0,-15} {1, -10}\t {2, -10} {3, -20} {4,-20} {5,-15}{6,-20}", a[i].N, a[i].Surname, a[i].Name, a[i].Login, a[i].Num, a[i].Sex, a[i].Year);
                                Console.WriteLine("New middle name: ");
                                string str = Console.ReadLine();
                                a[i].Login = str;
                                Key.Show(a);
                            }
                        }

                    }
                    break;
                case "Sex":
                    Console.WriteLine("What sex: ");
                    string name4 = Console.ReadLine();
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Sex == name4)
                            {
                                Console.WriteLine("{0,-15} {1, -10}\t {2, -10} {3, -20} {4,-20} {5,-15}{6,-20}", a[i].N, a[i].Surname, a[i].Name, a[i].Login, a[i].Num, a[i].Sex, a[i].Year);
                                Console.WriteLine("New sex: ");
                                string str = Console.ReadLine();
                                a[i].Sex = str;
                                Key.Show(a);
                            }
                        }
                    }
                    break;
                case "Number":
                    Console.WriteLine("What Year: ");
                    int name5 = int.Parse(Console.ReadLine());
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Year == name5)
                            {
                                Console.WriteLine("{0,-15} {1, -10}\t {2, -10} {3, -20} {4,-20} {5,-15}{6,-20}", a[i].N, a[i].Surname, a[i].Name, a[i].Login, a[i].Num, a[i].Sex, a[i].Year);
                                Console.WriteLine("New Year: ");
                                int str = int.Parse(Console.ReadLine());
                                a[i].Year = str;
                                Key.Show(a);
                            }
                        }
                    }
                    break;
                case "Course":
                    Console.WriteLine("What number: ");
                    int name6 = int.Parse(Console.ReadLine());
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Num == name6)
                            {
                                Console.WriteLine("{0,-15} {1, -10}\t {2, -10} {3, -20} {4,-20} {5,-15}{6,-20}", a[i].N, a[i].Surname, a[i].Name, a[i].Login, a[i].Num, a[i].Sex, a[i].Year);

                                Console.WriteLine("New number: ");
                                int str = int.Parse(Console.ReadLine());
                                a[i].Num = str;
                                Key.Show(a);
                            }
                        }

                    }
                    break;
            }
            Key.KKEY();
        }
    }
}
