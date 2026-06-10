# Student Grade Management System

A professional, feature-rich C# console application for managing student records and grades using modern .NET practices.

##  Features

### Core Requirements 
- **Add Student**: Input student name and grade, store in a `Dictionary`
- **Display All Students**: List all students with their grades in a formatted table
- **Search Student**: Look up a student by name and display their grade
- **Calculate Average Grade**: Compute the average of all grades using `LINQ`
- **Find Highest/Lowest Grade**: Use `Max()` and `Min()` to find extremes
- **Error Handling**: Comprehensive exception handling for invalid inputs and edge cases
- **Remove Student**: Delete a student from the system

### Additional Features
- **Grade Categories**: Enum-based letter grades (A, B, C, D, F)
- **Formatted Output**: Professional ASCII-formatted tables and menus
- **Case-Insensitive Search**: Find students by name regardless of case
- **Input Validation**: All inputs are validated before processing
- **Color-Coded Console Output**: Visual feedback for success/errors/warnings

##  Project Structure

```
StudentGradeManagementSystem/
Program.cs                      # Main program with menu-driven interface
Student.cs                      # Student struct definition
 GradeCategory.cs               # Enum for grade categories (A-F)
 StudentGradeManager.cs         # Core business logic with Dictionary operations
 StudentGradeManagementSystem.csproj
```

##  Architecture & Design

### Student Struct
```csharp
public struct Student
{
    public string Name { get; set; }
    public int Grade { get; set; }
    
    public GradeCategory GetGradeCategory() { ... }
}
```

### GradeCategory Enum
```csharp
public enum GradeCategory
{
    A = 90,  // 90-100
    B = 80,  // 80-89
    C = 70,  // 70-79
    D = 60,  // 60-69
    F = 0    // Below 60
}
```

### StudentGradeManager Class
Manages all student records using a `Dictionary<string, int>`:
- **AddStudent(name, grade)** - Add or update a student
- **SearchStudent(name)** - Find a student's grade
- **DisplayAllStudents()** - Show all students in table format
- **CalculateAverageGrade()** - Compute average using `LINQ.Average()`
- **GetHighestGrade()** - Find max grade using `LINQ.Max()`
- **GetLowestGrade()** - Find min grade using `LINQ.Min()`
- **RemoveStudent(name)** - Delete a student

## Getting Started

### Prerequisites
- .NET SDK 10.0 or later
- Windows, macOS, or Linux

### Installation & Build

1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/Student-Grade-Management-System.git
   cd Student-Grade-Management-System
   ```

2. **Navigate to project directory**:
   ```bash
   cd StudentGradeManagementSystem
   ```

3. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

4. **Build the project**:
   ```bash
   dotnet build
   ```

   For Release build:
   ```bash
   dotnet build --configuration Release
   ```

### Running the Application

**Development mode**:
```bash
dotnet run
```

**Release mode**:
```bash
dotnet run --configuration Release
```

**Execute compiled binary**:
```bash
# Windows
./bin/Debug/net10.0/StudentGradeManagementSystem.exe

# Linux/macOS
./bin/Debug/net10.0/StudentGradeManagementSystem
```

## 📖 Usage Guide

### Main Menu
```
╔════════════════════════════════╗
║         MAIN MENU              ║
╠════════════════════════════════╣
║ 1. Add a new student           ║
║ 2. Search for a student        ║
║ 3. Display all students        ║
║ 4. Calculate average grade     ║
║ 5. Find highest grade          ║
║ 6. Find lowest grade           ║
║ 7. Remove a student            ║
║ 8. Exit                        ║
╚════════════════════════════════╝
```

### Example Workflow

#### 1. Add a Student
```
Enter your choice (1-8): 1
Enter student name: John Smith
Enter grade (0-100): 85
✓ Added John Smith with grade 85/100
```

#### 2. Search for a Student
```
Enter your choice (1-8): 2
Enter student name to search: John Smith
✓ Found: John Smith           | Grade: 85/100 | Category: B
```

#### 3. Display All Students
```
Enter your choice (1-8): 3

Student Name          Gr  Grade Category

Alice Johnson        | 92 | A
Bob Wilson           | 78 | C
John Smith           | 85 | B

```

#### 4. Calculate Average
```
Enter your choice (1-8): 4
Average grade of all students: 85.00/100
```

#### 5. Find Highest/Lowest Grades
```
Enter your choice (1-8): 5
Highest grade in the system: 92/100 (Grade: A)
```

##  Error Handling

The application handles the following error scenarios:

### Input Validation Errors
| Error | Handling |
| **Empty student name** | `ArgumentNullException` with message |
| **Non-numeric grade** | `ArgumentException` with validation message |
| **Grade out of range (0-100)** | `ArgumentOutOfRangeException` |
| **Invalid menu choice** | Display error message, re-prompt user |
| **Student not found** | Display "not found" message, return to menu |

### Example Error Handling
```csharp
try
{
    Console.Write("Enter grade (0-100): ");
    if (!int.TryParse(Console.ReadLine(), out int grade))
    {
        throw new ArgumentException("Invalid input. Please enter a numeric grade.");
    }
    
    manager.AddStudent(name, grade);  // Validates 0-100 range
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"✗ Error: {ex.Message}");
    Console.ResetColor();
}
```

##  Code Highlights

### Dictionary Operations with LINQ
```csharp
// Average grade
public double CalculateAverageGrade()
{
    if (_students.Count == 0) return 0;
    return _students.Values.Average();
}

