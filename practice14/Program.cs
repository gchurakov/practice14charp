using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EngineLib;
using practice13;


namespace practice14
{
    internal class Program
    {
        public static void Print(IEnumerable col, string msg="")
        {
            //печать коллекции 
            Console.WriteLine('\n' + msg);
            bool is_empty = true;
            foreach (var e in col)
            {
                Console.WriteLine(e.ToString());
                is_empty = false;
            }
            if (is_empty)
                Console.Write("     Элементы отсутствуют!");
        }
        public static List<Stack<Engine>> ListsInit()
        {
            //инициализация коллекции коллекций
            Console.WriteLine("Список Списков Обектов класса Engine:\n");
            List<Stack<Engine>> lists = new List<Stack<Engine>>();
            for (int i = 0; i < new Random().Next(5, 15); i++)
            {
                lists.Add(new Stack<Engine>());
                for (int j = 0; j < new Random().Next(1, 15); j++)
                {
                    int c = new Random().Next() % 4;
                    if (c==0) lists[i].Push(new Engine(1));
                    else if (c==1) lists[i].Push(new DieselEngine(1));
                    else if (c==2) lists[i].Push(new IternCombEngine(1));
                    else lists[i].Push(new TurboJetEngine(1));
                }
                    
                foreach (Engine e in lists[i])
                {
                    Console.Write(e.ToString() + "->");
                }
            }

            return lists;
        }
        
        #region Query
        public static IEnumerable<Engine> PowerGreater100(List<Stack<Engine>> lists)
        {
            //выборка
            var subset = from list in lists
                                                from elem in list 
                                                where elem.Power > 100 
                                                select elem;
            return subset;
        }
        
        public static int WeightLess170Num(List<Stack<Engine>> lists)
        {
            //счетчик
            int count = (from list in lists
                        from elem in list
                        where elem.Weight < 170 
                        select elem).Count();
            
            return count;
        }
        
        public static IEnumerable<Engine> Diesels(List<Stack<Engine>> lists)
        {
            //выборка
            var subset = (from list in lists
                from elem in list
                where elem.GetType()==typeof(DieselEngine)
                select elem);
            
            return subset;
        }
        
        public static  IEnumerable<IGrouping<string,Engine>> GroupsByFuel(List<Stack<Engine>> lists)
        {
            //группировка
            var fuelTypeGroups = from list in lists
                from eng in list
                group eng by eng.GetFuelType();
            return fuelTypeGroups;
        }
        
        #endregion

        #region Extensions
        
        public static  IEnumerable<Engine> PowerGreater100E(List<Stack<Engine>> lists)
        {
            //выборка
            var engs = lists
                .SelectMany(l=>l
                    .Where(e=>e.Power>100)
                    .Select(e=>e));
            return engs;
        }

        public static int WeightLess170NumE(List<Stack<Engine>> lists)
        {
            //выборка -> счетчик
            int count = lists
                .SelectMany(l => l
                    .Where(e=>e.Power<170)
                    .Select(e=>e))
                .Count();
            return count;
        }
        

        public static double DieselsAverageWeightE(List<Stack<Engine>> lists)
        {
            //аггрегация
            double weight = lists
                .SelectMany(s=>s
                    .Where(e=>e.GetType()==typeof(DieselEngine))
                    .Select(e=>e))
                .Average(e=>e.Weight);
            
            return weight;
        }

