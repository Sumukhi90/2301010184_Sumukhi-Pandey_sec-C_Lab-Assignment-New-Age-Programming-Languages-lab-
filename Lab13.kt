// ============================================================
// LAB 13
// EXP 25, EXP 26 AND EXP 27
// ============================================================


// ============================================================
// EXPERIMENT 25: KOTLIN BASICS
// Hello World, Employee Bonus, Digit Average
// ============================================================

fun experiment25() {

    // 1. Hello Kotlin
    println("\n========== EXPERIMENT 25 ==========")
    println("Hello Kotlin!!!")


    // 2. Employee Bonus using when expression

    println("\nEmployee Bonus Calculation")

    val deptCode = "IT"
    val yearsOfExperience = 6
    val salary = 50000.0

    val bonusPercentage = when {
        deptCode == "IT" && yearsOfExperience > 5 -> 20
        deptCode == "HR" && yearsOfExperience > 3 -> 15
        deptCode == "Finance" -> 12
        else -> 10
    }

    val bonusAmount = salary * bonusPercentage / 100

    println("Department: $deptCode")
    println("Years of Experience: $yearsOfExperience")
    println("Salary: $salary")
    println("Bonus Percentage: $bonusPercentage%")
    println("Bonus Amount: Rs. $bonusAmount")


    // 3. Average of individual digits

    println("\nAverage of Digits")

    print("Enter an integer: ")
    val number = readln().trim().toInt()

    val digits = number.toString()
        .filter { it.isDigit() }
        .map { it.digitToInt() }

    val digitSum = digits.sum()
    val digitAverage = digitSum.toDouble() / digits.size

    println("Sum of digits: $digitSum")
    println("Number of digits: ${digits.size}")
    println("Average of digits: $digitAverage")
}


// ============================================================
// EXPERIMENT 26: FUNCTION DECLARATIONS
// Default, Named, Vararg and Higher-Order Functions
// ============================================================


// 1. Function with default parameters

fun displayMessage(name: String = "Student", course: String = "Kotlin") {
    println("Name: $name")
    println("Course: $course")
}


// 2. Function demonstrating named arguments

fun studentInfo(name: String, age: Int, city: String) {
    println("Name: $name")
    println("Age: $age")
    println("City: $city")
}


// 3. Function using vararg

fun printNumbers(vararg numbers: Int) {
    println("Numbers are:")
    for (number in numbers) {
        println(number)
    }
}


// 4. Higher-order function accepting a lambda

fun calculate(numbers: List<Int>, operation: (Int) -> Int): List<Int> {
    return numbers.map(operation)
}


fun experiment26() {

    println("\n========== EXPERIMENT 26 ==========")

    // Default arguments
    println("\nDefault Arguments:")

    displayMessage()
    println()

    displayMessage("Sumukhi")
    println()

    displayMessage("Sumukhi", "Computer Science")


    // Named arguments in different order
    println("\nNamed Arguments:")

    studentInfo(
        city = "Delhi",
        age = 21,
        name = "Sumukhi"
    )


    // Vararg
    println("\nVararg Function:")

    printNumbers(10, 20, 30, 40, 50)


    // Higher-order function and lambda
    println("\nHigher-Order Function:")

    val numbers = listOf(1, 2, 3, 4, 5)

    val squares = calculate(numbers) { it * it }

    println("Original List: $numbers")
    println("Squares: $squares")
}


// ============================================================
// EXPERIMENT 27: STUDENT CLASS
// Properties, Constructor and Member Functions
// ============================================================

class Student(
    rollNo: Int,
    name: String,
    course: String,
    percentage: Double
) {

    // Properties with private setters
    var rollNo: Int = rollNo
        private set

    var name: String = name
        private set

    var course: String = course
        private set

    var percentage: Double = percentage
        private set


    // 1. Function to accept/update student data

    fun acceptData() {

        println("\nEnter Student Details:")

        print("Enter Roll Number: ")
        rollNo = readln().toInt()

        print("Enter Name: ")
        name = readln()

        print("Enter Course: ")
        course = readln()

        print("Enter Percentage: ")
        percentage = readln().toDouble()
    }


    // 2. Function to display student details

    fun displayData() {

        println("\n----- Student Details -----")
        println("Roll Number: $rollNo")
        println("Name: $name")
        println("Course: $course")
        println("Percentage: $percentage%")
    }
}


// Experiment 27

fun experiment27() {

    println("\n========== EXPERIMENT 27 ==========")

    // Creating first Student object using primary constructor

    val student1 = Student(1, "Student One", "CSE", 0.0)

    // Accepting and updating data
    student1.acceptData()

    // Displaying student details
    student1.displayData()


    // Creating second Student object using primary constructor

    val student2 = Student(2, "Student Two", "CSE", 0.0)

    // Accepting and updating data
    student2.acceptData()

    // Displaying student details
    student2.displayData()
}


// ============================================================
// MAIN FUNCTION
// ============================================================

fun main() {

    experiment25()

    experiment26()

    experiment27()

    println("\n========== LAB 13 COMPLETED ==========")
}