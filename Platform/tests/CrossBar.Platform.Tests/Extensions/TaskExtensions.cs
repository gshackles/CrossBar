using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossBar.Platform.Tests.Extensions
{
    public static class TaskExtensions
    {
        public static T Test<T>(this Task<T> task)
        {
            task.Wait();

            return task.Result;
        }
    }
}
