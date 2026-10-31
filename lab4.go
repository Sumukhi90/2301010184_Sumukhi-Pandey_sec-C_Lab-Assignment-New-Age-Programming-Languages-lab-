package main

import (
	"fmt"
	"sync"
)

// Function to calculate squares
func squares(ch chan string, wg *sync.WaitGroup) {
	defer wg.Done()

	fmt.Println("Squares Goroutine Started")

	var result string
	for i := 1; i <= 5; i++ {
		result += fmt.Sprintf("%d ", i*i)
	}

	fmt.Println("Squares Goroutine Completed")
	ch <- "Squares: " + result
}

// Function to calculate cubes
func cubes(ch chan string, wg *sync.WaitGroup) {
	defer wg.Done()

	fmt.Println("Cubes Goroutine Started")

	var result string
	for i := 1; i <= 5; i++ {
		result += fmt.Sprintf("%d ", i*i*i)
	}

	fmt.Println("Cubes Goroutine Completed")
	ch <- "Cubes: " + result
}

// Function to calculate Fibonacci numbers
func fibonacci(ch chan string, wg *sync.WaitGroup) {
	defer wg.Done()

	fmt.Println("Fibonacci Goroutine Started")

	a, b := 0, 1
	var result string

	for i := 0; i < 10; i++ {
		result += fmt.Sprintf("%d ", a)
		a, b = b, a+b
	}

	fmt.Println("Fibonacci Goroutine Completed")
	ch <- "Fibonacci: " + result
}

func main() {
	ch := make(chan string, 3)

	var wg sync.WaitGroup
	wg.Add(3)

	fmt.Println("Starting Concurrent Goroutines...")

	// Launch three goroutines
	go squares(ch, &wg)
	go cubes(ch, &wg)
	go fibonacci(ch, &wg)

	// Wait for all goroutines to complete
	go func() {
		wg.Wait()
		close(ch)
	}()

	// Collect and print results
	for result := range ch {
		fmt.Println(result)
	}

	fmt.Println("All Goroutines Completed Successfully!")
}
