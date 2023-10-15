namespace ServerCore
{
    internal class Program
    {
        class SessionManager
        {
            static object _lock = new object();

            public static void Test()
            {
                lock(_lock)
                {
                    UserManager.TestSession();
                }
            }
            public static void TestSession()
            {
                lock(_lock) { }
            }
        }
        class UserManager
        {
            static object _lock = new object();

            public static void Test()
            {
                lock(_lock)
                {
                    SessionManager.TestSession();
                }
            }
            public static void TestSession()
            {
                lock(_lock) { }
            }
        }

        static void Thread1()
        {
            for(int i = 0; i < 100; ++i)
            {
                SessionManager.Test();
            }
        }
        static void Thread2()
        {
            for(int i = 0; i < 100; ++i)
            {
                UserManager.Test();
            }
        }
        static void Main(string[] args)
        {
            Task t1 = new Task(Thread1);
            Task t2 = new Task(Thread2);
            t1.Start();
            Thread.Sleep(100);
            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine("종료");

        }
    }
}