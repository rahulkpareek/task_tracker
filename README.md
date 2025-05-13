# Task Tracker

A simple command-line task management application built with C# that allows users to track and manage their tasks efficiently.

## Features

- Add new tasks with descriptions
- List all tasks
- Update existing tasks
- Delete tasks
- Persistent storage using JSON
- User-friendly command-line interface
- Color-coded output for better readability

## Requirements

- .NET 8.0 or later
- Windows/Linux/macOS compatible

## Installation

1. Clone the repository:
```powershell
git clone https://github.com/yourusername/task_tracker.git
cd task_tracker
```

2. Build the project:
```powershell
dotnet build
```

3. Run the application:
```powershell
dotnet run
```

## Usage

The application supports both interactive and direct command usage:

### Available Commands

- `add` - Add a new task interactively
- `add <description>` - Add a new task with a specific description
- `list` - Display all tasks
- `update` - Update a task interactively
- `update <number> <description>` - Update a specific task directly
- `delete` - Delete a task interactively
- `delete <number>` - Delete a specific task directly
- `help` - Display help information
- `exit` - Exit the application

### Examples

1. Adding a task:
```
Enter command: add Buy groceries
Task added successfully!
```

2. Listing tasks:
```
Enter command: list
1. Buy groceries
2. Call dentist
```

3. Updating a task:
```
Enter command: update 1 Buy organic groceries
Task updated successfully!
```

4. Deleting a task:
```
Enter command: delete 1
Task deleted successfully!
```

## Data Storage

Tasks are stored in a JSON file (`mytasks.json`) in the application directory. The file is automatically created when you add your first task.


## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- Built with .NET 8.0
- Uses System.Text.Json for JSON serialization
