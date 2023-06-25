using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace PackerChallenge
{
    public static class Packer
    {
        public static string Pack(string filePath)
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);
            List<string> results = new List<string>();

            foreach (string line in lines)
            {
                // Split the line into weight limit and item strings
                string[] parts = line.Split(':');
                int weightLimit = int.Parse(parts[0].Trim());
                string[] itemStrings = parts[1].Split(' ');

                // Parse the item strings into a list of items
                List<Item> items = ParseItems(itemStrings);

                // Validate the weight limit
                ValidateWeightLimit(weightLimit);

                // Select the items with maximum cost within the weight limit
                List<Item> selectedItems = SelectItemsWithMaxCost(items, weightLimit);

                // Convert the selected items to a string representation
                string result = selectedItems.Any() ? string.Join(",", selectedItems.Select(item => item.Index.ToString())) : "-";

                // Add the result to the results list
                results.Add(result);
            }

            // Join the results with new lines and return
            return string.Join(Environment.NewLine, results);
        }

        private static List<Item> ParseItems(string[] itemStrings)
        {
            List<Item> items = new List<Item>();

            foreach (string itemString in itemStrings)
            {
                if (string.IsNullOrWhiteSpace(itemString))
                    continue;

                // Split the item string into index, weight, and cost
                string[] itemParts = itemString.Trim('(', ')').Split(',');

                // Parse the item properties
                int index = int.Parse(itemParts[0]);
                double weight = double.Parse(itemParts[1], CultureInfo.InvariantCulture);
                decimal cost = decimal.Parse(itemParts[2].TrimStart('€'));

                // Validate item constraints
                ValidateItemConstraints(weight, cost);

                // Create and add the item to the list
                Item item = new Item(index, weight, cost);
                items.Add(item);
            }

            return items;
        }

        private static void ValidateWeightLimit(int weightLimit)
        {
            if (weightLimit > 100)
                throw new Exception("Max weight limit exceeded. Max weight limit should be <= 100.");
        }

        private static void ValidateItemConstraints(double weight, decimal cost)
        {
            if (weight > 100 || cost > 100)
                throw new Exception("Max weight or cost of an item exceeded. Max weight and cost should be <= 100.");
        }

        private static List<Item> SelectItemsWithMaxCost(List<Item> items, int weightLimit)
        {
            List<Item> selectedItems = new List<Item>();

            // Sort the items by cost in descending order
            // and select the items that fit within the weight limit
            foreach (Item item in items.OrderByDescending(item => item.Cost))
            {
                if (item.Weight <= weightLimit)
                {
                    selectedItems.Add(item);
                    weightLimit -= (int)item.Weight;
                }
            }

            // Sort the selected items by index in ascending order
            return selectedItems.OrderBy(item => item.Index).ToList();
        }
    }
}
