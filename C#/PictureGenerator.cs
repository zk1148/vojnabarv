using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PictureGenerator
{
    class PictureGenerator
    {
        static void Main(string[] args)
        {
            double[] prob = new double[3];
            Console.WriteLine("Vnesi R G B verjetnosti:");
            prob[0] = Convert.ToDouble(Console.ReadLine());
            prob[1] = Convert.ToDouble(Console.ReadLine());
            prob[2] = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Vnesi širino in višino:");
            int dimX = Convert.ToInt32(Console.ReadLine());
            int dimY = Convert.ToInt32(Console.ReadLine());
            PictureBox pictureBox1 = new PictureBox();
            Random r = new Random();
            try
            {
                // Retrieve the image.
                //Bitmap src = new Bitmap(@"C:\Users\Rok\Documents\Visual Studio 2015\Projects\PictureGenerator\PictureGenerator\sample.bmp", true);
                Bitmap newBitmap = new Bitmap(dimX, dimY);
                int x, y;

                // Loop through the images pixels to reset color.
                for (x = 0; x < newBitmap.Width; x++)
                {
                    for (y = 0; y < newBitmap.Height; y++)
                    {
                        double ran = r.NextDouble();
                        //Console.Write("Random: {0}  color:", Math.Floor(ran*100)/100);
                        if(ran <= prob[0])
                        {
                            //Console.WriteLine("R");
                            newBitmap.SetPixel(x, y, Color.Red);
                        }
                        else if(ran > prob[0] && ran <= prob[0]+prob[1])
                        {
                            //Console.WriteLine("G");
                            newBitmap.SetPixel(x, y, Color.LawnGreen);
                        }
                        else
                        {
                            //Console.WriteLine("B");
                            newBitmap.SetPixel(x, y, Color.Blue);
                        }
                        //Color pixelColor = src.GetPixel(x, y);
                        //Color newColor = Color.FromArgb(pixelColor.R, 55, 132);
                        //Color newColor = Color.Red;
                        //src.SetPixel(x, y, newColor);
                        //newBitmap.SetPixel(x, y, Color.Yellow);
                    }
                }
                newBitmap.Save(@"Random.bmp");

                // Set the PictureBox to display the image.
                //pictureBox1.Image = src;

                // Display the pixel format in Label1.
                //Label1.Text = "Pixel format: " + image1.PixelFormat.ToString();

            }
            catch (ArgumentException)
            {
                MessageBox.Show("There was an error." +
                    "Check the path to the image file.");
            }
        }
    }
}
