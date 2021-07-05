using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Critical
{
    public class Critic
    {
        string s = "";//Строчная переменная для записи путей.
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
        public List<Str> Input(string path)
        {
            Debug.WriteLine("Чтение:");
            List<Str> StQ = new List<Str>();
            try
            {
                using (StreamReader sr = new StreamReader(path))
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
            catch 
            {
                MessageBox.Show("Вы не выбрали файл");
                Debug.WriteLine("Вы не выбрали файл");
                Environment.Exit(0);
                return StQ;
            }
        }
        /// <summary>
        /// Метод записи в файл решения.
        /// </summary>
        /// <param name="LPathFunc"></param>
        /// <param name="maxind"></param>
        /// <param name="max"></param>
        public void Output(List<List<Str>> LPathFunc, int maxind, int max, string path)
        {
            using (StreamWriter sr = new StreamWriter(path, false, Encoding.Default, 10))
            {
                foreach (Str Path in LPathFunc[maxind])
                {
                    sr.Write(Path.point1 + " - " + Path.point2 + ";");
                }
                sr.WriteLine("\nДлина " + max);
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
            MessageBox.Show("Выберите файл для чтения");
            List<Str> StQ = Input(Dialog());//лист исходных данных 
            LPath = StQ.FindAll(x => x.point1 == StQ[MinElem(StQ)].point1);//запись точки начала в лист путей
            List<List<Str>> LPathFunc = new List<List<Str>>();//лист путей и функций
            foreach (Str rb in LPath)//построение путей из начальных возможных перемещений
            {
                CreatePath(StQ, rb);//Построение пути
                LPathFunc.Add(Branches(StQ, s));//Построение ветвей
                s = "";
            }
            OutputLog(LPathFunc);//Для записи всех путей в лог
            int max = LPathFunc[0][0].length, maxind = 0;
            for (int i = 0; i < LPath.Count; i++)// подсчет стоимости путей
            {
                if (LenFunc(LPathFunc[i]) >= max)// выбор самого большого
                {
                    max = LenFunc(LPathFunc[i]);
                    maxind = i;
                }
            }
            Debug.WriteLine("Максимум " + max);
            Debug.WriteLine("Номер максимума " + maxind);
            MessageBox.Show("Выберите файл для записи");
            Output(LPathFunc, maxind, max, Dialog());//Запись в файл решения
            Environment.Exit(0);
        }
        /// <summary>
        /// Поиск начальной точки.Путем взятия самого маленького из первого столбца, которого нет во втором.
        /// </summary>
        /// <param name="StQ"></param>
        /// <returns></returns>
        public int MinElem(List<Str> StQ)
        {
            int min = StQ[0].point1, minind = 0;
            foreach (Str Path in StQ)
            {
                if (Path.point1 <= min)
                {
                    min = Path.point1;
                    minind = StQ.IndexOf(Path);
                }
            }
            return minind;
        }
        /// <summary>
        /// Поиск конечной точки, по такому же принципу что и начальную точку.
        /// </summary>
        /// <param name="StQ"></param>
        /// <returns></returns>
        public int MaxElem(List<Str> StQ)
        {
            int min = StQ[0].point2, maxind = 0;
            foreach (Str Path in StQ)
            {
                if (Path.point2 >= min)
                {
                    min = Path.point1;
                    maxind = StQ.IndexOf(Path);
                }
            }
            return maxind;
        }
        /// <summary>
        /// Метод построения пути. Работает рекурсивно.
        /// </summary>
        /// <param name="StQ"></param>
        /// <param name="minel"></param>
        /// <returns></returns>
        public int CreatePath(List<Str> StQ, Str minel)
        {
            int Lenght = 0;
            Str MoveVar = StQ.Find(x => x.point1 == minel.point1 && x.point2 == minel.point2);//Поиск возможных вариантов передвижения
            s += MoveVar.point1.ToString() + "-" + MoveVar.point2.ToString();//Пишем передвижение
            if (MoveVar.point2 == StQ[MaxElem(StQ)].point2)//Смотрим не в конце ли мы
            {
                s += ";";
                return MoveVar.length;
            }
            else
            {
                for (int i = 0; i < StQ.Count; i++)//Ищем стоимость перемещения в ту точку в которую мы пришли
                {
                    if (StQ[i].point1 == MoveVar.point2)
                    {
                        s += ",";
                        Lenght = CreatePath(StQ, StQ[i]) + MoveVar.length;
                    }
                }
            }
            return Lenght;
        }
        /// <summary>
        /// Построение ветвлений и доставляющий в начало первую половину пути до ветвления, подсчет стоимостей.
        /// </summary>
        /// <param name="LPathFunc"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public List<Str> Branches(List<Str> StQ, string s)
        {
            List<List<Str>> LBr = new List<List<Str>>();
            string[] s1 = s.Split(';');
            foreach (string st1 in s1)
            {
                if (st1 != "")
                {
                    LBr.Add(new List<Str>());
                    string[] s2 = st1.Split(',');
                    foreach (string str2 in s2)
                    {
                        if (str2 != "")
                        {
                            string[] str3 = str2.Split('-');
                            LBr[LBr.Count - 1].Add(StQ.Find(x => x.point1 == Convert.ToInt32(str3[0]) && x.point2 == Convert.ToInt32(str3[1])));
                        }
                    }
                }
            }
            foreach (List<Str> l in LBr)
            {
                if (l[0].point1 != StQ[MinElem(StQ)].point1)
                {
                    foreach (List<Str> l1 in LBr)
                    {
                        if (l1[0].point1 == StQ[MinElem(StQ)].point1)
                        {
                            l.InsertRange(0, l1.FindAll(x => l1.IndexOf(x) <= l1.FindIndex(y => y.point2 == l[0].point1)));
                        }
                    }
                }
            }
            int max = LBr[0][0].length, maxind = 0;
            for (int i = 0; i < LBr.Count; i++)
            {
                if (LenFunc(LBr[i]) >= max)
                {
                    max = LenFunc(LBr[i]);
                    maxind = i;
                }
            }
            return LBr[maxind];
        }
        /// <summary>
        /// Подсчет длины пути.
        /// </summary>
        /// <param name="StQ"></param>
        /// <returns></returns>
        public int LenFunc(List<Str> StQ)
        {
            int Lenght = 0;
            foreach (Str rb in StQ)
            {
                Lenght += rb.length;
            }
            return Lenght;
        }
        /// <summary>
        /// Метод открытия диалога
        /// </summary>
        /// <returns>Возвращает путь к файлу выбранного в диалоге.</returns>
        [STAThread]
        static string Dialog()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "Файл";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "CSV documents (.csv)|*.csv";
            dlg.ShowDialog();
            return dlg.FileName;
            if (dlg.FileName == "Файл")
            {
                MessageBox.Show("Вы не выбрали файл");
                Environment.Exit(0);
            }
        }
    }
}
