using Microsoft.EntityFrameworkCore;
using Note_Taker.DbModels;

namespace Note_Taker
{
    internal class Program
    {
        private const string DontHaveNotes = $"You don't have notes!";
        private const string EnterCommand = "Enter command! Only number!";
        private const string EnterChoices = "1- add; 2- open; 3- delete";
        
        static void Main(string[] args)
        {
            using NoteTakerContext context = new NoteTakerContext();

            var notes = context.Notes.Include(n=>n.ProcessNavigation).ToList();
            if (notes.Count==0)
            {
                Console.WriteLine(DontHaveNotes);
            }
            else
            {
                foreach (var no in notes)
                {
                    Console.WriteLine($"{no.Id}{no.Title}");
                }
            }
            Console.WriteLine(EnterCommand);
            Console.WriteLine(EnterChoices);
            Console.ReadLine();
        }
    }
}