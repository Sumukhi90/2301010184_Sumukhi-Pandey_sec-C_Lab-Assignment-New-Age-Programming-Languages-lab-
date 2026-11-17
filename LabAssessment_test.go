package main

import "testing"

// Test 1: Verify that an item can be added.
func TestAddItem(t *testing.T) {
	var items []Item

	items, err := AddItem(items, Item{
		ID: 1, Name: "Laptop", Quantity: 5, Price: 50000,
	})

	if err != nil {
		t.Fatalf("Unexpected error: %v", err)
	}

	if len(items) != 1 {
		t.Errorf("Expected 1 item, got %d", len(items))
	}
}

// Test 2: Verify that an item can be updated.
func TestUpdateItem(t *testing.T) {
	items := []Item{
		{ID: 1, Name: "Laptop", Quantity: 5, Price: 50000},
	}

	items, err := UpdateItem(items, Item{
		ID: 1, Name: "Laptop", Quantity: 10, Price: 50000,
	})

	if err != nil {
		t.Fatalf("Unexpected error: %v", err)
	}

	if items[0].Quantity != 10 {
		t.Errorf("Expected quantity 10, got %d", items[0].Quantity)
	}
}

// Test 3: Verify that an item can be removed.
func TestRemoveItem(t *testing.T) {
	items := []Item{
		{ID: 1, Name: "Laptop", Quantity: 5, Price: 50000},
	}

	items, err := RemoveItem(items, 1)

	if err != nil {
		t.Fatalf("Unexpected error: %v", err)
	}

	if len(items) != 0 {
		t.Errorf("Expected empty inventory, got %d items", len(items))
	}
}

// Test 4: Verify duplicate ID validation.
func TestDuplicateItem(t *testing.T) {
	items := []Item{
		{ID: 1, Name: "Laptop", Quantity: 5, Price: 50000},
	}

	_, err := AddItem(items, Item{
		ID: 1, Name: "Mouse", Quantity: 2, Price: 500,
	})

	if err == nil {
		t.Error("Expected duplicate ID error, got nil")
	}
}

// Test 5: Verify concurrent summary calculation.
func TestCalculateSummary(t *testing.T) {
	items := []Item{
		{ID: 1, Name: "Laptop", Quantity: 2, Price: 50000},
		{ID: 2, Name: "Mouse", Quantity: 3, Price: 500},
	}

	summary := <-CalculateSummary(items)

	if summary.ItemCount != 2 {
		t.Errorf("Expected 2 items, got %d", summary.ItemCount)
	}

	if summary.TotalUnits != 5 {
		t.Errorf("Expected 5 units, got %d", summary.TotalUnits)
	}

	if summary.TotalValue != 101500 {
		t.Errorf("Expected value 101500, got %.2f", summary.TotalValue)
	}
}
