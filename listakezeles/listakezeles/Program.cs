    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    namespace listakezeles
    {
        class Versenyzo
        {
            private int rajtSzam;
            private string nev;
            private string szak;
            private int pontSzam;

            public Versenyzo(int rajtSzam, string nev, string szak)
            {
                this.rajtSzam = rajtSzam;
                nev = nev;
                szak = szak;
            }
            public int RajtSzam
            {
                get { return rajtSzam; }
            }
            public string Nev
            {
                get { return nev; }
            }
            public string Szak
            {
                get { return szak; }
            }
            public void PontotKap(int a)
            {
                pontSzam += a;
            }
            public override string ToString()
            {
                return rajtSzam + "\t" + nev + "\t" + szak + "\t" + pontSzam + " pont";
            }
            public int PontSzam
            {
                get { return pontSzam; }
            }
        }
            class VezerloOsztaly
            {
                List<Versenyzo> versenyzok = new List<Versenyzo>();
                private int zsuriLetszam = 5;
                private int pontHatar = 10;

                public void Start()
                {
                    AdatBevitel();

                    Kiiratas("\nRésztvevők:\n");
                    Verseny();
                    Kiiratas("\nEredmények:\n");

                    Eredmenyek();
                    Keresesek();
                }
                private void AdatBevitel()
                {
                    List<Versenyzo> versenyzok = new List<Versenyzo>();
                    Versenyzo versenyzo;
                    string nev, szak;
                    int sorszam = 1;

                    StreamReader sr = new StreamReader("versenyzok.txt");

                    while (!sr.EndOfStream)
                    {
                        nev = sr.ReadLine();
                        szak = sr.ReadLine();

                        versenyzo = new Versenyzo(sorszam, nev, szak);

                        versenyzok.Add(versenyzo);
                    }
                    sr.Close();
                }
                private void Kiiratas(string cim)
                {
                    Console.WriteLine(cim);
                    foreach (Versenyzo enekes in versenyzok)
                    {
                        Console.WriteLine(enekes);
                    }
                }
                private void Verseny()
                {
                    Random rnd = new Random();
                    int pont;
                    foreach (Versenyzo versenyzo in versenyzok)
                    {
                        for (int i = 1; i <= zsuriLetszam; i++)
                        {
                            pont = rnd.Next(pontHatar);
                            versenyzo.PontotKap(pont);
                        }
                    }
                }
                private void Eredmenyek()
                {
                    Nyertes();
                    Sorrend();
                }

                private void Nyertes()
                {
                    int max = versenyzok[0].PontSzam;

                    foreach (Versenyzo enekes in versenyzok)
                    {
                        if (enekes.PontSzam == max)
                        {
                            max = enekes.PontSzam;
                        }
                    }
                    Console.WriteLine("\nA legjobbak:\n");
                    foreach (Versenyzo enekes in versenyzok)
                    {
                        if (enekes.PontSzam == max)
                        {
                            Console.WriteLine(enekes);
                        }
                    }
                }
                private void Sorrend()
                {
                    Versenyzo temp;
                    for (int i = 0; i < versenyzok.Count; i++)
                    {
                        for (int j = i + 1; j < versenyzok.Count; j++)
                        {
                            if (versenyzok[i].PontSzam < versenyzok[j].PontSzam)
                            {
                                temp = versenyzok[i];
                                versenyzok[i] = versenyzok[j];
                                versenyzok[j] = temp;
                            }
                        }
                    }
                }
                private void Keresesek()
                {
                    Console.WriteLine("\nAdott szakhoz tartozó énekesek keresése\n");
                    Console.WriteLine("\nKeres valakit?\n");
                    char valasz;
                    while (!char.TryParse(Console.ReadLine(), out valasz))
                    {
                        Console.WriteLine("Egy karaktert írjon. ");
                    }
                    string szak;
                    bool vanIlyen;

                    while (valasz == 'i' || valasz == 'I')
                    {
                        Console.WriteLine("Szak: ");
                        szak = Console.ReadLine();
                        vanIlyen = false;

                        foreach (Versenyzo enekes in versenyzok)
                        {
                            if (enekes.Szak == szak)
                            {
                                Console.WriteLine(enekes);
                                vanIlyen = true;
                            }
                        }
                    
                        if (!vanIlyen)
                    {
                        Console.WriteLine("Erről a szakról senki nem indult.");
                    }
                    Console.WriteLine("Keres még valakit? (i/n)");
                    valasz = char.Parse(Console.ReadLine());
                }
                }
            }
    
            internal class Program
            {
                static void Main(string[] args)
                {
                    VezerloOsztaly a = new VezerloOsztaly();

            a.Start();            
            
                    Console.ReadKey();
                }
            }
    
    }

