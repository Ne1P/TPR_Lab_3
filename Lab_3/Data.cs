using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab_3
{
    public struct data
    {
        public int nVotes;
        public char fCan;
        public char sCan;
        public char tCan;

        public data(int nV, char fC, char sC, char tC)
        {
            nVotes = nV;
            fCan = fC;
            sCan = sC;
            tCan = tC;
        }
    }
    class Data
    {
        public static void WriteData()
        {
            data[] elements = new data[]{
               new data(){nVotes=24, fCan='A', sCan='B',tCan='C'},
               new data(){nVotes=25, fCan='A', sCan='C',tCan='B'},
               new data(){nVotes=26, fCan='C', sCan='B',tCan='A'},
               new data(){nVotes=25, fCan='B', sCan='C',tCan='A'},
               new data(){nVotes=12, fCan='B', sCan='A',tCan='C'}
            };

            string path = @"C:\Users\Petro\Desktop\Algoritm\TPR\Lab_3\Lab_3\elements.dat";

            try
            {
                // створення бінарного файлу
                using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
                {
                    // записуєм у файл кожне поле структури
                    foreach (data e in elements)
                    {
                        writer.Write(e.nVotes);
                        writer.Write(e.fCan);
                        writer.Write(e.sCan);
                        writer.Write(e.tCan);
                    }
                }
                // створення бінарного файлу
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    // поки не дійде до кінця файлу зчитує кожне значення
                    while (reader.PeekChar() > -1)
                    {
                        int nVotes = reader.ReadInt32();
                        char fCan = reader.ReadChar();
                        char sCan = reader.ReadChar();
                        char tCan = reader.ReadChar();

                        Console.WriteLine("Голосів: {0}  Бажає: {1} -> {2} -> {3} ",
                            nVotes, fCan, sCan, tCan);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
