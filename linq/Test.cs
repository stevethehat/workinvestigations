
using System;
using System.Collections.Generic;
using System.Linq;

namespace linq{
    public class Type1{
        public string Customer;
        public int OrderNo;
    }
    public class Type2{
        public int OrderNo;
        public int Price;
    }
    public class LinqTests{
        private readonly List<Type1> _type1List;
        private readonly List<Type2> _type2List;
        public LinqTests(){
            _type1List = new List<Type1>();
            _type2List = new List<Type2>();
            CreateElements();
        }
        public void RunTest1(){
            Console.WriteLine("test 1");
 
            var orderSums = _type2List.GroupBy(t2 => t2.OrderNo, t2 => t2.Price);
            Console.WriteLine(orderSums);

            foreach(var order in orderSums){
                Console.WriteLine($"{order.Key} = {order.Sum()}");
            }

            Console.WriteLine("done test 1");
        }
        public void RunTest2(){
            Console.WriteLine("test 2");

            int test = _type1List.Join(_type2List,
                t1 => t1.OrderNo,
                t2 => t2.OrderNo,
                (t1, t2) => new { Customer = t1.Customer, Price = t2.Price })
                .Where((cp) => cp.Customer == "steve")
                .Sum(cp => cp.Price);

            Console.WriteLine($"{test}");
            Console.WriteLine("done test 2");
        }

        public void RunTest3(){
            Console.WriteLine("test 3");
            JoinAndSum();
            JoinAndSum2();

            Console.WriteLine("done test 3");
        }
        protected int JoinAndSum(){
            int result = 0;
            var joined = _type1List.Join(_type2List,
                t1 => t1.OrderNo,
                t2 => t2.OrderNo,
                (t2, t1) => new { t1, t2 });

            var filtered = joined.Where((cp) => cp.t2.Customer == "steve");
            var sum = filtered.Sum(cp => cp.t1.Price);
            
            var orders = _type1List.Where(c => c.Customer == "steve");
            var orderLines = orders.Join(_type2List, c => c.OrderNo, ol => ol.OrderNo, (c, ol) => ol.Price);

            var sum2 = _type1List.Where(c => c.Customer == "steve")
                .Join(_type2List, c => c.OrderNo, ol => ol.OrderNo, (c, ol) => ol.Price)
                .Sum();
            return result;
        }
        protected int JoinAndSum2()
        {
            int result = 1;
            var test = from t1 in _type1List
                       join t2 in _type2List on t1.OrderNo equals t2.OrderNo
                       select new { t1.Customer, t2.Price };

            return result;
        }

        protected void CreateElements(){
            AddType1("steve", 1);
            AddType1("fred", 2);
            AddType1("steve", 3);
            AddType2(1, 10);
            AddType2(1, 10);
            AddType2(2, 15);
            AddType2(2, 12);
            AddType2(1, 10);
            AddType2(3, 10);
        }
        protected void AddType1(string customer, int order){
            _type1List.Add(new Type1() { Customer = customer, OrderNo = order});
        }
        protected void AddType2(int order, int price){
            _type2List.Add(new Type2() { OrderNo = order, Price = price });
        }
    }
}