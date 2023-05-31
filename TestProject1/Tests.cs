using System;
using System.Collections.Generic;
using System.Linq;
using EngineLib;
using practice13;
using practice14;
using Xunit;

namespace TestProject1
{
    public class Tests
    {
        [Fact]
        public void Test1()
        {
            MyNewCollection<Engine> col = new MyNewCollection<Engine>();
            IEnumerable<Engine> EngPowerGr100 = col.Where(e=>e.Power>100);
            
            int DieselsCount = col.Where(e=>e.GetType()==typeof(DieselEngine)).Count();
            
            int a =  col.Count(e=>e.Power<200);

            string b = col.CustomMax(e => e.Power).ToString();
            string c = col.CustomMin(e=>e.Power).ToString();

            //сортировка
            col.CustomOrderBy((x)=> (x.Power ));
            
            col.CustomOrderByDescending((x)=> (x.Power));
            Assert.True(true);
        }
    }
}