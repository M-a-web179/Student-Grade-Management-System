namespace StudentGradeManagementSystem;

using System;
using System.Collections.Generic;
using System.Linq;

public class StudentGradeManager
{
    private readonly Dictionary<string, int> _students = new(StringComparer.OrdinalIgnoreCase);

    public void AddStudent(string name, int grade)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "Student name cannot be empty.");
        }

        if (grade < 0 || grade > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(grade), "Grade must be between 0 and 100.");
        }

        if (_students.ContainsKey(name))
        {
            _students[name] = grade;
            Console.WriteLine($"✓ Updated {name}'s grade to {grade}/100");
        }
        else
        {
            _students.Add(name, grade);
            Console.WriteLine($"✓ Added {name} with grade {grade}/100");
        }
    }

    public int SearchStudent(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "Student name cannot be empty.");
        }

        if (_students.TryGetValue(name, out int grade))
        {
            return grade;
        }

        return -1;
    }

    public void DisplayAllStudents()
    {
        if (_students.Count == 0)
        {
            Console.WriteLine("No students in the system yet.");
            return;
        }

        Console.WriteLine("\n" + new string('=', 70));
        Console.WriteLine("{0,-20} | {1,3} | {2}", "Student Name", "Gr", "Grade Category");
        Console.WriteLine(new string('=', 70));

        foreach (var kvp in _students.OrderBy(x => x.Key))
        {
            var student = new Student(kvp.Key, kvp.Value);
            Console.WriteLine(student.ToString());
        }

        Console.WriteLine(new string('=', 70) + "\n");
    }

    public double CalculateAverageGrade()
    {
        if (_students.Count == 0)
        {
            return 0;
        }

        return _students.Values.Average();
    }

    public int GetHighestGrade()
    {
        if (_students.Count == 0)
        {
            return -1;
        }

        return _students.Values.Max();
    }

    public int GetLowestGrade()
    {
        if (_students.Count == 0)
        {
            return -1;
        }

        return _students.Values.Min();
    }

    public int GetStudentCount() => _students.Count;

    public IEnumerable<string> GetAllStudentNames() => _students.Keys.OrderBy(x => x);

    public bool RemoveStudent(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "Student name cannot be empty.");
        }

        if (_students.Remove(name))
        {
            Console.WriteLine($"✓ Removed {name} from the system");
            return true;
        }

        return false;
    }
}