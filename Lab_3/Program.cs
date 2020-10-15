using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab_3
{
    public class Candidates
    {
        int nVotes;
        string cand;

        public void AddCandidats(int NVotes, string Cand)
        {
            nVotes = NVotes;
            cand = Cand;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ///<summary>
            ///Зчитування даних
            ///</summary>
            Console.OutputEncoding = Encoding.UTF8;

            // Data.WriteData();
            printData();


            Console.WriteLine();

            Console.ReadLine();
        }

        public static void printData()
        {
            data[] elements = new data[5];

            string path = @"C:\Users\Petro\Desktop\Algoritm\TPR\Lab_3\Lab_3\elements.dat";

            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    // Зчитування в масив
                    // поки не дійде до кінця файлу зчитує кожне значення
                    int i = 0;
                    while (reader.PeekChar() > -1)
                    {
                        int nVotes = reader.ReadInt32();
                        char fCan = reader.ReadChar();
                        char sCan = reader.ReadChar();
                        char tCan = reader.ReadChar();

                        elements[i] = new data(nVotes, fCan, sCan, tCan);
                        i++;
                    }

                }
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    // Вивід даних на консоль
                    // поки не дійде до кінця файлу зчитує кожне значення
                    Console.WriteLine("     Вхідні дані, зчитані з файлу: ");
                    while (reader.PeekChar() > -1)
                    {
                        int nVotes = reader.ReadInt32();
                        char fCan = reader.ReadChar();
                        char sCan = reader.ReadChar();
                        char tCan = reader.ReadChar();

                        Console.WriteLine("Голосів: {0}  Бажає: {1} -> {2} -> {3} ", nVotes, fCan, sCan, tCan);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Kondorse(elements);
            Borda(elements);
        }

        public static void Kondorse(data[] elements)
        {
            Console.WriteLine();
            Console.WriteLine("     Метод Кондорсе");
;            char[] row= new char[3];
            row[0] = 'A';
            row[1] = 'B';
            row[2] = 'C';
            char[] colom = new char[3];
            colom[0] = 'A';
            colom[1] = 'B';
            colom[2] = 'C';

            int[,] matrix = new int[3, 3];

            for(int i = 0; i < row.Length; i++)
            {
                for(int j=0; j < colom.Length; j++)
                {
                    foreach(var e in elements)
                    {
                        if(row[i]==e.fCan && colom[j] == e.sCan)
                        {
                            matrix[i, j] += e.nVotes;
                        }
                        if (row[i] == e.fCan && colom[j] == e.tCan)
                        {
                            matrix[i, j] += e.nVotes;
                        }
                        if (row[i] == e.sCan && colom[j] == e.tCan)
                        {
                            matrix[i, j] += e.nVotes;
                        }
                    }
                }
            }
           
            Console.Write("   ");
            foreach (var c in colom)
            {
                Console.Write(c + "  ");
            }
            Console.WriteLine();
            for (int i = 0; i < row.Length; i++)
            {
                Console.Write(row[i] + " ");
                for (int j = 0; j < colom.Length; j++)
                {
                    Console.Write(matrix[i, j]+" ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();

            if (matrix[0, 1] > matrix[1, 0])
            {
                Console.WriteLine("A -> B");
            }
            else
            {
                Console.WriteLine("B -> A");
            }
            if (matrix[0, 2] > matrix[2, 0])
            {
                Console.WriteLine("A -> C");
            }
            else
            {
                Console.WriteLine("C -> A");
            }
            if (matrix[1, 2] > matrix[2, 1])
            {
                Console.WriteLine("B -> C");
            }
            else
            {
                Console.WriteLine("C -> B");
            }
            Console.WriteLine();

            Console.Write("Відповідь: ");
            if (matrix[0, 1] > matrix[1, 0] && matrix[0, 2] > matrix[2, 0] && matrix[1, 2] > matrix[2, 1])
            {
                Console.Write("A -> B -> C");
            } else if (matrix[0, 1] > matrix[1, 0] && matrix[0, 2] > matrix[2, 0] && matrix[1, 2] < matrix[2, 1])
            {
                Console.Write("A -> C -> B");
            } else if (matrix[0, 1] < matrix[1, 0] && matrix[1, 2] > matrix[2, 1] && matrix[0, 2] > matrix[2, 0])
            {
                Console.Write("B -> A -> C");
            }
            else if (matrix[0, 1] < matrix[1, 0] && matrix[1, 2] > matrix[2, 1] && matrix[0, 2] < matrix[2, 0])
            {
                Console.Write("B -> C -> A");
            } else if (matrix[0, 2] < matrix[2, 0] && matrix[1, 2] < matrix[2, 1] && matrix[0, 1] > matrix[1, 0])
            {
                Console.Write("C -> A -> B");
            } else if (matrix[0, 2] < matrix[2, 0] && matrix[1, 2] < matrix[2, 1] && matrix[0, 1] < matrix[1, 0])
            {
                Console.Write("C -> B -> A");
            }
            Console.WriteLine();

        }

        public static void Borda(data[] elements)
        {
            Console.WriteLine();
            Console.WriteLine("     Метод Борда");
            Console.WriteLine("Проводимо розрахунки за формулою: F += Голоси * Пріоритет(3-1)");
            int[] coef = new int[3];

            foreach(var e in elements)
            {
                if (e.fCan == 'A')
                {
                    coef[0] += 3 * e.nVotes;
                }

                if (e.fCan == 'B')
                {
                    coef[1] += 3 * e.nVotes;
                }

                if (e.fCan == 'C')
                {
                    coef[2] += 3 * e.nVotes;
                }

                if (e.sCan == 'A')
                {
                    coef[0] += 2 * e.nVotes;
                }

                if (e.sCan == 'B')
                {
                    coef[1] += 2 * e.nVotes;
                }

                if (e.sCan == 'C')
                {
                    coef[2] += 2 * e.nVotes;
                }
                if (e.tCan == 'A')
                {
                    coef[0] += 1 * e.nVotes;
                }
                if (e.tCan == 'B')
                {
                    coef[1] += 1 * e.nVotes;
                }
                if (e.tCan == 'C')
                {
                    coef[2] += 1 * e.nVotes;
                }
            }

            Console.WriteLine("В сумі:");
             Console.WriteLine("А: {0};   Б: {1};    C: {2}.", coef[0], coef[1], coef[2]);

            char[] cand = new char[]
            {
                'А','Б','С'
            };

            for(int i = 0; i < coef.Length; i++)
            {
                if(coef[i]==coef.Max())
                {
                    Console.WriteLine("Відповідь: " + cand[i]+" - "+ coef.Max());
                }
            }
        }
    }
}
