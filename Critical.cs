using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Critical
{
    class Critical
    {
        /// <summary>
        /// Структура путей и стоимости перемещения
        /// </summary>
        public struct Str
        {
            public int point1;
            public int point2;
            public int length;
            public bool Equals(Str obj)
            {
                if (this.point1 == obj.point1 && this.point2 == obj.point2 && this.length == obj.length) return true;
                else return false;
            }
            public override string ToString()
            {
                return point1.ToString() + " - " + point2.ToString() + " " + length.ToString();
            }
        }
        /// Чтение из файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<Str> Input()
        {
            Debug.WriteLine("Чтение:");
            List<Str> StQ = new List<Str>();
            using (StreamReader sr = new StreamReader("Ввод.csv"))
            {
                while (sr.EndOfStream != true)
                {
                    string[] s1 = sr.ReadLine().Split(';');
                    string[] s2 = s1[0].Split('-');
                    Debug.WriteLine(s2[0] + " - " + s2[1] + "; " + s1[1]);
                    StQ.Add(new Str { point1 = Convert.ToInt32(s2[0]), point2 = Convert.ToInt32(s2[1]), length = Convert.ToInt32(s1[1]) });

                }
            }
            return StQ;
        }
        /// <summary>
        /// Метод записи в файл решения.
        /// </summary>
        /// <param name="LPathFunc"></param>
        /// <param name="maxind"></param>
        /// <param name="max"></param>
        public void Output(List<List<Str>> LPathFunc, int maxind, int max)
        {
            using (StreamWriter sr = new StreamWriter(@"Вывод.csv", false, Encoding.Default, 10))
            {
                foreach (Str Path in LPathFunc[maxind])
                {
                    sr.Write(Path.point1 + " - " + Path.point2 + ";(" + Path.length + ") ");
                }
                sr.WriteLine("Длина " + max);
            }
        }
        /// <summary>
        /// Метод записи в файл Лога всех путей, найденных программой.
        /// </summary>
        /// <param name="LPathFunc"></param>
        public void OutputLog(List<List<Str>> LPathFunc)
        {
            Debug.WriteLine("Пути: ");
            foreach (var c in LPathFunc)
            {
                foreach (Str path in c)
                {
                    Debug.Write(path.point1 + " - " + path.point2 + ";(" + path.length + ") ");
                }
                Debug.WriteLine("");
            }
        }
        /// <summary>
        /// Рабочий метод по построению путей и подсчета длины.
        /// </summary>
        public void Work()
        {
            List<Str> LPath;//лист путей
            List<Str> StQ = Input();//лист исходных данных          
        }
    }
}
