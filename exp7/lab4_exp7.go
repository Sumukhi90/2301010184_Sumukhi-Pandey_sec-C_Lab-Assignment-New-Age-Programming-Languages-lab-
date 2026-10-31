package main

import (
	"fmt"
	"math"
	"strings"
)

// Function to perform division
func Divide(a, b float64) (float64, error) {
	if b == 0 {
		return 0, fmt.Errorf("division error: cannot divide by zero")
	}
	return a / b, nil
}

// Function to calculate square root
func SquareRoot(n float64) (float64, error) {
	if n < 0 {
		return 0, fmt.Errorf("square root error: negative number is not allowed")
	}
	return math.Sqrt(n), nil
}

// Function to validate input
func ValidateInput(input string) (string, error) {
	if strings.TrimSpace(input) == "" {
		return "", fmt.Errorf("input error: input cannot be empty")
	}
	return input, nil
}

// Main function
func main() {
	fmt.Println("Experiment 7: Error Handling and Unit Testing")

	// 1. Valid division
	result, err := Divide(10, 2)
	if err != nil {
		fmt.Println(err)
	} else {
		fmt.Println("Division Result:", result)
	}

	// 2. Valid square root
	result, err = SquareRoot(25)
	if err != nil {
		fmt.Println(err)
	} else {
		fmt.Println("Square Root:", result)
	}

	// 3. Valid input
	input, err := ValidateInput("Hello Go")
	if err != nil {
		fmt.Println(err)
	} else {
		fmt.Println("Valid Input:", input)
	}

	// 4. Error: Division by zero
	_, err = Divide(10, 0)
	if err != nil {
		fmt.Println(err)
	}

	// 5. Error: Negative square root
	_, err = SquareRoot(-25)
	if err != nil {
		fmt.Println(err)
	}

	// 6. Error: Empty input
	_, err = ValidateInput("")
	if err != nil {
		fmt.Println(err)
	}
}
