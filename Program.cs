using System;
using AspectInjector.Broker;

namespace AspectOrientedProgramming
{
    [TraceAspect]
    static class Program
    {
        public static void Main(string[] args)
        {
            Method1(new ViewModel
            {
                Id = 1,
                Name = "Good"
            });
        }
        private static void Method1(ViewModel vm)
        {
            Console.WriteLine("Running Method 1");
        }

    }

    public class ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [Aspect(Scope.Global)]
    [Injection(typeof(TraceAspectAttribute))]
    public sealed class TraceAspectAttribute : Attribute
    {
        [Advice(Kind.Before, Targets = Target.Method | Target.Private)]
        public void TraceStart(
            [Argument(Source.Type)] Type type,
            [Argument(Source.Arguments)] object[] arguments,
            [Argument(Source.Name)] string name)
        {
            foreach (var item in arguments)
            {
                if (item is ViewModel model)
                {
                    Console.WriteLine("Validate ViewModel");
                }

            }
            Console.WriteLine($"[{DateTime.UtcNow}] Method {type.Name}.{name} started");
        }
    }
}
