﻿namespace ServerCore
{
    internal class Program
    {
        
        static Mutex _lock = new Mutex();
        static int _num = 0;
        static void Thread1()
        {
            for (int i = 0; i < 100000; ++i)
            {
                _lock.WaitOne();
                _num++;
                _lock.ReleaseMutex();
            }
        }
        static void Thread2()
        {
            for (int i = 0; i < 100000; ++i)
            {
                _lock.WaitOne();
                _num--;
                _lock.ReleaseMutex();
            }
        }
        static void Main(string[] args)
        {
                Task t1 = new Task(Thread1);
                Task t2 = new Task(Thread2);

                t1.Start();
                t2.Start();
                Task.WaitAll(t1, t2);

                Console.WriteLine($"값은 : {_num}");
        }
    }
}