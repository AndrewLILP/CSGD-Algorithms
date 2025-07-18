using UnityEngine;

public class RecursiveExample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HierachySearch(transform);
    }

    void HierachySearch(Transform current)
    {
        if (current == null) return; // Check if the current transform is null to avoid NullReferenceException
        
        Color randomColor = new Color(Random.value, Random.value, Random.value); // Generate a random color
        Renderer renderer = current.GetComponent<Renderer>(); // Get the Renderer component of the current transform
        if (renderer != null) // Check if the Renderer component exists
        {
            renderer.material.color = randomColor; // Set the material color to the random color
        }

        Debug.Log("Set: " + current.gameObject.name + "to the colour:" + randomColor); // Log the name and color of the current transform

        foreach (Transform child in current) // Iterate through each child transform of the current transform
        {
            HierachySearch(child); // Recursive call to search through child transforms

        }
    }
}
