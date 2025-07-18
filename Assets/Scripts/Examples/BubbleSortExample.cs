using UnityEngine;

public class BubbleSortExample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int[] numbers = { 64, 34, 25, 12, 22, 11, 90 };
        Debug.Log("Original array: " + ArrayToString(numbers));
        BubbleSort(numbers);
        Debug.Log("Sorted array: " + ArrayToString(numbers));
    }

    public void BubbleSort(int[] array)
    {
        int n = array.Length;
        bool swapped;

        for (int i = 0; i < n - 1; i++)
        {
            swapped = false;
            for (int j = 0; j < n  - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    Debug.Log($"Swapping index {j} {array[j]} and index {j + 1} {array[j + 1]}");
                    // Swap array[j] and array[j + 1]
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                    swapped = true;
                }
            }
            // If no two elements were swapped by inner loop, then break
            if (!swapped)
                break;
        }
    }

    string ArrayToString(int[] array)
    {
        return string.Join(", ", array);
    }
}
