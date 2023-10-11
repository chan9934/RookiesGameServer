namespace ServerCore
{
    internal class Program
    {
        static int x = 0;
        static int y = 0;
        static int r1 = 0;
        static int r2 = 0;

        static void Thread_1()
        {
            y = 1;
            Thread.MemoryBarrier();
            r1 = x;
        }
        static void Thread_2()
        {
            x = 1;
            Thread.MemoryBarrier();
            r2 = y;
        }
        static void Main(string[] args)
        {
            int count = 0;
            while(true)
            {
                count++;

                x = y = r1 = r2 = 0;

                Task t1 = new Task(Thread_1);
                Task t2 = new Task(Thread_2);
                t1.Start();
                t2.Start();

                Task.WaitAll(t1, t2);

                if(r1 == 0 && r2 == 0)
                {
                    break;
                }
            }
            Console.WriteLine($"{count}번만에 끝냄");

        }
    }
}