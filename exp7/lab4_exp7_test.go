package main

import (
	"strings"
	"testing"
)

// Test division function
func TestDivide(t *testing.T) {
	// Valid input
	result, err := Divide(10, 2)

	if err != nil {
		t.Fatalf("Unexpected error: %v", err)
	}

	if result != 5 {
		t.Errorf("Expected 5, got %f", result)
	}

	// Division by zero
	_, err = Divide(10, 0)

	if err == nil {
		t.Fatal("Expected division by zero error")
	}

	if !strings.Contains(err.Error(), "cannot divide by zero") {
		t.Errorf("Unexpected error message: %v", err)
	}
}

// Test square root function
func TestSquareRoot(t *testing.T) {
	// Valid input
	result, err := SquareRoot(25)

	if err != nil {
		t.Fatalf("Unexpected error: %v", err)
	}

	if result != 5 {
		t.Errorf("Expected 5, got %f", result)
	}

	// Negative number
	_, err = SquareRoot(-25)

	if err == nil {
		t.Fatal("Expected negative square root error")
	}

	if !strings.Contains(err.Error(), "negative number") {
		t.Errorf("Unexpected error message: %v", err)
	}
}

// Test input validation function
func TestValidateInput(t *testing.T) {
	// Valid input
	result, err := ValidateInput("Hello Go")

	if err != nil {
		t.Fatalf("Unexpected error: %v", err)
	}

	if result != "Hello Go" {
		t.Errorf("Expected Hello Go, got %s", result)
	}

	// Empty input
	_, err = ValidateInput("")

	if err == nil {
		t.Fatal("Expected empty input error")
	}

	if !strings.Contains(err.Error(), "input cannot be empty") {
		t.Errorf("Unexpected error message: %v", err)
	}

	// Whitespace-only input
	_, err = ValidateInput("   ")

	if err == nil {
		t.Error("Expected whitespace input error")
	}
}
