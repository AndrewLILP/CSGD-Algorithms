using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public float Size => transform.localScale.x;
    public float Heigth => transform.localScale.y;

    [SerializeField] private GameObject highlightEffect;
    public void SetSize(float size)
    {
        Vector3 scale = transform.localScale;
        scale.x = size;
        transform.localScale = scale;
    }

    public void Highlight(bool bSet)
    {
        highlightEffect.SetActive(bSet);
    }

    public void SetColor(Color c)
    {
        GetComponent<SpriteRenderer>().color = c;
    }
}
