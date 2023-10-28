using Microsoft.EntityFrameworkCore;
using Note_Taker.DbModels;

namespace Note_Taker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using NoteTakerContext context = new NoteTakerContext();

            var notes = context.Notes.Include(n=>n.ProcessNavigation).ToList();
            if (notes.Count==0)
            {
                Console.WriteLine(Sentences.DontHaveNotes);
            }
            else
            {
                foreach (var no in notes)
                {
                    Console.WriteLine($"{no.Id}{no.Title}");
                }
            }

            int cmd = GetClientCommand();

            if (cmd == 1)
            {
                
            }

        }

        static int GetClientCommand()
        {
            Console.WriteLine(Sentences.EnterCommand);
            Console.WriteLine(Sentences.EnterChoices);
            int cmd = int.Parse(Console.ReadLine());
            if (cmd < 1 || cmd > 4)
            {
                while (cmd < 1 || cmd > 4)
                {
                    Console.WriteLine(Sentences.EnterCommand);
                    Console.WriteLine(Sentences.EnterChoices);
                    cmd = int.Parse(Console.ReadLine());
                }
            }

            return cmd;
        }
    }
}