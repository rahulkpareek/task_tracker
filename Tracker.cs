using System.Text.Json;

public class Tracker
{
    private readonly string tasksFilePath;

    public Tracker()
    {
        tasksFilePath = Path.Combine(Directory.GetCurrentDirectory(), "mytasks.json");
    }

    private bool Initialize()
    {
        try
        {
            if (!File.Exists(tasksFilePath))
            {
                File.Create(tasksFilePath).Close();
                Console.WriteLine($"Created mytasks.json at: {tasksFilePath}");
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred while initializing Task Tracker:");
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
            return false;
        }
    }    
    
    public void Run()
    {
        DisplayWelcomeMessage();

        DisplayHelp();

        if (!Initialize())
        {
            return;
        }

        while (true)
        {
            string? command = Console.ReadLine()?.Trim().ToLower();

            if (string.IsNullOrEmpty(command))
                continue;

            switch (command)
            {
                case "exit":
                    Console.WriteLine("Goodbye!");
                    return;
                case "add":
                    // TODO: Implement add task
                    Console.WriteLine("Adding task...");
                    break;
                case "list":
                    // TODO: Implement list tasks
                    Console.WriteLine("Listing tasks...");
                    break;
                case "update":
                    // TODO: Implement update task
                    Console.WriteLine("Updating task...");
                    break;
                case "delete":
                    // TODO: Implement delete task
                    Console.WriteLine("Deleting task...");
                    break;
                default:
                    Console.WriteLine("Unknown command. Please try again.");
                    break;
            }
        }
    }

    private void DisplayWelcomeMessage()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"
            ╔══════════════════════════════════════╗
            ║                                      ║
            ║      WELCOME TO TASK TRACKER         ║
            ║           Version 1.0.0              ║
            ║                                      ║
            ╚══════════════════════════════════════╝
        ");
        Console.ResetColor();
    }

    private void DisplayHelp()
    {
        Console.WriteLine("\nAvailable commands:");
        Console.WriteLine("  add     - Add a new task");
        Console.WriteLine("  list    - List all tasks");
        Console.WriteLine("  update  - Update a task");
        Console.WriteLine("  delete  - Delete a task");
        Console.WriteLine("  exit    - Exit the application");
        Console.Write("\nEnter command: ");
    }
}


