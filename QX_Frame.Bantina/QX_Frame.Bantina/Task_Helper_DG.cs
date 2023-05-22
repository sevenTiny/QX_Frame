using System;
using System.Threading;
using System.Threading.Tasks;

/**
 * author:qixiao
 * create:2017-5-15 16:01:07
 * */
namespace QX_Frame.Bantina
{
    public class Task_Helper_DG
    {
        public static Task TaskRun(Action action)
        {
            return Task.Run(action);
        }
        public static void ThreadRun(Action action)
        {
            new Thread(() => { action(); }).Start();
        }
        public static void ThreadPoolRun(Action action)
        {
            ThreadPool.QueueUserWorkItem(obj => { action(); });
        }
        public static void ThreadPoolRun<T>(Action<T> action, T t)
        {
            ThreadPool.QueueUserWorkItem(obj => action(t));
        }
    }
}
