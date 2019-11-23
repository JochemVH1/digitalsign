using System;
using Microsoft.AspNetCore.Diagnostics;

namespace digitalsign.console
{
    static class ReactiveExtensions
    {
        public static IDisposable Inspect<T>(this IObservable<T> self, string token)
        {
            return self.Subscribe(
                x => Console.WriteLine($"{token} received {x}"),
                error => Console.WriteLine($"{token} threw an exception: {error.Message}"),
                () => Console.WriteLine($"{token} completed")

            );
        }

        public static IObserver<T> OnNext<T>(this IObserver<T> self, params T[] args)
        {
            foreach (var arg in args)
            {
                self.OnNext(arg);
            }
            return self;
        }
    }
}
