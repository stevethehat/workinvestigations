namespace Test.Interfaces{
    public interface ITest1{
        int TwoPlusTwo();
    }

    public class Test1: ITest1{

        public Test1(){
        
        }
        public int TwoPlusTwo(){
            return 4;
        }
    }

    public class Test2{
        public Test2(ITest1 test1){

        }
    }
}