using System;
using AspectInjector.Broker;

namespace AspactOrientedProgramming
{
    [TraceAspect]
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Method1(new ViewModel
            {
                Id = 1,
                Name = "Abdullah"
            });
        }
        private static void Method1(ViewModel vm)
        {
            Console.WriteLine("Running Method 1");
        }

        public static void Method2()
        {
            Console.WriteLine("Running Method 2");
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
        [Advice(Kind.Before, Targets = Target.Method| Target.Private)]
        public void TraceStart(
            [Argument(Source.Type)] Type type,
            [Argument(Source.Arguments)] object[] arguments,
            [Argument(Source.Name)] string name)
        {
            foreach (var item in arguments)
            {
                if (item is ViewModel model)
                {
                    Console.WriteLine(model.Id);
                }

            }
            Console.WriteLine($"[{DateTime.UtcNow}] Method {type.Name}.{name} started");
        }
    }
}
