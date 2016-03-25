using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace VojnaBarv
{
    class VojnaBarv
    {
        static void Main(string[] args)
        {
            try {
                Random r = new Random();
                Bitmap src = new Bitmap("Random.bmp");
                Console.WriteLine("Vnesi število iteracij:");
                int iteracije = Convert.ToInt32(Console.ReadLine());
                Color[] pixli = new Color[4];
                double[] prob = new double[3];
                int stBarv = 0;
                Color g = Color.LawnGreen;
                Color red = Color.Red;

                System.IO.StreamWriter file = new System.IO.StreamWriter("barve.txt", true);
                //System.IO.File.WriteAllText(@"D:\Fax\2. Letnik\MM\Projekt 1\Vojne\barve.txt", text);



                double Nr = 0;
                double Ng = 0;
                double Nb = 0;
                int dimenzije = src.Width * src.Height;
                //for (int i = 0; i < iteracije; i++)
                int i = 0;
                Boolean pogoj = false;
                while(!pogoj)
                {
                    i++;
                    //Console.WriteLine("Iteracija: {0}...", i+1);
                    Bitmap newBitmap = new Bitmap(src.Width, src.Height);

                    for (int y = 0; y < src.Width; y++)
                    {
                        for (int x = 0; x < src.Height; x++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                prob[j] = 0;
                            }
                            stBarv = 0;
                            if (x - 1 >= 0)
                            {
                                pixli[stBarv] = src.GetPixel(x-1, y);
                                stBarv++;
                            }
                            if (x + 1 < src.Width)
                            {
                                pixli[stBarv] = src.GetPixel(x + 1, y);
                                stBarv++;
                            }
                            if (y - 1 >= 0)
                            {
                                pixli[stBarv] = src.GetPixel(x, y-1);
                                stBarv++;
                            }
                            if (y + 1 < src.Height)
                            {
                                pixli[stBarv] = src.GetPixel(x, y+1);
                                stBarv++;
                            }
                            for (int j = 0; j < stBarv; j++)
                            {
                                //Console.WriteLine(pixli[j].ToString());
                                if (pixli[j].ToString() == "Color [A=255, R=255, G=0, B=0]")
                                {
                                    prob[0] += (double)1 /stBarv;
                                }
                                
                                else if (pixli[j].ToString() == "Color [A=255, R=124, G=252, B=0]")
                                {
                                    prob[1] += (double)1 /stBarv;
                                }
                                else
                                {
                                    prob[2] += (double)1 /stBarv;
                                }
                                //Console.WriteLine((double)1/ stBarv);
                            }
                            //Console.WriteLine("("+y +"," + x + ") " +string.Join(",", prob));
                            double ran = r.NextDouble();
                            if (ran < prob[0])
                            {
                               // Console.WriteLine("R");
                                newBitmap.SetPixel(x, y, Color.Red);
                                Nr++;
                            }
                            else if (ran > prob[0] && ran < prob[0] + prob[1])
                            {
                                //Console.WriteLine("G");
                                newBitmap.SetPixel(x, y, Color.LawnGreen);
                                Ng++;
                            }
                            else
                            {
                                //Console.WriteLine("B");
                                newBitmap.SetPixel(x, y, Color.Blue);
                                Nb++;
                            }
                        }
                    }
                    pogoj = (Nr == 2500 || Nb == 2500 || Ng == 2500);
                    double pR = Math.Floor(Nr / dimenzije*100);
                    double pG = Math.Floor(Ng / dimenzije * 100);
                    double pB = Math.Floor(Nb / dimenzije * 100);
                    Console.WriteLine("Rdeče:{0}% Zeleno:{1}% Modro:{2}%", pR, pG, pB);
                    //Console.WriteLine("Rdeče:{0} Zeleno:{1} Modro:{2}", Nr, Ng, Nb);
                    file.Write("{0} {1} {2}\n", Nr, Ng, Nb);
                    Nr = 0; Ng = 0; Nb = 0;
                    newBitmap.Save("Vojna" + i + ".bmp");
                    newBitmap.Dispose();
                    src.Dispose();
                    src = new Bitmap("Vojna" + i + ".bmp");
                    String s = "Vojna" + (i-1) + ".bmp";
                    try
                    {
                        System.IO.File.Delete(s);
                    }
                    catch (System.IO.IOException e)
                    {
                        Console.WriteLine(e.Message);
                        return;
                    }
                }
                file.Close();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("There was an error." +
                    "Check the path to the image file.");
            }
        }
    }
}
