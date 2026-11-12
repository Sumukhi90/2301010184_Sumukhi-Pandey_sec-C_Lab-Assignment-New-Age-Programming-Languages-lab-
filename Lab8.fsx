
#r "nuget: Microsoft.Data.Sqlite"
#r "nuget: MongoDB.Driver"

open System
open Microsoft.Data.Sqlite
open MongoDB.Bson
open MongoDB.Driver

// ==========================================
//                  LAB 8
// ==========================================

// EXPERIMENT 1: OOP - CLASSES, OBJECTS AND INHERITANCE

// Base Class
[<AbstractClass>]
type Shape() =
    abstract member Area: unit -> float

// Derived Class 1
type Circle(radius: float) =
    inherit Shape()

    override this.Area() =
        Math.PI * radius * radius

// Derived Class 2
type Rectangle(length: float, width: float) =
    inherit Shape()

    override this.Area() =
        length * width

let experiment1 () =
    printfn "\n===== EXPERIMENT 1: OOP AND INHERITANCE ====="

    // Creating objects
    let circle = Circle(5.0)
    let rectangle = Rectangle(4.0, 6.0)

    printfn "\nCircle Area: %.2f" (circle.Area())
    printfn "Rectangle Area: %.2f" (rectangle.Area())

    // Demonstrating Polymorphism
    printfn "\nDemonstrating Polymorphism:"

    let shapes: Shape list =
        [ circle :> Shape
          rectangle :> Shape ]

    shapes
    |> List.iter (fun shape ->
        printfn "Shape Area: %.2f" (shape.Area()))

// EXPERIMENT 2: DATABASE CRUD OPERATIONS

// PART 1: RELATIONAL DATABASE - SQLITE

let executeSql (connection: SqliteConnection) (sql: string) =
    use command = connection.CreateCommand()
    command.CommandText <- sql
    command.ExecuteNonQuery() |> ignore

let displaySqliteStudents (connection: SqliteConnection) =
    use command = connection.CreateCommand()

    command.CommandText <-
        "SELECT Id, Name, Age FROM Students ORDER BY Id"

    use reader = command.ExecuteReader()

    printfn "Current Student Records:"

    let mutable found = false

    while reader.Read() do
        found <- true
        printfn "ID: %d, Name: %s, Age: %d"
            (reader.GetInt32(0))
            (reader.GetString(1))
            (reader.GetInt32(2))

    if not found then
        printfn "No records found."

let sqliteCRUD () =
    printfn "\n--- PART 1: SQLITE DATABASE CRUD ---"

    use connection =
        new SqliteConnection("Data Source=Lab8Students.db")

    connection.Open()

    // Create Table
    executeSql connection
        "CREATE TABLE IF NOT EXISTS Students
         (Id INTEGER PRIMARY KEY, Name TEXT, Age INTEGER)"

    // Clear old sample records
    executeSql connection "DELETE FROM Students"

    // CREATE
    printfn "\n1. CREATE: Adding a student"

    executeSql connection
        "INSERT INTO Students (Id, Name, Age)
         VALUES (1, 'Asha', 20)"

    displaySqliteStudents connection

    // READ
    printfn "\n2. READ: Displaying student records"

    displaySqliteStudents connection

    // UPDATE
    printfn "\n3. UPDATE: Updating student details"

    executeSql connection
        "UPDATE Students SET Name = 'Asha Sharma', Age = 21
         WHERE Id = 1"

    displaySqliteStudents connection

    // DELETE
    printfn "\n4. DELETE: Removing student record"

    executeSql connection
        "DELETE FROM Students WHERE Id = 1"

    displaySqliteStudents connection

// PART 2: NOSQL DATABASE - MONGODB

let mongoCRUD () =
    printfn "\n--- PART 2: MONGODB DATABASE CRUD ---"

    // Connect to local MongoDB server
    let client = MongoClient("mongodb://localhost:27017")

    let database = client.GetDatabase("Lab8DB")

    let collection =
        database.GetCollection<BsonDocument>("Students")

    let filter =
        Builders<BsonDocument>.Filter.Eq("_id", BsonInt32(1))

    // Clear previous sample record
    collection.DeleteOne(filter) |> ignore

    // CREATE
    printfn "\n1. CREATE: Adding a student"

    let student =
        BsonDocument.Parse(
            """{"_id":1,"name":"Rahul","age":21}"""
        )

    collection.InsertOne(student)

    printfn "Student added successfully!"
    collection.Find(filter).ToList()
    |> List.ofSeq
    |> List.iter (fun s -> printfn "%O" s)

    // READ
    printfn "\n2. READ: Displaying student records"

    collection.Find(filter).ToList()
    |> List.ofSeq
    |> List.iter (fun s -> printfn "%O" s)

    // UPDATE
    printfn "\n3. UPDATE: Updating student details"

    let update =
        Builders<BsonDocument>.Update
            .Set("name", BsonString("Rahul Sharma"))
            .Set("age", BsonInt32(22))

    collection.UpdateOne(filter, update) |> ignore

    collection.Find(filter).ToList()
    |> List.ofSeq
    |> List.iter (fun s -> printfn "%O" s)

    // DELETE
    printfn "\n4. DELETE: Removing student record"

    collection.DeleteOne(filter) |> ignore

    let remaining = collection.Find(filter).ToList()

    if remaining.Count = 0 then
        printfn "Student deleted successfully!"
        printfn "No records found."
    else
        remaining
        |> List.ofSeq
        |> List.iter (fun s -> printfn "%O" s)

// ==========================================
//                MAIN PROGRAM
// ==========================================

printfn "\n========== LAB 8 PROGRAMS =========="

experiment1 ()

try
    sqliteCRUD ()
with ex ->
    printfn "SQLite Error: %s" ex.Message

try
    mongoCRUD ()
with ex ->
    printfn "\nMongoDB Error: %s" ex.Message
    printfn "Please ensure MongoDB Server is installed and running."

printfn "\n========== END OF LAB 8 =========="