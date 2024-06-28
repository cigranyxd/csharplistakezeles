using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace allat
{
    class Allat
    {
        public string nev { get; private set; }
        public int szuletesiEv { get; private set; }
        public int rajtSzam { get; private set; }

        public int szepsegPont { get; private set; }
        public int viselkedesPont { get; private set; }

        public static int aktualisEv { get; set; }
        public static int korHatar { get; set; }

        public Allat(string nev, int szuletesiEv, int rajtSzam)
        {
            this.nev = nev;
            this.szuletesiEv = szuletesiEv;
            this.rajtSzam = rajtSzam;
        }

        public int Kor()
        {
            return aktualisEv - szuletesiEv;
        }
        
        public virtual int PontSzam()
        {
            if (Kor() < korHatar)
            {
                return viselkedesPont * Kor() + szepsegPont * (korHatar - Kor());
            }
            return 0;
        }
        
        public void Pontozzak(int szepsegPont, int viselkedesPont)
        {
            this.szepsegPont = szepsegPont;
            this.viselkedesPont = viselkedesPont;
        }

        public override string ToString()
        {
            return rajtSzam + ". " + nev + " nevű " + this.GetType().Name.ToLower() + " pontszáma: " + PontSzam() + " pont."; 
        }
    }

    class Kutya : Allat
    {
        public int gazdaViszonyPont { get; private set; }
        public bool kapottViszonyPontot { get; private set; }
        public Kutya(string nev, int szuletesiEv, int rajtSzam) : base(nev, szuletesiEv, rajtSzam)
        {
        }
        public void ViszonyPontozas(int gazdaViszonyPont)
        {
            this.gazdaViszonyPont = gazdaViszonyPont;
            kapottViszonyPontot = true;
        }
        public override int PontSzam()
        {
            int pont = 0;
            if (kapottViszonyPontot)
            {
                pont = base.PontSzam() + gazdaViszonyPont;
            }
            return pont;
        }
    }
    class Macska : Allat
    {
        public bool vanMacskaSzallitoDoboz { get; set; }
        public Macska(string nev, int szuletesiEv, int rajtSzam, bool vanMacskaSzallitoDoboz) : base(nev, szuletesiEv, rajtSzam)
        {
            this.vanMacskaSzallitoDoboz = vanMacskaSzallitoDoboz;
        }
        public override int PontSzam()
        {
            if (vanMacskaSzallitoDoboz)
            {
                return base.PontSzam();
            }
            return 0;
        }
    }
    class Vezerles
    {
        private List<Allat> allatok = new List<Allat>();

        public void Start()
        {
            Allat.aktualisEv = 2015;
            Allat.korHatar = 10;

            Proba();
            Regisztracio();
            Verseny();
        }
        private void Proba()
        {
            Allat allat1, allat2;

            string nev1 = "Günther", nev2 = "Pamacs";
            int szulEv1 = 2010, szulEv2 = 2011;
            bool vanDoboz = true;
            int rajtSzam = 1;

            int szepsegpont = 5,
                viselkedesPont = 4,
                viszonyPont = 6;

            allat1 = new Kutya(nev1, szulEv1,rajtSzam);
            rajtSzam++;

            allat2 = new Macska(nev2, szulEv2, rajtSzam, vanDoboz);

            allat2.Pontozzak(szepsegpont, viselkedesPont);

            if (allat1 is Kutya)
            {
                ((Kutya)allat1).Pontozzak(szepsegpont, viselkedesPont);
            }
            allat1.Pontozzak(szepsegpont, viselkedesPont);

            Console.WriteLine("\nA verseny eredménye: ");
            Console.WriteLine(allat1);
            Console.WriteLine(allat2);
        }
        private void Regisztracio()
        {
            StreamReader sr = new StreamReader("allatok.txt");

            string fajta, nev;
            int rajtSzam = 1, szulEv;
            bool vanDoboz;

            while (!sr.EndOfStream) 
            {
                fajta = sr.ReadLine();
                nev = sr.ReadLine();
                szulEv = int.Parse(sr.ReadLine());

                if (fajta == "kutya")
                {
                    allatok.Add(new Kutya(nev, szulEv, rajtSzam));
                }
                else
                {
                    vanDoboz = bool.Parse(sr.ReadLine());
                    allatok.Add(new Macska(nev, szulEv, rajtSzam, vanDoboz));
                }
                rajtSzam++;
            }
            sr.Close();
        }
        private void Verseny()
        {
            Random rnd = new Random();
            int hatar = 11;
            foreach (Allat item in allatok) 
            {
                if (item is Kutya)
                {
                    ((Kutya)item).ViszonyPontozas(rnd.Next(hatar));
                }
                item.Pontozzak(rnd.Next(hatar), rnd.Next (hatar));
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Vezerles a = new Vezerles();
            a.Start();

            Console.ReadKey();
        }
    }
}
