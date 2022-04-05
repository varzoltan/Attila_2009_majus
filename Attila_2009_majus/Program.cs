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
            Console.ReadKey();
        }
    }
}
