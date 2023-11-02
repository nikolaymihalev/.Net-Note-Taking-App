using Microsoft.EntityFrameworkCore;
using Note_Taker.DbModels;
namespace Note_Taker;

public class Engine
{
    public static void Run()
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Clear();
        using NoteTakerContext context = new NoteTakerContext();
        GetClientNotes(context);

        int cmd = -1;
        while (cmd != 10)
        {
            cmd = GetClientCommand();
            switch (cmd)
            {
                case 1 :AddNote(context);
                    break;
                case 2: OpenNote(context);
                    break;
                case 3: DeleteNote(context);
                    break;
                case 4: UpdateNote(context);
                    break;
            }
        }

    }
    static void GetClientNotes(NoteTakerContext context)
    {
        var notes = context.Notes.Include(n=>n.ProcessNavigation).ToList();
        if (notes.Count==0)
        {
            Console.WriteLine(Sentences.DontHaveNotes);
        }
        else
        {
            Console.WriteLine("Your notes!");
            for (int i = 0; i < notes.Count; i++)
            {
                Console.WriteLine($"{notes[i].Id-1}. {notes[i].Title}");
            }
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

    static void AddNote(NoteTakerContext context)
    {
        Console.WriteLine(Sentences.AddChose);
        Console.WriteLine(Sentences.EnterTitle);
        string title = Console.ReadLine();
        if (title is null)
        {
            while (title is null)
            {
                Console.WriteLine(Sentences.EnterTitle);
                title = Console.ReadLine();
            }
        }
        Console.WriteLine(Sentences.EnterDescription);
        string description = Console.ReadLine();
        if (description is null)
        {
            while (description is null)
            {
                Console.WriteLine(Sentences.EnterDescription);
                description = Console.ReadLine();
            }
        }
        
        Console.WriteLine(Sentences.EnterProcess);
        string process = Console.ReadLine();
        if (process is null)
        {
            while (process is null)
            {
                Console.WriteLine(Sentences.EnterProcess);
                Console.WriteLine(Sentences.Processes);
                process = Console.ReadLine();
            }
        }

        int processId = -1;
        var processes = context.Processes.ToList();
        foreach (var pro in processes)
        {
            if (pro.Name == process)
            {
                processId = pro.Id;
            }
        }
        context.Notes.Add(new Note
        {
            Title = title,
            Description = description,
            Process = processId
        });
        context.SaveChanges();
    }

    static void OpenNote(NoteTakerContext context)
    {
        Console.WriteLine(Sentences.WhichNoteToOpen);
        Console.WriteLine(Sentences.EnterTitle);
        string title = Console.ReadLine();
        var note= context.Notes.FirstOrDefault(n => n.Title == title);
        if (note is not null)
        {
            OpenningNote(note);
        }
        else
        {
            Console.WriteLine(Sentences.WrongeCmd);
            Console.WriteLine();
        }
    }

    static void OpenningNote(Note note)
    {
        Console.WriteLine();
        Console.WriteLine("Openning note!");
        Console.WriteLine($"Tittle: {note.Title}");
        Console.WriteLine($"Description: {note.Description}");
        Console.WriteLine($"Process: {note.ProcessNavigation.Name}");
        Console.WriteLine();
    }

    static void DeleteNote(NoteTakerContext context)
    {
        Console.WriteLine(Sentences.WhichNoteToDelete);
        string title = Console.ReadLine();
        var note= context.Notes.FirstOrDefault(n => n.Title == title);
        if (note is not null)
        {
            OpenningNote(note);
            Console.WriteLine(Sentences.SureDelete);
            string answer = Console.ReadLine().ToLower();
            if (answer == "yes")
            {
                context.Remove(note);
                context.SaveChanges();
                Console.WriteLine("Removed");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                return;
            }
        }
        else
        {
            Console.WriteLine(Sentences.WrongeCmd);
            Console.WriteLine();
            return;
        }
    }

    static void UpdateNote(NoteTakerContext context)
    {
        Console.WriteLine(Sentences.WhichNoteToUpdate);
        string title = Console.ReadLine();
        var note= context.Notes.FirstOrDefault(n => n.Title == title);
        if (note is not null)
        {
            OpenningNote(note);
            string answer = "";
            while (answer != "no")
            {
                Console.WriteLine(Sentences.WhatToUpdate);
                string updatePart = Console.ReadLine().ToLower();
                switch (updatePart)
                {
                    case "title":
                        UpdateTitle(note,context);
                        break;
                    case "description":
                        UpdateDescription(note,context);
                        break;
                    case "process": 
                        UpdateProcess(note,context);
                        break;
            }
                Console.WriteLine(Sentences.WantToContinue);
                answer = Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine(Sentences.WrongeCmd);
            Console.WriteLine();
        }
    }

    static void UpdateTitle(Note note,NoteTakerContext context)
    {
        Console.WriteLine(Sentences.EnterNewTitle);
        note.Title = Console.ReadLine();
        context.SaveChanges();
    }

    static void UpdateDescription(Note note,NoteTakerContext context)
    {
        Console.WriteLine(Sentences.UpdateDescription);
        string type = Console.ReadLine().ToLower();
        if (type == "new")
        {
            Console.WriteLine(Sentences.EnterNewDesc);
            string desc = Console.ReadLine();
            note.Description = desc;
            context.SaveChanges();
        }
        else if (type == "add")
        {
            Console.WriteLine(Sentences.EnterAddition);
            string oldDesc = note.Description;
            string newDesc = Console.ReadLine();
            string desc = oldDesc + " " + newDesc;

            note.Description = desc;
            context.SaveChanges();
        }
    }
    static void UpdateProcess(Note note,NoteTakerContext context)
    {
        Console.WriteLine(Sentences.EnterNewProc);
        Console.WriteLine(Sentences.Processes);
        string process = Console.ReadLine();
        int processId = -1;
        var processes = context.Processes.ToList();
        foreach (var pro in processes)
        {
            if (pro.Name == process)
            {
                processId = pro.Id;
            }
        }

        note.Process = processId;
        context.SaveChanges();
    }
}