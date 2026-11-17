package main

import (
	"errors"
	"fmt"
)

// Item represents a product in the inventory.
type Item struct {
	ID       int
	Name     string
	Quantity int
	Price    float64
}

// Summary stores inventory statistics.
type Summary struct {
	ItemCount  int
	TotalUnits int
	TotalValue float64
}

// AddItem adds a new item to the inventory.
func AddItem(items []Item, item Item) ([]Item, error) {
	if item.ID <= 0 {
		return items, errors.New("item ID must be positive")
	}
	if item.Name == "" {
		return items, errors.New("item name cannot be empty")
	}
	if item.Quantity < 0 {
		return items, errors.New("quantity cannot be negative")
	}
	if item.Price < 0 {
		return items, errors.New("price cannot be negative")
	}

	for _, existing := range items {
		if existing.ID == item.ID {
			return items, errors.New("item ID already exists")
		}
	}

	items = append(items, item)
	return items, nil
}

// UpdateItem updates an existing inventory item.
func UpdateItem(items []Item, updatedItem Item) ([]Item, error) {
	if updatedItem.ID <= 0 {
		return items, errors.New("item ID must be positive")
	}
	if updatedItem.Name == "" {
		return items, errors.New("item name cannot be empty")
	}
	if updatedItem.Quantity < 0 {
		return items, errors.New("quantity cannot be negative")
	}
	if updatedItem.Price < 0 {
		return items, errors.New("price cannot be negative")
	}

	for i, item := range items {
		if item.ID == updatedItem.ID {
			items[i] = updatedItem
			return items, nil
		}
	}

	return items, errors.New("item not found")
}

// RemoveItem removes an item using its ID.
func RemoveItem(items []Item, id int) ([]Item, error) {
	if id <= 0 {
		return items, errors.New("item ID must be positive")
	}

	for i, item := range items {
		if item.ID == id {
			items = append(items[:i], items[i+1:]...)
			return items, nil
		}
	}

	return items, errors.New("item not found")
}

// CalculateSummary computes statistics concurrently.
// The result is returned through a channel.
func CalculateSummary(items []Item) <-chan Summary {
	result := make(chan Summary)

	go func() {
		var summary Summary

		summary.ItemCount = len(items)

		for _, item := range items {
			summary.TotalUnits += item.Quantity
			summary.TotalValue += float64(item.Quantity) * item.Price
		}

		result <- summary
		close(result)
	}()

	return result
}

func main() {
	fmt.Println("===== INVENTORY MANAGEMENT SYSTEM =====")

	var inventory []Item

	// 1. ADD ITEMS
	fmt.Println("\n1. Adding Inventory Items:")

	var err error

	inventory, err = AddItem(inventory, Item{
		ID: 1, Name: "Laptop", Quantity: 5, Price: 50000,
	})
	if err != nil {
		fmt.Println("Error:", err)
	} else {
		fmt.Println("Laptop added successfully.")
	}

	inventory, err = AddItem(inventory, Item{
		ID: 2, Name: "Mouse", Quantity: 10, Price: 500,
	})
	if err != nil {
		fmt.Println("Error:", err)
	} else {
		fmt.Println("Mouse added successfully.")
	}

	inventory, err = AddItem(inventory, Item{
		ID: 3, Name: "Keyboard", Quantity: 8, Price: 1200,
	})
	if err != nil {
		fmt.Println("Error:", err)
	} else {
		fmt.Println("Keyboard added successfully.")
	}

	// 2. DISPLAY ITEMS
	fmt.Println("\n2. Current Inventory:")
	displayInventory(inventory)

	// 3. UPDATE ITEM
	fmt.Println("\n3. Updating Item:")
	inventory, err = UpdateItem(inventory, Item{
		ID: 1, Name: "Laptop", Quantity: 7, Price: 50000,
	})
	if err != nil {
		fmt.Println("Error:", err)
	} else {
		fmt.Println("Laptop quantity updated successfully.")
	}

	// 4. REMOVE ITEM
	fmt.Println("\n4. Removing Item:")
	inventory, err = RemoveItem(inventory, 2)
	if err != nil {
		fmt.Println("Error:", err)
	} else {
		fmt.Println("Mouse removed successfully.")
	}

	// 5. DISPLAY FINAL INVENTORY
	fmt.Println("\n5. Final Inventory:")
	displayInventory(inventory)

	// 6. CONCURRENT SUMMARY USING GOROUTINE AND CHANNEL
	fmt.Println("\n6. Inventory Summary:")

	summaryChannel := CalculateSummary(inventory)
	summary := <-summaryChannel

	fmt.Println("Total Number of Items:", summary.ItemCount)
	fmt.Println("Total Units in Stock:", summary.TotalUnits)
	fmt.Printf("Total Inventory Value: Rs. %.2f\n", summary.TotalValue)

	fmt.Println("\n===== PROGRAM COMPLETED SUCCESSFULLY =====")
}

// displayInventory displays all available items.
func displayInventory(items []Item) {
	if len(items) == 0 {
		fmt.Println("Inventory is empty.")
		return
	}

	for _, item := range items {
		fmt.Printf(
			"ID: %d | Name: %s | Quantity: %d | Price: Rs. %.2f\n",
			item.ID, item.Name, item.Quantity, item.Price,
		)
	}
}
