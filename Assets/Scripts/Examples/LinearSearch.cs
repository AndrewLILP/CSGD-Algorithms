using UnityEngine;

public class LinearSearch : MonoBehaviour
{
    public int[] iventoryIDs = { 3403, 6504, 4705, 7106, 3407, 3408, 3409, 3410, 3411, 3412 };

    public int targetID = 3407;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Starting Linear Search for iventory ID: " + targetID);
        int index = LinearSearcher(iventoryIDs, targetID);

        if (index != -1)
        {
            Debug.Log("Item found at index: " + targetID + "@ index: " + index);
        }
        else
        {
            Debug.LogWarning("Inventory item ID not found! 😒 " );
        }
    }
    private int LinearSearcher(int[] array, int target)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Debug.Log("Checking index " + i + "Value: " + array[i]);

            if (array[i] == target)
            {
                return i; // Return the index of the found item
            }
        }
        return -1; // Return -1 if the item is not found
    }
}