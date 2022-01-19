using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CATAGY_Kektura
{
    class Tura
    {
        public string Kiindulopont { get; set; }
        public string Vegpont { get; set; }
        public int Hossz { get; set; }
        public int Emelkedes { get; set; }
        public int Lejtes { get; set; }
        public bool Pecserelo { get; set; }
        public bool HianyosNev => !Vegpont.Contains("pecsetelohely") && Pecserelo;

        public Tura(string sor)
        {
            var buffer = sor.Split(';');

            Kiindulopont = buffer[0];
            Vegpont = buffer[1];
            Hossz = int.Parse(buffer[2]);
            Emelkedes = int.Parse(buffer[3]);
            Lejtes = int.Parse(buffer[4]);
            Pecserelo = buffer[5] == "i";
        }

    }
    internal class Program
    {
        static List<Tura> turak = new List<Tura>();
        static int InduloTSZFM = 0;
        static void Main(string[] args)
        {
            Beolvasas();
            F03();
            F04();
            F05();
            F07();
            F08();
        }

        private static void F08()
        {
            var vegpontSzerint = new Dictionary<string, int>();

            var TSZFM = InduloTSZFM;

            foreach (var tura in turak)
            {
                TSZFM = TSZFM + tura.Emelkedes - tura.Lejtes;
                vegpontSzerint.Add(tura.Vegpont, TSZFM);

            }
            var legmagasabban = vegpontSzerint.OrderByDescending(x => x.Value).First();
            Console.WriteLine("8. Feladat: A túra legmagasabban fekvő pontja: " +
                $"\n\tA végpont neve: {legmagasabban.Key}" +
                $"\n\tA végpont tengerszint feletti magassága: {legmagasabban.Value} m");
        }

        private static void F07()
        {
            Console.WriteLine("7. Feladat: Hiányos állomásnevek: ");
            var hianyos = 0;
            foreach (var tura in turak)
            {
                if (tura.HianyosNev)
                {
                    Console.WriteLine("\t" + tura.Vegpont);
                    hianyos++;
                }
            }

            if (hianyos == 0)
                Console.WriteLine("Nincs hiányos állomásnév!");
        }

        private static void F05()
        {
            var legrovidebb = turak.OrderBy(x => x.Hossz).First();
            Console.WriteLine($"5. Feladat: A legrövidebb szakasz adatai:" +
                $"\n\tKezdete: {legrovidebb.Kiindulopont}" +
                $"\n\tVége: {legrovidebb.Vegpont}" +
                $"\n\tTűvolság: {legrovidebb.Hossz} km");
        }

        private static void F04()
        {
            Console.WriteLine($"4. Feladat: A túra teljes hossza: {turak.Sum(x => x.Hossz)} km");
        }

        private static void F03()
        {
            Console.WriteLine($"3. Feladat: Szakaszok száma: {turak.Count} db");
        }

        private static void Beolvasas()
        {
            using (var sr = new StreamReader(@"..\..\RES\kektura.csv", Encoding.UTF8))
            {
                InduloTSZFM = int.Parse(sr.ReadLine());
                while (!sr.EndOfStream)
                {
                    turak.Add(new Tura(sr.ReadLine()));
                }
            }
        }
    }
}
