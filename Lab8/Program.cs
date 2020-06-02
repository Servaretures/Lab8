using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8
{
    public class InterProfile
    {
        public string Name = "";
        public string LastName = "";
        public DateTime DateOut;
        public float FilmTime = 0;
        public int Budget = 0;
        public InterProfile(string Name = "", string LastName = "", string DateOut = "00.00.0000", float FilmTime = 0, int Budget = 0)
        {
            this.Name = Name;
            this.LastName = LastName;
            this.FilmTime = FilmTime;
            this.Budget = Budget;
        }
        public int CompareTo(InterProfile p)
        {
            return this.DateOut.CompareTo(p.Budget);
        }
        public class SortByDate : IComparer<InterProfile>
        {
            public int Compare(InterProfile p1, InterProfile p2)
            {
                if (p1.DateOut > p2.DateOut)
                    return 1;
                else if (p1.DateOut < p2.DateOut)
                    return -1;
                else
                    return 0;
            }
        }
        public class SortByTimeAndBuget : IComparer<InterProfile>
        {
            public int Compare(InterProfile p1, InterProfile p2)
            {
                if (p1.FilmTime > p2.FilmTime)
                {
                    return 1;
                }
                else if (p1.FilmTime < p2.FilmTime)
                {
                    return -1;
                }
                else if (p1.Budget > p2.Budget)
                {
                    return 1;
                }
                else if (p1.Budget < p2.Budget)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            OpenFileDialog fl = new OpenFileDialog();
            List<InterProfile> data = new List<InterProfile>();
            if (fl.ShowDialog() == DialogResult.OK)
            {
                data = ReadDate(fl.FileName);
            }
            while (true)
            {
                Console.Clear();
                Table(data);
                var k = Console.ReadKey().Key;
                if (k == ConsoleKey.A)
                {
                    Add(data);
                }
                if (k == ConsoleKey.R)
                {
                    Remove(data);
                }
                if (k == ConsoleKey.C)
                {
                    ChangeData(data);
                }
                if (k == ConsoleKey.D)
                {
                    data.Sort(new InterProfile.SortByDate());
                }
                if (k == ConsoleKey.T)
                {
                    data.Sort(new InterProfile.SortByTimeAndBuget());
                }
                SaveDate(data, fl.FileName);
            }
        }
        static void Table(List<InterProfile> v)
        {
            string[] Texts = new string[5];
            Texts[0] = "    Name    ";
            Texts[1] = " Last Name ";
            Texts[2] = " Last Date ";
            Texts[3] = " Time at site ";
            Texts[4] = " Budget ";
            Console.WriteLine($"{Texts[0]}|{Texts[1]}|{Texts[2]}|{Texts[3]}|{Texts[4]}|");
            foreach (InterProfile vg in v)
            {
                Console.WriteLine(vg.Name + s(Texts[0].Length - vg.Name.Length) + "|" +
                    vg.LastName + s(Texts[1].Length - vg.LastName.Length) + "|" +
                    vg.DateOut.Date.ToString("dd.MM.yyyy") + s(Texts[2].Length - vg.DateOut.Date.ToString("dd.MM.yyyy").Length) + "|" +
                    vg.FilmTime + s(Texts[3].Length - vg.FilmTime.ToString().Length) + "|" +
                    vg.Budget + s(Texts[4].Length - vg.Budget.ToString().Length) + "|"
                    );
            }
            Console.WriteLine("A) Add new\nR) Remove\nC) Change\nD) Sort By Date\nT) Sort by Time and Buget");
        }
        static void Add(List<InterProfile> v)
        {
            InterProfile New = new InterProfile();
            Console.WriteLine("Enter name");
            New.Name = Console.ReadLine();
            Console.WriteLine("Enter Last");
            New.LastName = Console.ReadLine();
            Console.WriteLine("Enter last date");
            New.DateOut = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine("Enter time at site");
            try
            {
                New.FilmTime = (float)Convert.ToDouble(Console.ReadLine());
            }
            catch
            {

            }
            Console.WriteLine("Enter Budget");
            New.Budget = Convert.ToInt32(Console.ReadLine());
            v.Add(New);
        }
        static void Remove(List<InterProfile> v)
        {
            Console.WriteLine("Enter name to delete");
            string name = Console.ReadLine();
            v.RemoveAt(v.FindIndex(f => f.Name == name));
        }
        static void ChangeData(List<InterProfile> v)
        {
            Console.WriteLine("Enter name to change");
            string name = Console.ReadLine();
            if ((v.FindIndex(f => f.Name == name) != -1))
            {
                InterProfile Change = v[v.FindIndex(f => f.Name == name)];
                Console.WriteLine("1)Name\n2)Produser Name\n3)Date out\n4)Film time\n5)Budget");
                var res = Console.ReadKey().KeyChar;
                Console.WriteLine("Enter new value");
                if (res == '1')
                {
                    Change.Name = Console.ReadLine();
                }
                if (res == '2')
                {
                    Change.LastName = Console.ReadLine();
                }
                if (res == '3')
                {
                    Change.DateOut = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                if (res == '4')
                {
                    Change.FilmTime = Convert.ToInt16(Console.ReadLine());
                }
                if (res == '5')
                {
                    Change.Budget = Convert.ToInt16(Console.ReadLine());
                }
            }
            else
            {
                Console.WriteLine("Entered name not found");
                Console.ReadKey();
            }

        }
        public static string s(int c)
        {
            try
            {
                return new String(' ', c);
            }
            catch
            {
                return "";
            }
        }
        public static void SaveDate(List<InterProfile> Date, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                foreach (InterProfile g in Date)
                {

                    sw.WriteLine(g.Name.Trim() + "|" + g.LastName + "|" + g.DateOut.Date.ToString("dd.MM.yyyy") + "|" + g.FilmTime + "|" + g.Budget + "/");

                }
            }
        }
        public static List<InterProfile> ReadDate(string path)
        {
            List<InterProfile> g = new List<InterProfile>();
            string text = "";
            using (StreamReader sr = new StreamReader(path))
            {
                text = sr.ReadToEnd();
            }
            string[] Dates = text.Split('/');
            foreach (string s in Dates)
            {
                string[] MetaDete = s.Split('|');
                if (MetaDete.Length == 5)
                {
                    InterProfile d = new InterProfile
                    {
                        Name = MetaDete[0].Trim(),
                        LastName = MetaDete[1],
                        DateOut = DateTime.ParseExact(MetaDete[2], "dd.MM.yyyy", CultureInfo.InvariantCulture),
                        FilmTime = (float)Convert.ToDouble(MetaDete[3]),
                        Budget = Convert.ToInt32(MetaDete[4])
                    };
                    g.Add(d);
                }
            }
            return g;
        }
    }
}
