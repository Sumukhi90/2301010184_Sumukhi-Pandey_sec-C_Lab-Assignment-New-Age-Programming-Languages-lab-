
open System
open System.Text.RegularExpressions

// ==========================================
//                 LAB 6
// ==========================================


// EXPERIMENT 1: MARKS PERCENTAGE AND DIVISION

let experiment1 () =
    printfn "\n===== EXPERIMENT 1: MARKS AND DIVISION ====="

    printf "Enter marks for Subject 1: "
    let m1 = float (Console.ReadLine())

    printf "Enter marks for Subject 2: "
    let m2 = float (Console.ReadLine())

    printf "Enter marks for Subject 3: "
    let m3 = float (Console.ReadLine())

    printf "Enter marks for Subject 4: "
    let m4 = float (Console.ReadLine())

    let total = m1 + m2 + m3 + m4
    let percentage = total / 4.0

    let division =
        match percentage with
        | p when p >= 60.0 -> "First Division"
        | p when p >= 50.0 -> "Second Division"
        | p when p >= 40.0 -> "Third Division"
        | _ -> "Fail"

    printfn "Total Marks = %.2f" total
    printfn "Percentage = %.2f%%" percentage
    printfn "Division = %s" division


// EXPERIMENT 2: PRIME NUMBER CHECK

let rec isPrime n divisor =
    if n <= 1 then
        false
    elif divisor > n / divisor then
        true
    elif n % divisor = 0 then
        false
    else
        isPrime n (divisor + 1)

let checkPrime n =
    isPrime n 2

let experiment2 () =
    printfn "\n===== EXPERIMENT 2: PRIME NUMBER ====="

    // Three test cases
    let testNumbers = [7; 10; 13]

    printfn "\nTest Cases:"

    testNumbers
    |> List.iter (fun n ->
        printfn "%d -> %b" n (checkPrime n))

    printf "\nEnter a number to check: "
    let number = int (Console.ReadLine())

    if checkPrime number then
        printfn "%d is a Prime Number" number
    else
        printfn "%d is not a Prime Number" number


// EXPERIMENT 3: STRING PROCESSING


// PART 1: REVERSE STRING AND PALINDROME CHECK

let reverseString (s: string) =
    String(s.ToCharArray() |> Array.rev)

let isPalindrome (s: string) =
    s.ToLowerInvariant() = (reverseString s).ToLowerInvariant()


// PART 2: STRING TRANSFORMATION

let transformString (s: string) =
    match s with
    | x when x.Length > 0 && (x |> Seq.forall Char.IsLetter) ->
        x.ToUpper()
    | x when x.Length > 0 && (x |> Seq.forall Char.IsDigit) ->
        x + x
    | _ -> s


// PART 3: EMAIL VALIDATION

let validateEmail (email: string) =
    Regex.IsMatch(email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$")


let experiment3 () =
    printfn "\n===== EXPERIMENT 3: STRING PROCESSING ====="

    // PART 1 TEST CASES

    printfn "\nPart 1: Reverse and Palindrome"

    let strings = ["madam"; "hello"; "level"]

    strings
    |> List.iter (fun s ->
        printfn "Original: %s" s
        printfn "Reversed: %s" (reverseString s)
        printfn "Palindrome: %b\n" (isPalindrome s))


    // PART 2 TEST CASES

    printfn "\nPart 2: String Transformation"

    let inputs = ["hello"; "1234"; "hello123"]

    inputs
    |> List.iter (fun s ->
        printfn "%s -> %s" s (transformString s))


    // PART 3 TEST CASES

    printfn "\nPart 3: Email Validation"

    let emails =
        ["student@gmail.com"; "abc@yahoo.in"; "invalid-email"]

    emails
    |> List.iter (fun email ->
        printfn "%s -> %b" email (validateEmail email))


// ==========================================
//              MAIN PROGRAM
// ==========================================

printfn "\n========== LAB 6 PROGRAMS =========="

experiment1 ()
experiment2 ()
experiment3 ()

printfn "\n========== END OF LAB 6 =========="
