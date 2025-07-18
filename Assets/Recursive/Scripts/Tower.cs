using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{

    public Circle lastCircle => circles.Peek();
    public UnityEvent OnSelectTower;
    [SerializeField] private Transform _beginningPoint;

    private Stack<Circle> circles = new Stack<Circle>();
    private bool isSelected;

    public void HighlightCircle()
    {

        isSelected = true;
        if (circles.Count <= 0) return;
        circles.Peek().Highlight(true);
    }
    public void UnHighlightCircle()
    {
        isSelected = false;
        if (circles.Count <= 0) return;
        circles.Peek().Highlight(false);
    }
    public bool CanAdd(Circle c)
    {
        return circles.Count == 0 || c.Size < circles.Peek().Size;
    }

    public Circle GetFromTower()
    {
        return circles.Pop();
    }

    public void AddInTower(Circle c)
    {
        c.transform.parent = _beginningPoint;
        c.transform.localPosition = new Vector3(0, c.Heigth * circles.Count, 0);
        circles.Push(c);

    }

    private void OnMouseDown()
    {
        OnSelectTower.Invoke();
    }
}
