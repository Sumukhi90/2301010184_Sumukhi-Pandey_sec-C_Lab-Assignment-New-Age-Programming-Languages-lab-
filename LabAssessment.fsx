
#r "nuget: Microsoft.Data.Sqlite"

open System
open Microsoft.Data.Sqlite

// ==========================================
// 1. DOMAIN MODEL USING F# TYPES AND RECORDS
// ==========================================

type StudentStatus =
    | Active
    | Graduated
    | Suspended

type Student = {
    Id: int
    Name: string
    Course: string
    Marks: float
    Status: StudentStatus
}

// Convert status to database text
let statusToString status =
    match status with
    | Active -> "Active"
    | Graduated -> "Graduated"
    | Suspended -> "Suspended"

// Convert database text to F# status using pattern matching
let stringToStatus value =
    match value with
    | "Active" -> Active
    | "Graduated" -> Graduated
    | "Suspended" -> Suspended
    | _ -> failwith "Unknown student status"

// Sample domain data
let students = [
    { Id = 1; Name = "Aarav"; Course = "F# Programming"; Marks = 92.0; Status = Active }
    { Id = 2; Name = "Priya"; Course = "Database Systems"; Marks = 85.0; Status = Active }
    { Id = 3; Name = "Rahul"; Course = "F# Programming"; Marks = 68.0; Status = Graduated }
    { Id = 4; Name = "Ananya"; Course = "AI and ML"; Marks = 95.0; Status = Active }
]

// ==========================================
// 2. FUNCTIONAL DATA PROCESSING
// ==========================================

// Higher-order function: map
let studentNames =
    students
    |> List.map (fun student -> student.Name.ToUpper())

// Higher-order function: filter
let highScorers =
    students
    |> List.filter (fun student -> student.Marks >= 85.0)

// Pattern matching
let getResult marks =
    match marks with
    | m when m >= 90.0 -> "Excellent"
    | m when m >= 75.0 -> "Very Good"
    | m when m >= 60.0 -> "Good"
    | _ -> "Needs Improvement"

// Display processed data
printfn "\n===== STUDENT MANAGEMENT SYSTEM ====="

printfn "\nStudent Names (MAP):"
studentNames |> List.iter (printfn "%s")

printfn "\nHigh Scoring Students (FILTER):"
highScorers
|> List.iter (fun s -> printfn "%s - %.1f" s.Name s.Marks)

printfn "\nResults (PATTERN MATCHING):"
students
|> List.iter (fun s ->
    printfn "%s: %s" s.Name (getResult s.Marks))

// ==========================================
// 3. SQLITE DATABASE PERSISTENCE
// ==========================================

let connectionString =
    "Data Source=students.db"

let createConnection () =
    new SqliteConnection(connectionString)

// CREATE TABLE
let initializeDatabase () =
    async {
        use connection = createConnection()
        do! connection.OpenAsync() |> Async.AwaitTask

        use command = connection.CreateCommand()
        command.CommandText <-
            """
            CREATE TABLE IF NOT EXISTS Students (
                Id INTEGER PRIMARY KEY,
                Name TEXT NOT NULL,
                Course TEXT NOT NULL,
                Marks REAL NOT NULL,
                Status TEXT NOT NULL
            )
            """

        let! _ =
            command.ExecuteNonQueryAsync()
            |> Async.AwaitTask

        printfn "\nDatabase initialized successfully."
    }

// CREATE: Insert student
let addStudent (student: Student) =
    async {
        use connection = createConnection()
        do! connection.OpenAsync() |> Async.AwaitTask

        use command = connection.CreateCommand()

        command.CommandText <-
            """
            INSERT OR REPLACE INTO Students
            (Id, Name, Course, Marks, Status)
            VALUES ($id, $name, $course, $marks, $status)
            """

        command.Parameters.AddWithValue("$id", student.Id) |> ignore
        command.Parameters.AddWithValue("$name", student.Name) |> ignore
        command.Parameters.AddWithValue("$course", student.Course) |> ignore
        command.Parameters.AddWithValue("$marks", student.Marks) |> ignore
        command.Parameters.AddWithValue("$status", statusToString student.Status) |> ignore

        let! _ =
            command.ExecuteNonQueryAsync()
            |> Async.AwaitTask

        printfn "Student added: %s" student.Name
    }

// READ: Retrieve all students
let getStudents () =
    async {
        use connection = createConnection()
        do! connection.OpenAsync() |> Async.AwaitTask

        use command = connection.CreateCommand()
        command.CommandText <- "SELECT * FROM Students ORDER BY Id"

        let! reader =
            command.ExecuteReaderAsync()
            |> Async.AwaitTask

        let results = ResizeArray<Student>()

        while reader.Read() do
            results.Add({
                Id = reader.GetInt32(0)
                Name = reader.GetString(1)
                Course = reader.GetString(2)
                Marks = reader.GetDouble(3)
                Status = stringToStatus (reader.GetString(4))
            })

        return List.ofSeq results
    }

// UPDATE: Modify student marks
let updateStudentMarks id newMarks =
    async {
        use connection = createConnection()
        do! connection.OpenAsync() |> Async.AwaitTask

        use command = connection.CreateCommand()

        command.CommandText <-
            "UPDATE Students SET Marks = $marks WHERE Id = $id"

        command.Parameters.AddWithValue("$marks", newMarks) |> ignore
        command.Parameters.AddWithValue("$id", id) |> ignore

        let! affected =
            command.ExecuteNonQueryAsync()
            |> Async.AwaitTask

        printfn "Updated records: %d" affected
    }

// DELETE: Remove student
let deleteStudent id =
    async {
        use connection = createConnection()
        do! connection.OpenAsync() |> Async.AwaitTask

        use command = connection.CreateCommand()
        command.CommandText <- "DELETE FROM Students WHERE Id = $id"

        command.Parameters.AddWithValue("$id", id) |> ignore

        let! affected =
            command.ExecuteNonQueryAsync()
            |> Async.AwaitTask

        printfn "Deleted records: %d" affected
    }

// ==========================================
// 4. EXECUTE ASYNCHRONOUS CRUD OPERATIONS
// ==========================================

let main () =
    async {
        // Initialize database
        do! initializeDatabase()

        // Insert sample students
        for student in students do
            do! addStudent student

        // Read records
        printfn "\n===== DATABASE RECORDS ====="

        let! records = getStudents()

        records
        |> List.iter (fun s ->
            printfn "ID: %d | Name: %s | Course: %s | Marks: %.1f | Status: %s"
                s.Id s.Name s.Course s.Marks (statusToString s.Status))

        // Update a student
        printfn "\n===== UPDATE OPERATION ====="
        do! updateStudentMarks 2 90.0

        // Read updated records
        let! updatedRecords = getStudents()

        printfn "\nAfter updating marks:"
        updatedRecords
        |> List.iter (fun s ->
            printfn "%s - %.1f" s.Name s.Marks)

        // Delete a student
        printfn "\n===== DELETE OPERATION ====="
        do! deleteStudent 3

        // Final database records
        let! finalRecords = getStudents()

        printfn "\n===== FINAL STUDENT RECORDS ====="
        finalRecords
        |> List.iter (fun s ->
            printfn "%d | %s | %.1f | %s"
                s.Id s.Name s.Marks (statusToString s.Status))

        printfn "\nAssessment application completed successfully!"
    }

// Run application
main() |> Async.RunSynchronously
