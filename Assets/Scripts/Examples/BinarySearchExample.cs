using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BinarySearchExample : MonoBehaviour // Binary Search Example Script - sort the array first before searching (alphabetical order for strings, numerical order for integers)
{
    public string[] iventoryItemNames = { "axe", "bow", "crossbow", "dagger", "hammer", "mace", "spear", "sword", "wand", "shield" };

    public string targetName = "sword";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Ensure the array is sorted before performing binary search
        Array.Sort(iventoryItemNames, StringComparer.OrdinalIgnoreCase); // Sort the array in a case-insensitive manner
        Debug.Log("Starting Linear Search for iventory ID: " + targetName);
        int index = BinarySearch(iventoryItemNames, targetName);

        if (index != -1)
        {
            Debug.Log("Item found at index: " + targetName + "@ index: " + index);
        }
        else
        {
            Debug.LogWarning("Inventory item ID not found! 😒 ");
        }
    }

    int BinarySearch(string[] array, string target)
    {
        int left = 0;
        int right = array.Length - 1;

        while (left <= right)
        {
            int middle = left + (right - left) / 2;
            string middleValue = array[middle].ToLower();
            string targetValue = target.ToLower();
            
            if (targetValue == middleValue)
            {
                return middle;
            }
            else if (targetValue.CompareTo(middleValue) > 0)
            {
                left = middle + 1; // Target is in the left half
            }
            else
            {
                right = middle - 1; // Target is in the right half
            }
            Debug.Log("Checking the middle of the array value: " + array[middle]);

        }
        return -1; // Target not found
    }
}