// Highest and lowest grades
public int GetHighestGrade() => _students.Values.Max();
public int GetLowestGrade() => _students.Values.Min();
```

### Grade Category Mapping
```csharp
public GradeCategory GetGradeCategory()
{
    return Grade switch
    {
        >= 90 => GradeCategory.A,
        >= 80 => GradeCategory.B,
        >= 70 => GradeCategory.C,
        >= 60 => GradeCategory.D,
        _ => GradeCategory.F
    };
}
```

### Case-Insensitive Dictionary
```csharp
private readonly Dictionary<string, int> _students 
    = new(StringComparer.OrdinalIgnoreCase);
```

##  Testing

### Manual Test Cases

1. **Add students with valid grades**:
   - Input: Name="Alice", Grade=92
   - Expected: Student added successfully ✓

2. **Test invalid grade input**:
   - Input: Grade="abc"
   - Expected: Error message displayed ✓

3. **Test grade out of range**:
   - Input: Grade=150
   - Expected: `ArgumentOutOfRangeException` caught ✓

4. **Search for non-existent student**:
   - Input: Name="NonExistent"
   - Expected: "not found" message ✓

5. **Calculate average with multiple students**:
   - Input: Students with grades 80, 90, 70
   - Expected: Average = 80.0 ✓

6. **Test case-insensitive search**:
   - Input: Add "John Smith", Search "john smith"
   - Expected: Found (case-insensitive match) ✓

##  Optional UI Extension (Bonus Features)

### For Additional Marks: Desktop UI Version

You can extend this console application with a GUI using one of these frameworks:

#### Option 1: WPF (Windows Presentation Foundation)
```bash
dotnet new wpf -n StudentGradeManagementSystem.WPF
```
**Pros**: Native Windows, XAML support, professional appearance
**Best for**: Windows-first projects

#### Option 2: WinForms
```bash
dotnet new winforms -n StudentGradeManagementSystem.WinForms
```
**Pros**: Simple, legacy-friendly, quick to implement

#### Option 3: Avalonia (Cross-Platform)
```bash
dotnet new avalonia.mvvm -n StudentGradeManagementSystem.Avalonia
```
**Pros**: Cross-platform (Windows, macOS, Linux), modern XAML

#### Option 4: MAUI (Modern Alternative)
```bash
dotnet new maui -n StudentGradeManagementSystem.MAUI
```
**Pros**: Cross-platform, latest Microsoft framework

### Suggested GUI Features
- **Data Grid**: Display all students in a DataGrid with sorting/filtering
- **Input Forms**: Dedicated panels for adding/searching students
- **Charts**: Visualize grade distribution (bar chart, pie chart)
- **Statistics Panel**: Real-time display of average, min, max grades
- **Export**: Save student records to CSV/Excel

##  Clean Code Practices Applied

 **Meaningful Names**: Clear method and variable names
 **Single Responsibility**: Each class has one responsibility
 **DRY Principle**: No code duplication
 **XML Documentation**: Comprehensive code comments
 **Exception Handling**: Try-catch with specific error messages
 **Input Validation**: All user inputs validated
 **Consistent Formatting**: Professional ASCII UI
 **LINQ Usage**: Proper use of LINQ for collections

##  Technical Details

### .csproj Configuration
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
```

### Minimum .NET Version
- **.NET 6.0+** for all features
- Console application with no external dependencies

##  Troubleshooting

| Issue | Solution |
| **"dotnet: command not found"** | Install .NET SDK from https://dotnet.microsoft.com/download |
| **Build fails with "TargetFramework not found"** | Update .NET SDK to latest version |
| **Console colors not showing** | Ensure terminal supports ANSI colors |
| **Unicode characters not displaying** | Update Windows Terminal or use WSL2 |

## Requirements Checklist

- [x] Add a student with name and grade (Dictionary storage)
- [x] Display all students and grades
- [x] Search for a student by name
- [x] Calculate average grade (using LINQ)
- [x] Find highest/lowest grade (using Max/Min)
- [x] Error handling for invalid inputs
- [x] Error handling for non-existent students
- [x] Graceful exception handling
- [x] Struct definition (Student)
- [x] Enum usage (GradeCategory)
- [x] Clean code with comments
- [x] Console UI with menu
- [x] .NET CLI build/run instructions
- [x] GitHub-ready code

##  Video Demonstration Guide

When recording your demonstration video, cover these points:

1. **Project Overview** (15 sec)
   - Show the folder structure
   - Explain the 4 main classes

2. **Build & Run** (30 sec)
   - Run `dotnet build`
   - Run `dotnet run`

3. **Feature Demonstration** (2 min)
   - Add 3-4 students with different grades
   - Search for a student
   - Display all students
   - Show average, highest, lowest grades

4. **Error Handling Demo** (1 min)
   - Try invalid grade (non-numeric)
   - Try grade out of range
   - Search for non-existent student
   - Show error messages handled gracefully

5. **Code Explanation** (2 min)
   - Show Dictionary implementation
   - Explain StudentGradeManager class
   - Show LINQ usage (Average, Max, Min)
   - Highlight exception handling

6. **Summary** (30 sec)
   - List requirements met
   - Mention code quality practices
   - Optional: Show GUI extension possibilities

##  Deliverables Checklist

- [x] Complete C# source code
- [x] Project file (.csproj)
- [x] README.md with build instructions
- [x] Comments and documentation
- [x] Error handling throughout

##  Related Resources

- [.NET Documentation](https://learn.microsoft.com/en-us/dotnet/)
- [C# Dictionary Class](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)
- [LINQ Overview](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)
- [Exception Handling](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/exceptions/)
- [Enums in C#](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum)



