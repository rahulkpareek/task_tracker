using System.Runtime.InteropServices;
using System.Text.Json;

public class Tracker
{
    private readonly string tasksFilePath;
    private List<MyTask> tasks;

    public Tracker()
    {
        tasksFilePath = Path.Combine(Directory.GetCurrentDirectory(), "mytasks.json");
        tasks = new List<MyTask>();
    }

    private bool Initialize()
    {
        try
        {
            if (!File.Exists(tasksFilePath))
            {
                File.Create(tasksFilePath).Close();
                //PrintSuccessMessage("Tasks file created successfully.");
            }
            else
            {
                if (new FileInfo(tasksFilePath).Length > 0)
                {
                    string json = File.ReadAllText(tasksFilePath);
                    tasks = JsonSerializer.Deserialize<List<MyTask>>(json) ?? new List<MyTask>();
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            PrintErrorMessage($"Error initializing tasks: {ex.Message}");
            return false;
        }
    }

    private void PrintSuccessMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private void PrintErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private void PrintQuestion(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(message);
        Console.ResetColor();
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
            Console.Write("\nEnter command: ");
            string? command = Console.ReadLine()?.Trim().ToLower();

            if (string.IsNullOrEmpty(command))
                continue;

            string input = command.Split(' ')[0];

            switch (input)
            {
                case "exit":
                    Console.WriteLine("Goodbye!");
                    return;
                case "add":
                    try
                    {
                        string? description;

                        if (command.Length > 4 && command.StartsWith("add "))
                        {
                            description = command.Substring(4).Trim('\'', '"');
                        }
                        else
                        {
                            PrintQuestion("Enter task description: ");
                            description = Console.ReadLine();
                        }

                        var newTask = new MyTask
                        {
                            Description = string.IsNullOrWhiteSpace(description) ? "no description given" : description,
                        };

                        tasks.Add(newTask);
                        string updatedJson = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
                        File.WriteAllText(tasksFilePath, updatedJson);
                        PrintSuccessMessage("Task added successfully!");
                    }
                    catch (Exception ex)
                    {
                        PrintErrorMessage($"Error adding task: {ex.Message}");
                    }
                    break;
                case "list":
                    try
                    {
                        if (tasks.Count == 0)
                        {
                            PrintSuccessMessage("No current tasks.");
                            break;
                        }

                        for (int i = 0; i < tasks.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {tasks[i].Description}");
                        }
                    }
                    catch (Exception ex)
                    {
                        PrintErrorMessage($"Error listing tasks: {ex.Message}");
                    }
                    break;
                case "update":
                    try
                    {
                        if (tasks.Count == 0)
                        {
                           PrintSuccessMessage("No tasks to update.");
                            break;
                        }
                        
                        Console.WriteLine("Current tasks:");
                        for (int i = 0; i < tasks.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {tasks[i].Description}");
                        }

                        string[] parts = command.Split(' ', 3);
                        if (parts.Length >= 3)
                        {
                            // Handle command line update: "update <number> <description>"
                            if (int.TryParse(parts[1], out int taskNum) && taskNum > 0 && taskNum <= tasks.Count)
                            {
                                string newDescription = parts[2].Trim('\'', '"');
                                tasks[taskNum - 1].Description = newDescription;
                                
                                string updatedJson = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
                                File.WriteAllText(tasksFilePath, updatedJson);
                                PrintSuccessMessage("Task updated successfully!");
                            }
                            else
                            {
                                PrintErrorMessage("Invalid task number.");
                            }
                        }
                        else
                        {
                            // Interactive update
                            PrintQuestion("Enter task number to update: ");
                            if (int.TryParse(Console.ReadLine(), out int taskNum) && taskNum > 0 && taskNum <= tasks.Count)
                            {
                                PrintQuestion("Enter new description: ");
                                string? newDescription = Console.ReadLine();
                                tasks[taskNum - 1].Description = newDescription ?? tasks[taskNum - 1].Description;
                                
                                string updatedJson = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
                                File.WriteAllText(tasksFilePath, updatedJson);
                                PrintSuccessMessage("Task updated successfully!");
                            }
                            else
                            {
                                PrintErrorMessage("Invalid task number.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        PrintErrorMessage($"Error updating task: {ex.Message}");
                    }
                    break;
                case "delete":
                    try
                    {
                        if (tasks.Count == 0)
                        {
                            PrintSuccessMessage("No tasks to delete.");
                            break;
                        }

                        Console.WriteLine("Current tasks:");
                        for (int i = 0; i < tasks.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {tasks[i].Description}");
                        }

                        string[] parts = command.Split(' ', 2);
                        if (parts.Length == 2)
                        {
                            // Handle command line delete: "delete <number>"
                            if (int.TryParse(parts[1], out int deleteNum) && deleteNum > 0 && deleteNum <= tasks.Count)
                            {
                                tasks.RemoveAt(deleteNum - 1);
                                string updatedJson = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
                                File.WriteAllText(tasksFilePath, updatedJson);
                                PrintSuccessMessage("Task deleted successfully!");
                            }
                            else
                            {
                                PrintErrorMessage("Invalid task number.");
                            }
                        }
                        else
                        {
                            // Interactive delete
                            PrintQuestion("Enter task number to delete: ");
                            if (int.TryParse(Console.ReadLine(), out int deleteNum) && deleteNum > 0 && deleteNum <= tasks.Count)
                            {
                                tasks.RemoveAt(deleteNum - 1);
                                string updatedJson = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
                                File.WriteAllText(tasksFilePath, updatedJson);
                                PrintSuccessMessage("Task deleted successfully!");
                            }
                            else
                            {
                                PrintErrorMessage("Invalid task number.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        PrintErrorMessage($"Error deleting task: {ex.Message}");
                    }
                    break;
                case "help":
                    DisplayHelp();
                    break;
                default:
                    PrintErrorMessage("\nUnknown command. Please try again.\n");
                    DisplayHelp();
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
        Console.WriteLine("  add <description> - Add a new task with a specific description");
        Console.WriteLine("  list    - List all tasks");
        Console.WriteLine("  update  - Update a task");
        Console.WriteLine("  update <number> <description> - Update a task with a specific number");    
        Console.WriteLine("  delete  - Delete a task");
        Console.WriteLine("  delete <number> - Delete a task with a specific number");
        Console.WriteLine("  exit    - Exit the application");
        Console.WriteLine("  help    - Display this help message");
    }
}


