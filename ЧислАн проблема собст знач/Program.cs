using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ЧислАн_проблема_собст_знач
{
    class Program
    {
        
        static double Norma(double[] vector)
        {
            double norma = 0.0;
            for (int i = 0; i < vector.Length; i++)
            {
                norma += vector[i] * vector[i];
            }
            return Math.Sqrt(norma);
        }

        static void EvNorma(double[] vector, double[] x)
        {
            double norma = Norma(vector);
            for (int i = 0; i < vector.Length; i++)
            {
                x[i] = vector[i] / norma;
            }
        }

        static void Mult(double[,] a, double[] x, double[] y)
        {
            for (int i = 0; i < x.Length; i++)
            {
                y[i] = 0;
                for (int j = 0; j < x.Length; j++)
                {
                    y[i] += a[i, j] * x[j];
                }
            }
        }

        static double Scalar(double[] x, double[] y)
        {
            double scal = 0;
            for (int i = 0; i < x.Length; i++)
            {
                scal += x[i] * y[i];
            }
            return scal;
        }

        static void Chastn_Rele(double[] y, double[] x0, double[,] matrix, int count, int k, double[] eps, double[] ym, double[] xm0, double[,] obrmatr)
        {
            double l0, l1, lm0, lm1;

            for (int i = 0; i < 5; i++)
            {
                y[i] = 1;
            }
            EvNorma(y, x0);
            Mult(matrix, x0, y);
            l1 = Scalar(y, x0) / Scalar(x0, x0);

            Console.WriteLine("                             Метод частных Рэлея");
            do
            {
                l0 = l1;
                EvNorma(y, x0);
                Mult(matrix, x0, y);
                l1 = Scalar(y, x0) / Scalar(x0, x0);
                if (Math.Abs(l1 - l0) < eps[k])
                {
                    Console.WriteLine("Лямбда: {0} Точность: {1} Итерация: {2}", l1, eps[k], count + 1);
                    if (k == 5)
                        break;
                    k++;
                    for (int i = 0; i < 5; i++)
                    {
                        Console.WriteLine("x{0} {1}", i + 1, x0[i]);
                    }

                }

                count++;
            } while (true);


            k = 0;
            count = 0;
            Console.WriteLine();
            EvNorma(ym, xm0);
            Mult(matrix, xm0, ym);
            lm1 = Scalar(ym, xm0) / Scalar(xm0, xm0);
            do
            {
                lm0 = lm1;
                EvNorma(ym, xm0);
                Mult(obrmatr, xm0, ym);
                l1 = Scalar(ym, xm0) / Scalar(xm0, xm0);
                if (Math.Abs(lm1 - lm0) < eps[k])
                {
                    if (k == 5)
                        break;
                    k++;
                }
                count++;
            } while (true);
            Console.WriteLine(" Лямбда минимальное: {0} ", 1.0 / lm1);
        }

        static void Main(string[] args)
        {
            double[,] matrix =
            {
                {10, 1, -1, 11, 1},
                {1, 10, -1, 10, -2},
                {-1, -1, 2, 1, 3},
                {11, 10, 1, 10, -0.5},
                {1, -2, 3, -0.5, 10}

            },
            obrmatr =
                {
                    { 0.015991, -0.082705, -0.089225, 0.0746555, 0.0123602}, 
                    {-0.082705, 0.0548019, -0.132733, -0.0525307, 0.0616773},
                    {-0.089225, -0.132733, 0.584806, 0.163154, -0.184908}, 
                    {0.0746555, 0.0525307, 0.163154, -0.053396, -0.0485755},
                    {0.0123602, 0.0616773, -0.184908, -0.0485755, 0.164143}
                }; 
            int count = 0, k = 0;
          
            double[] x0 = new double[5],
                 x1 = new double[5],
                 y = new double[5],
                 ym = { 1, 1, 1, 1, 1 },
                 xm0 = new double[5],
                 eps = new[] { 1E-1, 1E-2, 1E-3, 1E-4, 1E-5, 1E-6 };
            Chastn_Rele(y, x0, matrix, count, k, eps, ym, xm0, obrmatr);

            Console.ReadKey();

        }
    }
}
