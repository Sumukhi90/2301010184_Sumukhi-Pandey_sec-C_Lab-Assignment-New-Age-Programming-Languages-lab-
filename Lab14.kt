
fun main() {

    // ==========================================
    // LAB 14 - EXPERIMENT 29
    // COLLECTIONS AND FUNCTIONAL TRANSFORMATIONS
    // ==========================================

    println("========== EXPERIMENT 29 ==========")

    // 1. Mutable and Read-only List, Set and Map

    println("\n--- 1. LIST OPERATIONS ---")

    // Mutable List
    val mutableList = mutableListOf(10, 20, 30, 40)
    mutableList.add(50)
    mutableList.remove(20)
    println("Mutable List: $mutableList")
    println("Contains 30: ${mutableList.contains(30)}")
    println("Element at index 1: ${mutableList[1]}")

    // Read-only List
    val readOnlyList = listOf(100, 200, 300)
    println("Read-only List: $readOnlyList")
    println("Contains 200: ${readOnlyList.contains(200)}")
    println("Element at index 0: ${readOnlyList[0]}")

    println("\n--- 2. SET OPERATIONS ---")

    // Mutable Set
    val mutableSet = mutableSetOf(1, 2, 3, 4)
    mutableSet.add(5)
    mutableSet.remove(2)
    println("Mutable Set: $mutableSet")
    println("Contains 3: ${mutableSet.contains(3)}")

    // Read-only Set
    val readOnlySet = setOf(10, 20, 30, 40)
    println("Read-only Set: $readOnlySet")
    println("Contains 20: ${readOnlySet.contains(20)}")

    println("\n--- 3. MAP OPERATIONS ---")

    // Mutable Map
    val mutableMap = mutableMapOf(
        1 to "Apple",
        2 to "Banana",
        3 to "Mango"
    )

    mutableMap[4] = "Orange"
    mutableMap.remove(2)

    println("Mutable Map: $mutableMap")
    println("Value of key 1: ${mutableMap[1]}")
    println("Contains key 3: ${mutableMap.containsKey(3)}")

    // Read-only Map
    val readOnlyMap = mapOf(
        101 to "Rahul",
        102 to "Priya",
        103 to "Aman"
    )

    println("Read-only Map: $readOnlyMap")
    println("Value of key 102: ${readOnlyMap[102]}")
    println("Contains key 101: ${readOnlyMap.containsKey(101)}")

    // 2. map, filter and reduce transformations

    println("\n--- 4. FUNCTIONAL TRANSFORMATIONS ---")

    val numbers = listOf(1, 2, 3, 4, 5)

    println("Original List: $numbers")

    // map: doubles every element
    val mappedList = numbers.map { it * 2 }
    println("After map (double): $mappedList")

    // filter: selects even numbers
    val filteredList = numbers.filter { it % 2 == 0 }
    println("After filter (even numbers): $filteredList")

    // reduce: calculates sum of all elements
    val sum = numbers.reduce { total, number -> total + number }
    println("After reduce (sum): $sum")

    // 3. Sorting objects and grouping collections

    println("\n--- 5. SORTING STUDENTS ---")

    data class Student(val name: String, val percentage: Double)

    val students = listOf(
        Student("Rahul", 85.5),
        Student("Priya", 92.0),
        Student("Aman", 78.5),
        Student("Sneha", 88.0)
    )

    val sortedStudents = students.sortedByDescending { it.percentage }

    println("Students sorted by percentage:")
    sortedStudents.forEach {
        println("${it.name} - ${it.percentage}%")
    }

    println("\n--- 6. GROUPING COLLECTIONS ---")

    val groupedStudents = students.groupBy {
        if (it.percentage >= 85) "Distinction" else "First Class"
    }

    groupedStudents.forEach { (category, studentList) ->
        println("$category: ${studentList.map { it.name }}")
    }


    // ==========================================
    // EXPERIMENT 28 - NULL SAFETY
    // ==========================================

    println("\n========== EXPERIMENT 28 ==========")

    // 1. Nullable and Non-nullable variables

    println("\n--- 1. NULLABLE AND NON-NULLABLE ---")

    val nonNullable: String = "Hello Kotlin"
    val nullable: String? = null

    println("Non-nullable String: $nonNullable")
    println("Nullable String: $nullable")

    // A non-nullable String cannot be assigned null.
    // Example: val name: String = null  // Compile-time error

    // 2. Safe call operator (?.)

    println("\n--- 2. SAFE CALL OPERATOR ---")

    val name: String? = "Sumukhi"

    println("Name length: ${name?.length}")

    val nullName: String? = null
    println("Null name length: ${nullName?.length}")

    // 3. Elvis operator (?:)

    println("\n--- 3. ELVIS OPERATOR ---")

    val userName: String? = null

    val displayName = userName ?: "Guest"

    println("Display Name: $displayName")

    // 4. Not-null assertion operator (!!)

    println("\n--- 4. NOT-NULL ASSERTION OPERATOR ---")

    val nullValue: String? = null

    try {
        val result = nullValue!!
        println("Value: $result")
    } catch (e: NullPointerException) {
        println("Exception caught: ${e::class.simpleName}")
        println("Cannot force-unwrap a null value!")
    }

    println("\n========== LAB 14 COMPLETED ==========")
}
