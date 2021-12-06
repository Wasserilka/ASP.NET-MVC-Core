using System.Collections.Generic;
using System.Windows;

namespace Lesson_1
{
    public class ThreadLocksList<T> : List<string> 
    {
        public void AddFromThread(T Object, int ThreadId)
        {
            lock (this)
            {
                Add($"Поток {ThreadId} - {Object}\n");
            }
        }

        public void RemoveFromThread(int Index)
        {
            lock (this)
            {
                if (Count >= Index + 1)
                {
                    RemoveAt(Index);
                }
            }
        }
    }
}
