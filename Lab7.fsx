
open System

// ==========================================
//                  LAB 7
// ==========================================


// EXPERIMENT 1: ASYNCHRONOUS AND PARALLEL WORKFLOWS


let task1 () =
    async {
        printfn "Task 1 started: Downloading data..."

        do! Async.Sleep 2000

        printfn "Task 1 completed!"
        return "Data Downloaded"
    }

let task2 () =
    async {
        printfn "Task 2 started: Processing data..."

        do! Async.Sleep 1000

        printfn "Task 2 completed!"
        return "Data Processed"
    }

let task3 () =
    async {
        printfn "Task 3 started: Saving results..."

        do! Async.Sleep 1500

        printfn "Task 3 completed!"
        return "Results Saved"
    }

let experiment1 () =
    printfn "\n===== EXPERIMENT 1: ASYNC AND PARALLEL WORKFLOWS ====="

    printfn "Starting all three tasks concurrently...\n"

    let tasks = [ task1 (); task2 (); task3 () ]

    let results =
        tasks
        |> Async.Parallel
        |> Async.RunSynchronously

    printfn "\nAll tasks completed!"

    printfn "\nCollected Results:"

    results
    |> Array.iter (fun result -> printfn "%s" result)


// EXPERIMENT 2: HIGHER-ORDER FUNCTIONS


let experiment2 () =
    printfn "\n===== EXPERIMENT 2: HIGHER-ORDER FUNCTIONS ====="


    // PART 1: LIST.MAP

    printfn "\nPart 1: List.map - Square Each Element"

    let numbers1 = [1; 2; 3; 4; 5]
    let numbers2 = [2; 4; 6; 8]

    let squares1 = List.map (fun x -> x * x) numbers1
    let squares2 = List.map (fun x -> x * x) numbers2

    printfn "Original List: %A" numbers1
    printfn "Squared List: %A" squares1

    printfn "Original List: %A" numbers2
    printfn "Squared List: %A" squares2


    // PART 2: LIST.FILTER

    printfn "\nPart 2: List.filter - Select Even Numbers"

    let list1 = [1; 2; 3; 4; 5; 6]
    let list2 = [10; 15; 20; 25; 30]

    let even1 = List.filter (fun x -> x % 2 = 0) list1
    let even2 = List.filter (fun x -> x % 2 = 0) list2

    printfn "Original List: %A" list1
    printfn "Even Numbers: %A" even1

    printfn "Original List: %A" list2
    printfn "Even Numbers: %A" even2


    // PART 3: LIST.FOLD

    printfn "\nPart 3: List.fold - Calculate Sum"

    let values1 = [1; 2; 3; 4; 5]
    let values2 = [10; 20; 30; 40]

    let sum1 = List.fold (fun acc x -> acc + x) 0 values1
    let sum2 = List.fold (fun acc x -> acc + x) 0 values2

    printfn "Original List: %A" values1
    printfn "Sum: %d" sum1

    printfn "Original List: %A" values2
    printfn "Sum: %d" sum2


// ==========================================
//               MAIN PROGRAM
// ==========================================

printfn "\n========== LAB 7 PROGRAMS =========="

experiment1 ()

experiment2 ()

printfn "\n========== END OF LAB 7 =========="
