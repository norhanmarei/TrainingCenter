using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TrainingCenter.Data;

// Build configuration
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Read connection string
string? connectionString = configuration.GetConnectionString("DefaultConnection");

// Validate connection string.
if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("Connection string not found.");
    return;
}


// Configure EF Core
var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseNpgsql(connectionString)
    .Options;

// Create context
using var context = new AppDbContext(options);

// Test
Console.WriteLine("================================================");

Console.WriteLine(context.Database.CanConnect()
    ? "Connected! You are ready to retrieve Data :-)"
    : "Failed to connect.");

Console.WriteLine("================================================");

RetrieveAndPrintStudents(context);


static void RetrieveAndPrintStudents(AppDbContext context)
{
    var query = context.Students
        .OrderBy(s => s.StudentId);

    PrintGeneratedSql("Students", query.ToQueryString());

    var students = query.ToList();

    if (students.Count == 0)
    {
        Console.WriteLine("No students found in the database.");
        Console.WriteLine();
        return;
    }

    Console.WriteLine("Students List:");
    Console.WriteLine("--------------");

    foreach (var student in students)
    {
        Console.WriteLine(
            $"Id: {student.StudentId}, " +
            $"Name: {student.FirstName} {student.LastName}, " +
            $"Email: {student.Email}, " +
            $"Status: {student.Status}, " +
            $"Phone: {student.PhoneNumber ?? "N/A"}");
    }

    Console.WriteLine();
    Console.WriteLine($"Total Students: {students.Count}");
    Console.WriteLine(new string('=', 70));
    Console.WriteLine();
}


static void PrintGeneratedSql(string tableName, string sqlQuery)
{
    Console.WriteLine($"Generated SQL Query for {tableName}:");
    Console.WriteLine(new string('-', 40));
    Console.WriteLine(sqlQuery);
    Console.WriteLine();
}
