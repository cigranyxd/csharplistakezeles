using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Allat
    {
        private int EletKor;
        private string AllatFajta;

        private int SzepsegPont;
        private int ViselkedesPont;
        private int osszPont;

        private static int jelenEv;
        private static int korHatar;

        public Allat(int eletKor, string allatFajta)
        {
            EletKor = eletKor;
            AllatFajta = allatFajta;
        }

        public int Kor()
        {
            return jelenEv - EletKor;
        }

        public void Pontozzak(int szepsegPont, int viselkedesPont)
        {
            this.SzepsegPont = szepsegPont;
            this.ViselkedesPont = viselkedesPont;

            if(Kor() <= korHatar) {
                osszPont = viselkedesPont * Kor() + szepsegPont * (korHatar - Kor());
            }
            else
            {
                osszPont = 0;
            }
        }
        public override string ToString()
        {
            return AllatFajta + " pontszáma " + osszPont + " pont";
        }
        public string allatfajta { 
            get { return AllatFajta; }
        }
        public int eletKor
        {
            get { return EletKor; }
        }
        public int szepsegPont
        {
            get { return SzepsegPont; }
        }
        public int viselkedesPont
        {
            get { return ViselkedesPont; }
        }
        public int Osszpont
        {
            get { return osszPont; }
        }
        public static int JelenEv
        {
            get { return jelenEv; }
            set { jelenEv = value; }
        }
        public static int Korhatar
        {
            get { return korHatar; }
            set { korHatar = value; }
        }

        private static void AllatVerseny()
        {
            Allat allat;

            string nev;
            int szulEv;
            int szepseg, viselkedes;
            int veletlenPonthatar = 10;

            Random rnd = new Random();

            int osszesVersenyzo = 0;
            int osszesPont = 0;
            int legtobbPont = 0;

            char tovabb = 'i';
            while (tovabb == 'i')
            {
                Console.WriteLine("Az állat neve: ");
                nev = Console.ReadLine();

                Console.WriteLine("Az állat születési éve: ");
                while (!int.TryParse(Console.ReadLine(), out szulEv) || szulEv <=0 || szulEv > Allat.jelenEv)
                {
                    Console.WriteLine("Hibás adat, kérem újra.");
                }


                szepseg = rnd.Next(veletlenPonthatar + 1);
                viselkedes = rnd.Next(veletlenPonthatar + 1);

                allat = new Allat(szulEv, nev);

                Console.WriteLine(allat);

                osszesVersenyzo++;
                osszesPont += allat.osszPont;

                if (legtobbPont < allat.osszPont) {
                    legtobbPont = allat.osszPont;
                }

                Console.WriteLine("Van még állat?(i/n)");

                tovabb = char.Parse(Console.ReadLine());
            }

            Console.WriteLine($"\nÖsszesen {osszesVersenyzo} volt.\nÖsszpontszámuk: {osszesPont} pont\nLegnagyobb pontszám: {legtobbPont}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {


            int aktEv = 2015, korhatar = 10;

            

            Allat.JelenEv = aktEv;
            Allat.Korhatar = korhatar;

            
            Console.ReadKey();

        }
    }
}
