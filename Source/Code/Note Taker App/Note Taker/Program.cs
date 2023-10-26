using Microsoft.EntityFrameworkCore;
using Note_Taker.DbModels;

namespace Note_Taker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using NoteTakerContext context = new NoteTakerContext();

            var list = context.Processes.ToList();
            foreach (var po in list)
            {
                Console.WriteLine($"{po.Name}");
                
            }
        }
    }
}