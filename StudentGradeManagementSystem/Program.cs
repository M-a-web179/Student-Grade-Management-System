namespace StudentGradeManagementSystem;

using System;

class Program
{
    private static readonly StudentGradeManager manager = new();

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        DisplayWelcomeBanner();

        bool running = true;

        while (running)
        {
            DisplayMenu();
            Console.Write("Enter your choice (1-8): ");
            string choice = Console.ReadLine() ?? "";

            try
            {
                running = ProcessMenuChoice(choice);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"✗ Error: {ex.Message}");
                Console.ResetColor();
            }

            if (running)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n✓ Thank you for using Student Grade Management System. Goodbye!");
        Console.ResetColor();
    }

    private static void DisplayWelcomeBanner()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
        Console.WriteLine("║   Student Grade Management System                         ║");
        Console.WriteLine("║   Manage student records and grades efficiently           ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");
        Console.ResetColor();
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("\n╔════════════════════════════════╗");
        Console.WriteLine("║         MAIN MENU              ║");
        Console.WriteLine("╠════════════════════════════════╣");
        Console.WriteLine("║ 1. Add a new student           ║");
        Console.WriteLine("║ 2. Search for a student        ║");
        Console.WriteLine("║ 3. Display all students        ║");
        Console.WriteLine("║ 4. Calculate average grade     ║");
        Console.WriteLine("║ 5. Find highest grade          ║");
        Console.WriteLine("║ 6. Find lowest grade           ║");
        Console.WriteLine("║ 7. Remove a student            ║");
        Console.WriteLine("║ 8. Exit                        ║");
        Console.WriteLine("╚════════════════════════════════╝");
    }

    private static bool ProcessMenuChoice(string choice)
    {
        return choice switch
        {
            "1" => AddStudent(),
            "2" => SearchStudent(),
            "3" => DisplayAllStudents(),
            "4" => CalculateAverage(),
            "5" => FindHighestGrade(),
            "6" => FindLowestGrade(),
            "7" => RemoveStudent(),
            "8" => false,
            _ => InvalidChoice(),
        };
    }

    private static bool AddStudent()
    {
        Console.WriteLine("\n--- Add a New Student ---");
        Console.Write("Enter student name: ");
        string? name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Student name cannot be empty.");
        }

        Console.Write("Enter grade (0-100): ");
        if (!int.TryParse(Console.ReadLine(), out int grade))
        {
            throw new ArgumentException("Invalid input. Please enter a numeric grade.");
        }

        manager.AddStudent(name, grade);
        return true;
    }

    private static bool SearchStudent()
    {
        Console.WriteLine("\n--- Search for a Student ---");
        Console.Write("Enter student name to search: ");
        string? name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Student name cannot be empty.");
        }

        int grade = manager.SearchStudent(name);

        if (grade >= 0)
        {
            var student = new Student(name, grade);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✓ Found: {student}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n✗ Student '{name}' not found in the system.");
            Console.ResetColor();
        }

        return true;
    }

    private static bool DisplayAllStudents()
    {
        Console.WriteLine("\n--- All Students ---");
        manager.DisplayAllStudents();
        return true;
    }

    private static bool CalculateAverage()
    {
        Console.WriteLine("\n--- Calculate Average Grade ---");

        if (manager.GetStudentCount() == 0)
        {
            Console.WriteLine("No students in the system.");
            return true;
        }

        double average = manager.CalculateAverageGrade();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Average grade of all students: {average:F2}/100");
        Console.ResetColor();
        return true;
    }

    private static bool FindHighestGrade()
    {
        Console.WriteLine("\n--- Find Highest Grade ---");

        int highest = manager.GetHighestGrade();

        if (highest >= 0)
        {
            var student = new Student("", highest);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Highest grade in the system: {highest}/100 (Grade: {student.GetGradeCategory()})");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("No students in the system.");
        }

        return true;
    }

    private static bool FindLowestGrade()
    {
        Console.WriteLine("\n--- Find Lowest Grade ---");

        int lowest = manager.GetLowestGrade();

        if (lowest >= 0)
        {
            var student = new Student("", lowest);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Lowest grade in the system: {lowest}/100 (Grade: {student.GetGradeCategory()})");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("No students in the system.");
        }

        return true;
    }

    private static bool RemoveStudent()
    {
        Console.WriteLine("\n--- Remove a Student ---");
        Console.Write("Enter student name to remove: ");
        string? name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Student name cannot be empty.");
        }

        if (!manager.RemoveStudent(name))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"✗ Student '{name}' not found.");
            Console.ResetColor();
        }

        return true;
    }

    private static bool InvalidChoice()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("✗ Invalid choice. Please enter a number between 1 and 8.");
        Console.ResetColor();
        return true;
    }
}