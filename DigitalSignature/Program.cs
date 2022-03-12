using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DigitalSignature
{
    class Program
    {
        static void Main(string[] args)
        {

            int p, q;
            Console.Write("Bir asal p sayısı giriniz : ");
            p = Convert.ToInt32(Console.ReadLine());

            Console.Write("Bir asal q sayısı giriniz : ");
            q = Convert.ToInt32(Console.ReadLine());


            int n = p * q;
            int phi = N_KadarAsalSayiSayisi(n); // int phi=(q-1)(p-1)


            int e = 0; //1<e<phi(n)
            int random;
            while(true)
            {
                random = new Random().Next(1, phi);
                if (Obeb(random,phi)==1)
                {
                    e = random;
                    break;
                }
            }
            /*while (true)
            {
                int[] degerTutucu = new int[phi];
                int deger = 0;
                //kullanıcıya e seçdiriyoruz
                Console.WriteLine("\n----- Seçilecebilecek sayılar(e) -----");
                for (int i = 2; i < phi; i++)
                {
                    if (Obeb(phi, i) == 1)
                    {
                        degerTutucu[i - 2] += i;
                        Console.Write(i + " ");
                    }
                }

                Console.Write("\ne sayısı seçiniz : ");
                e = Convert.ToInt32(Console.ReadLine());

                foreach (int t in degerTutucu)
                {
                    if (e == t)
                    {
                        deger = t;
                        break;
                    }
                }
                if (e == deger)
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\nHATALI GİRİŞ... YENİDEN DENEYİN\n\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
            }*/

            int d = D_Bulma(phi, e);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\nGenel anahtar(n:{0},d:{1})", n, d);
            Console.WriteLine("\nÖzel anahtar(e:{0})", e);


            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nMetin giriniz : ");
            Console.ForegroundColor = ConsoleColor.Green;
            string text = Convert.ToString(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.White;
            string hashMessage = Sha256Hash(text);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\nHash\n");
            Console.WriteLine(hashMessage);
            Console.ForegroundColor = ConsoleColor.White;

            
            Console.WriteLine("\n\n\n");
            string sifrelenmisMetin = Encrypt(hashMessage, d, n);
            

            //string cozulmusMetin = Decrypt(sifrelenmisMetin, e, n);

            Alici(Decrypt(sifrelenmisMetin, e, n), text);
            
            
        }
        


        static string Encrypt(string text, int d, int n)
        {
            int L;
            L = BasamakSayisi(n);
            int L_clear = L - 1;
            int L_cipher = L;

            byte[] bytes = Encoding.ASCII.GetBytes(text);

            // 3lü basamak şeklinde ascii değerler
            string[] ucBasamakliAsciiDegerler = new string[bytes.Length];
            
            for (int i = 0; i < bytes.Length; i++)
            {
                int a = Int32.Parse(bytes[i].ToString());
                if (a < 10)
                {
                    ucBasamakliAsciiDegerler[i] = ("00" + a.ToString());
                }
                else if (a < 100)
                {
                    ucBasamakliAsciiDegerler[i] = ('0' + a.ToString());
                }
                else
                {
                    ucBasamakliAsciiDegerler[i] = a.ToString();
                }
            }


            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--------------------------- Şifreleme ---------------------------\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n");
            Console.WriteLine("uc basamaklı  ascii değerler\n");

            for (int i = 0; i < ucBasamakliAsciiDegerler.Length; i++)
            {
                Console.Write(ucBasamakliAsciiDegerler[i] + "\t");
            }

            string[] ikiBasamakliAsciiDegerler;
            if ((ucBasamakliAsciiDegerler.Length * 3) % L_clear == 0)
            {
                ikiBasamakliAsciiDegerler = new string[(ucBasamakliAsciiDegerler.Length * 3) / L_clear];
            }
            else
            {
                ikiBasamakliAsciiDegerler = new string[(ucBasamakliAsciiDegerler.Length * 3) / L_clear + 1];
            }

            int sayac = 0;
            char[] tekTekAsciiDegerTutan = new char[ucBasamakliAsciiDegerler.Length * 3];
            for (int i = 0; i < ucBasamakliAsciiDegerler.Length; i++)
            {
                for (int j = 0; j < ucBasamakliAsciiDegerler[i].Length; j++)
                {
                    tekTekAsciiDegerTutan[sayac] = ucBasamakliAsciiDegerler[i][j];
                    sayac++;
                }
            }
            
            for (int i = 0; i < tekTekAsciiDegerTutan.Length; i++)
            {
                ikiBasamakliAsciiDegerler[i / L_clear] += tekTekAsciiDegerTutan[i].ToString();
            }
            while (ikiBasamakliAsciiDegerler[ikiBasamakliAsciiDegerler.Length - 1].Length < L_clear)
            {
                ikiBasamakliAsciiDegerler[ikiBasamakliAsciiDegerler.Length - 1] += "0";
            }

            Console.WriteLine("\n\n\n");
            Console.WriteLine("L_clear'a göre ayarlanmış (string türünde) ascii değerler\n");
            int[] m = new int[ikiBasamakliAsciiDegerler.Length];
            for (int i = 0; i < m.Length; i++)
            {
                m[i] = Int32.Parse(ikiBasamakliAsciiDegerler[i]);
                Console.Write(ikiBasamakliAsciiDegerler[i] + "\t");
            }


            Console.WriteLine("\n\n\n");
            Console.WriteLine("L_clear'a göre ayarlanmış (int türünde) ascii değerler\n");
            int[] tekTeksifrelenmisDegerler = new int[m.Length];
            for (int i = 0; i < tekTeksifrelenmisDegerler.Length; i++)
            {
                Console.Write(m[i] + "\t");
                tekTeksifrelenmisDegerler[i] = UstelMod(m[i], d, n);
            }

            Console.WriteLine("\n\n\nmod alınmış değerler - n:{0} d:{1}\n",n,d);
            
            for (int i = 0; i < tekTeksifrelenmisDegerler.Length; i++)
            {
                Console.Write(tekTeksifrelenmisDegerler[i] + "\t");
            }

            
            string sifrelenmisMetin = null;
            sayac = 0;
            string sifirTutucu = "";
            for (int i = 0; i < tekTeksifrelenmisDegerler.Length; i++)
            {


                sayac = tekTeksifrelenmisDegerler[i].ToString().Length;
                while (sayac < L_cipher)
                {
                    sifirTutucu += "0";
                    sayac++;
                }
                sifrelenmisMetin += sifirTutucu + tekTeksifrelenmisDegerler[i].ToString();
                sifirTutucu = "";
            }

            Console.WriteLine("\n\n\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Basamak sayısı L_cipher'a göre ayarlı\n");
            Console.WriteLine("Şifrelenmiş Metin\n");
            Console.WriteLine(sifrelenmisMetin);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n\n");

            return sifrelenmisMetin;

        }

        static string Decrypt(string text, int e, int n)
        {
            int L_cipher = BasamakSayisi(n);
            int L_clear = L_cipher - 1;
            string[] araDeger = new string[text.Length/L_cipher];
            int sayac = 0;
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < L_cipher; j++)
                {
                    araDeger[sayac] += text[i].ToString();
                    i++;
                }
                i--;
                sayac++;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--------------------------- Deşifreleme ---------------------------\n");
            Console.ForegroundColor = ConsoleColor.White;


            Console.WriteLine("\n\n\nŞifrelenmiş metnin deşifrelemeye aktarılması");
            for (int i = 0; i < araDeger.Length; i++)
            {
                Console.Write(araDeger[i] + "\t");
            }



            Console.WriteLine("\n\n\nŞifrelenmis metnin integer halinde kullanılması");
            int[] s = new int[araDeger.Length];
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = Int32.Parse(araDeger[i]);
                Console.Write(s[i] + "\t");
            }
            


            Console.WriteLine("\n\n\n");
            Console.WriteLine("mod alınmış değerler - n:{0} e:{1}\n", n, e);
            int[] tekTekDesifrelenmisDegerler = new int[araDeger.Length];
            for (int i = 0; i < tekTekDesifrelenmisDegerler.Length; i++)
            {
                tekTekDesifrelenmisDegerler[i] = UstelMod(s[i], e, n);
            }
            
            Console.WriteLine("tek tek desifrelenmis(mod alınmış) değerler\n");
            for (int i = 0; i < tekTekDesifrelenmisDegerler.Length; i++)
            {
                Console.Write(tekTekDesifrelenmisDegerler[i] + "\t");
            }

            string[] ikiBasamakliTekTekDesifrelenmisDegerler = new string[tekTekDesifrelenmisDegerler.Length];
            string sifirTutucu = null;
            for (int i = 0; i < ikiBasamakliTekTekDesifrelenmisDegerler.Length; i++)
            {
                sayac = tekTekDesifrelenmisDegerler[i].ToString().Length;
                while (sayac < L_clear)
                {
                    sifirTutucu += '0';
                    sayac++;
                }
                ikiBasamakliTekTekDesifrelenmisDegerler[i] += sifirTutucu + tekTekDesifrelenmisDegerler[i].ToString();
                sifirTutucu = null;
            }

            Console.WriteLine("\n\n\n");
            Console.WriteLine("L_clear basamaklı tek tek desifrelenmis değerler\n");
            string cozulmusMetin = null;
            for (int i = 0; i < ikiBasamakliTekTekDesifrelenmisDegerler.Length; i++)
            {
                cozulmusMetin += ikiBasamakliTekTekDesifrelenmisDegerler[i];
                Console.Write(ikiBasamakliTekTekDesifrelenmisDegerler[i] + "\t");
            }
            

            string[] ucluDesifrelenmisAsciiDegerler = new string[cozulmusMetin.Length / 3];
            sayac = 0;
            int k = 0;
            for (int i = 0; i < cozulmusMetin.Length; i++)
            {
                while (k < 3)
                {
                    ucluDesifrelenmisAsciiDegerler[sayac] += cozulmusMetin[i].ToString();
                    i++;
                    k++;
                }
                i--;
                sayac++;
                k = 0;
            }

            Console.WriteLine("\n\n\ndesifrelenmis uclu ascii değerler");
            for (int i = 0; i < ucluDesifrelenmisAsciiDegerler.Length; i++)
            {
                Console.Write(ucluDesifrelenmisAsciiDegerler[i] + "\t");
            }


            Console.WriteLine("\n\n\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Çözülmüş Metin\n");
            Console.WriteLine(cozulmusMetin);

            return cozulmusMetin;
        }

        static void Alici(string t, string message)
        {
            string cozulmusMetin = t;
            string hashMessage = Sha256Hash(message);
            string[] asciiDonusum = new string[cozulmusMetin.Length / 3];
            int k = 0, sayac = 0;
            Console.WriteLine("\n\n");
            for (int i = 0; i < cozulmusMetin.Length; i++)
            {
                while (k < 3)
                {
                    asciiDonusum[sayac] += cozulmusMetin[i].ToString();
                    k++;
                    i++;
                }
                sayac++;
                i--;
                k = 0;
            }

            string fromAsciiToHash = null;
            for (int i = 0; i < asciiDonusum.Length; i++)
            {
                fromAsciiToHash += Char.ConvertFromUtf32(Convert.ToInt32(asciiDonusum[i]));
            }

            Console.WriteLine("\n\n\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Çözülmüş metnin string'i(hash değerler)\n");
            Console.WriteLine(fromAsciiToHash);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n\n");
            if (fromAsciiToHash == hashMessage)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Mesaj doğrulandı");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Mesaj doğrulanmadı");
            }
            

            Console.ForegroundColor = ConsoleColor.White;
        }

        // 1,2,3,4,5...n gider
        // ikisinin ebobu 1 olanların sayısını alır
        // bir sayıya kadar olan asal sayıların toplam sayısı
        static int N_KadarAsalSayiSayisi(int n)
        {
            int adet = 0;

            for (int i = 1; i < n; i++)
            {
                if (Obeb(n, i) == 1)
                    adet++;
            }

            return adet;
        }

        static int Obeb(int x, int y)
        {
            int min = Math.Min(x, y);
            int obeb = 1;

            for (int i = 2; i <= min; i++)
            {
                if (x % i == 0 && y % i == 0)
                    obeb = i;
            }
            return obeb;
        }

        static int D_Bulma(int phi, int e)
        {
            int d = 0;

            for (int i = 2; i < phi; i++)
            {
                    if ((e * i) % phi == 1)
                    {
                        d = i;

                        break;
                    }
            }

            return d;
        }

        static string Sha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // array'i byte dizisine döndürme 
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // byte dizisini bir string'e dönüştürme
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    //Console.WriteLine(bytes[i]);
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        static int UstelMod(int deger, int ust, int mod)//a^b(mod m)
        {
            int _deger = deger % mod;
            if (ust == 0)
            {
                return 1;
            }

            while (ust > 1)
            {
                _deger *= deger;
                _deger %= mod;
                ust--;
            }

            return _deger;
        }

        static int BasamakSayisi(int n)
        {
            int nBasamakSayisi = 0;
            while (n > 0)
            {
                n /= 10;
                nBasamakSayisi++;
            }

            return nBasamakSayisi;
        }
        
    }

}