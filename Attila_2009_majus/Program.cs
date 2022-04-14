using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Attila_2009_majus
{
    internal class Program
    {
        struct Adat
        {
            public int ora { get; set; }
            public int perc { get; set; }
            public int masodperc { get; set; }
            public int csapatszam { get; set; }
            public int induloszint { get; set; }
            public int celszint { get; set; }
            public int maxszint { get { return Math.Max(induloszint, celszint); } }
            public int minszint { get { return Math.Min(induloszint, celszint); } }
            public Adat(string sor)//Konstruktor
            {
                string[] db = sor.Split();
                ora = int.Parse(db[0]);
                perc = int.Parse(db[1]);
                masodperc = int.Parse(db[2]);
                csapatszam = int.Parse(db[3]);
                induloszint = int.Parse(db[4]);
                celszint = int.Parse(db[5]);
            }
        }
        static List<Adat> lista = new List<Adat>();
        static void Main(string[] args)
        {
            //1.feladat
            Console.WriteLine("1.feladat: Beolvasás kész");
            foreach (var i in File.ReadAllLines("igeny.txt").Skip(3)) lista.Add(new Adat(i));

            //2.feladat
            Console.WriteLine("\n2.feladat");
            Console.Write("Kérem adja meg hol áll most a lift: ");
            int lift_kezdet = Convert.ToInt32(Console.ReadLine());

            //3.feladat
            Console.WriteLine("\n3.feladat");
            Console.WriteLine($"A lift a {lista.Last().celszint}. szinten áll az utolsó igény teljesítése után.");

            //4.feladat
            Console.WriteLine("\n4.feladat");
            int minszint = int.MaxValue, maxszint = int.MinValue;
            for (int i = 0; i < lista.Count; i++)
            {
                if (minszint > lista[i].minszint)
                {
                    minszint = lista[i].minszint;
                }
                if (maxszint < lista[i].maxszint)
                {
                    maxszint = lista[i].maxszint;
                }
            }
            Console.WriteLine($"A legalacsonyabb soszámú emelet: {Math.Min(minszint,lift_kezdet)}, a legmagasabb szint: {Math.Max(maxszint,lift_kezdet)}");

            //5.feladat
            Console.WriteLine("\n5.feladat");
            int utasnelkul = 0, utassal = 0;
            for (int i=0;i<lista.Count()-1;i++)
            {
                if (lista[i].celszint < lista[i+1].induloszint)//utasnelkuél
                {
                    utasnelkul++;
                }
                if (lista[i].induloszint < lista[i].celszint)//utassal
                {
                    utassal++;
                }
            }
            if (lista.Last().induloszint < lista.Last().celszint)
            {
                utassal++;
            }
            if (lift_kezdet < lista.First().induloszint)
            {
                utasnelkul++;
            }
            Console.WriteLine($"Felefelé a lift utassal: {utassal}, utasnélkül: {utasnelkul}.");

            //6.feladat
            Console.WriteLine("\n6.feladat");
            int csapatokszama = int.Parse(File.ReadLines("igeny.txt").ElementAt(1));
            for (int i = 1;i<=csapatokszama;i++)
            {
                bool volt = true;//Villanykapcsolós módszer.
                /*foreach(var j in lista)
                {
                    if (i == j.csapatszam)
                    {
                        volt = false;
                    }
                }*/
                for (int j = 0;j<lista.Count();j++)
                {
                    if (i == lista[j].csapatszam)
                    {
                        volt = false;
                    }
                }
                if (volt)
                {
                    Console.Write($"{i} ");
                }
            }
            //7.feladat
            Console.WriteLine("\n\n7.feladat");
            Random veletlen = new Random();
            int veltelen_csapat = veletlen.Next(1,csapatokszama+1);
            Console.WriteLine($"{veltelen_csapat}");
            var csapatszam = lista.Where(x => x.csapatszam == veltelen_csapat);
            bool igaz = true;
            for(int i = 0;i<csapatszam.Count()-1;i++)
            {
                if (csapatszam.ElementAt(i).celszint != csapatszam.ElementAt(i+1).induloszint)
                {
                    Console.WriteLine($"A {veltelen_csapat} csapat a(z) {csapatszam.ElementAt(i).celszint}-ről gyalog ment a {csapatszam.ElementAt(i + 1).induloszint}-re.");
                    igaz = false;
                }
            }
            if (igaz)
            {
                Console.WriteLine("Nem bizonyítható szabálytalanság");
            }

            //8.feladat
            Console.WriteLine("\n8.feladat");
            StreamWriter ir = new StreamWriter("blokkol.txt");
            if (csapatszam.Count()==0)
            {
                Console.WriteLine($"{veltelen_csapat} csapat a megfigyelt időszakban nem szerepelt!");
            }
            else
            {
                for (int i = 0; i < csapatszam.Count(); i++)
                {
                    Console.WriteLine($"Indulási emelet: {csapatszam.ElementAt(i).induloszint}");
                    ir.WriteLine($"Indulási emelet: {csapatszam.ElementAt(i).induloszint}");
                    Console.WriteLine($"Célemelet: {csapatszam.ElementAt(i).celszint}");
                    ir.WriteLine($"Célemelet: {csapatszam.ElementAt(i).celszint}");
                    Console.Write($"Feladatkód: ");
                    string feladatkod = Console.ReadLine();
                    ir.WriteLine($"Feladatkód: " + feladatkod);
                    Console.WriteLine($"Befejezés ideje: {csapatszam.ElementAt(i).ora}:{csapatszam.ElementAt(i).perc}:{csapatszam.ElementAt(i).masodperc}");
                    ir.WriteLine($"Befejezés ideje: {csapatszam.ElementAt(i).ora}:{csapatszam.ElementAt(i).perc}:{csapatszam.ElementAt(i).masodperc}");
                    Console.Write($"Sikeresség: ");
                    string sikeresseg = Console.ReadLine();
                    ir.WriteLine("Sikeresség: " + sikeresseg);
                    Console.WriteLine("-----");
                    ir.WriteLine("-----");
                }
                ir.Close();
            }            
            Console.ReadKey();
        }
    }
}
