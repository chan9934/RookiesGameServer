﻿namespace ServerCore
{
    internal class Program
    {
        static volatile int _count = 0;
        static Lock _lock = new Lock();
        static void Main(string[] args)
        {
            Task t1 = new Task(
                delegate ()
                {
                    for (int i = 0; i < 100000; ++i)
                    {
                        _lock.WriteLock();
                        _lock.WriteLock();
                        _count++;
                        _lock.WriteUnLock();
                        _lock.WriteUnLock();
                    }
                }
                );
            Task t2 = new Task(
                delegate ()
                {
                    for (int i = 0; i < 100000; ++i)
                    {
                        _lock.WriteLock();
                        _count--;
                        _lock.WriteUnLock();
                    }
                }
                );
            t1.Start();
            t2.Start();
            Task.WaitAll(t1, t2);
            Console.WriteLine(_count);
        }
    }
}