        public static  IEnumerable<IGrouping<string,Engine>> GroupsByFuelE(List<Stack<Engine>> lists)
        {
            //группировка
            var fuelTypeGroups = lists
                .SelectMany(s=>s
                    .Select(e => e))
                .GroupBy(e=>e.GetFuelType());
            return fuelTypeGroups;
        }

            
        #endregion
        
        
        public static void Main(string[] args)
        {
            List<Stack<Engine>> lists = ListsInit();
            
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("                                 Query\n");
            #region query

            var powerGr100 = PowerGreater100(lists);
            Print(powerGr100,"Мощность > 100");
            
            int weightLess170Num = WeightLess170Num(lists);
            Console.WriteLine($"Количество Двигателей ( Масса < 270 ) : {weightLess170Num}");

            var intersectDieselsPowerGr100 = Diesels(lists).Intersect(powerGr100);
            Print(intersectDieselsPowerGr100,"\n\nDieselEngine && Мощность > 100");

            Console.WriteLine("Max Eng Power = {0}", (from list in lists from elem in list select elem).Max());
            Console.WriteLine("Min Eng Power = {0}", (from list in lists from elem in list select elem).Min());

            var fuelTypeGroups = GroupsByFuel(lists);
            foreach(var fuel in fuelTypeGroups)//перебор групп
            {
                Console.WriteLine(fuel.Key);
 
                foreach(var s in fuel)//перебор в группе
                {
                    Console.WriteLine(s.ToString());
                }
                Console.WriteLine(); // для разделения между группами
            }
            #endregion
            
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("                                     Extensions\n");
            #region Extensions

            var PowerG100 = PowerGreater100E(lists);
            Print(PowerG100,  "Мощность > 100");
            
            int CountWeightL170N = WeightLess170NumE(lists);
            Console.Write($"\n\nКоличестов Двигателей масса < 170 : {CountWeightL170N}");

            Console.WriteLine($"\nСредняя Масса Дизельных двигателей : {DieselsAverageWeightE(lists)}");
            
            var fuelTypeGroupsE = GroupsByFuelE(lists);
            foreach(var fuel in fuelTypeGroupsE)//перебор групп
            {
                Console.WriteLine(fuel.Key);
 
                foreach(var s in fuel)//перебор в группе
                {
                    Console.WriteLine(s.ToString());
                }
                Console.WriteLine(); // для разделения между группами
            }
            
            #endregion
            
            Console.WriteLine("\n------------------------------------------------------------------------------------------");
            Console.WriteLine("                                         MyNewCollection<Engine> Extensions");
            #region Methods

            MyNewCollection<Engine> col = new MyNewCollection<Engine>();
            col.Add(new Engine(1));
            col.Add(new DieselEngine(1));
            col.Add(new TurboJetEngine(1));
            col.Add(new IternCombEngine(1));
            col.Add(new Engine(1));
            col.Add(new DieselEngine(1));
            col.Add(new TurboJetEngine(1));
            col.Add(new IternCombEngine(1));
            col.Add(new Engine(1));
            col.Add(new DieselEngine(1));
            col.Add(new TurboJetEngine(1));
            col.Add(new IternCombEngine(1));
            Print(col, "MyNewCollection<Engine>");
            
            Console.WriteLine("\n\nЗапросы на выборку:");
            
            var EngPowerGr100 = col.Where(e=>e.Power>100).Select(eng=>eng);
            Print(EngPowerGr100, "Мощность > 100");
            
            int DieselsCount = col.Where(e=>e.GetType()==typeof(DieselEngine)).Select(eng=>eng).Count();
            Console.WriteLine("\nКоличество элементов в коллекции DieselEngine : " + DieselsCount);
            
            Console.WriteLine("\nКоличество элементов в коллекции 'Мощность < 200' : " + col.Count(e=>e.Power<200));
            
            Console.WriteLine($"\nMAX Power eng: {col.CustomMax(e=>e.Power).ToString()}");
            Console.WriteLine($"MIN Power eng: {col.CustomMin(e=>e.Power).ToString()}\n");

            //сортировка
            col.CustomOrderBy(x=> x.Power);
            Print(col, "Коллекция, упорядоченная по Мощности двигателей  : ");
            
            col.CustomOrderByDescending(x=> x.Power);
            Print(col, "Коллекция, упорядоченная ( по убыванию ) по Мощности двигателей : ");

            #endregion
        }
    }
}
