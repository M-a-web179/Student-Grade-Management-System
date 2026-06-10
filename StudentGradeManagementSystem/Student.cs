namespace StudentGradeManagementSystem;

public struct Student
{
    public string Name { get; set; }

    public int Grade { get; set; }

    public Student(string name, int grade)
    {
        Name = name;
        Grade = grade;
    }

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

    public override string ToString()
    {
        return $"{Name,-20} | Grade: {Grade,3}/100 | Category: {GetGradeCategory()}";
    }
}