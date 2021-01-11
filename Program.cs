using System;
using System.IO;

namespace ConsoleFileSquare
{
    class Program
    {
        static void Main(string[] args)
        {
            //СОЗДАДИМ ИСХОДНЫЙ ФАЙЛ...
            using (var f = File.Create("Input.dat"))
            {
                // инициализируем генератор случайных чисел
                var rand = new Random();            
                // заполняем файл...
                for (var i = 1; i <= 1000; i++)
                {
                    // случайное число от -500 до 499
                    var j = rand.Next(1000) - 500;
                    // переводим в массив байтов и записываем
                    var buff = BitConverter.GetBytes(j);
                    f.Write(buff, 0, buff.Length);
                }
                // закрываем исходный файл
                f.Close();
            }

            //ПОЛУЧИТЬ В ФАЙЛЕ G ВСЕ КОМПОНЕНТЫ ФАЙЛА F:
            //ЯВЛЯЮЩИЕСЯ ТОЧНЫМИ КВАДРАТАМИ

            using (var f = File.Open("Input.dat", FileMode.Open))   // открываем файл для чтения
            {
                var buff = new byte[4];
                using (var g = File.Create("Output.dat"))           // создаём выходной файл и открываем для записи
                {
                    while (f.Position < f.Length)   // пока не конец файла
                    {
                        f.Read(buff, 0, buff.Length);
                        var j = BitConverter.ToInt32(buff, 0);
                        var i = (int)Math.Round(Math.Sqrt(j));
                        if (j * j == i) // если число является точным квадратом
                        {
                            buff = BitConverter.GetBytes(j);
                            // пишем в выходной файл
                            g.Write(buff, 0, buff.Length);
                        }
                    }
                    g.Close();  // закрываем результирующий файл
                }
                f.Close();      // закрываем исходный файл
            }

        }
    }
}
