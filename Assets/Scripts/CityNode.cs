using UnityEngine;
using TMPro;

public class CityNode : MonoBehaviour
{
    [SerializeField] TMP_Text nodeValueText;
    public string nodeValue;

    private void Start()
    {
        // Initialize the node value text
        if (nodeValueText != null)
        {
            nodeValueText.text = nodeValue;
        }
    }
}
