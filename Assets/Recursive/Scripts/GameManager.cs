using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Tower[] towers;
    //[SerializeField] private Tower towerA;
    //[SerializeField] private Tower towerB;
    //[SerializeField] private Tower towerC;

    [SerializeField] private int amountOfPieces;
    [SerializeField] private float maxSize = 3;
    [SerializeField] private Circle piecePrefab;

    private Tower lastSelectedTower;
    private void Awake()
    {
        float sizeIncrement = maxSize / amountOfPieces;

        Color color = Color.HSVToRGB(0, 1, 1);

        for(int i = 0; i < amountOfPieces; i++)
        {
            Circle c = Instantiate(piecePrefab);
            c.SetSize(maxSize - (sizeIncrement * i));
            color = Color.HSVToRGB(0.1f * i, 1, 1);
            c.SetColor(color);
            towers[0].AddInTower(c);

        }
    }

    private void Start()
    {
        towers[0].OnSelectTower.AddListener(() => SelectTower(towers[0]));
        towers[1].OnSelectTower.AddListener(() => SelectTower(towers[1]));
        towers[2].OnSelectTower.AddListener(() => SelectTower(towers[2]));

        SolveHanoi(amountOfPieces, 0, 2);

    }
    private void SelectTower(Tower t)
    {
        t.HighlightCircle();
        if (lastSelectedTower)
        {
            if (t == lastSelectedTower)
            {
                t.UnHighlightCircle();
            }
            else
            {
                MoveTowerToTower(lastSelectedTower, t);
            }

            lastSelectedTower = null;
        }
        else
        {
            lastSelectedTower = t;
        }
    }


    public void MoveTowerToTower(Tower from, Tower to)
    {
        to.UnHighlightCircle();
        from.UnHighlightCircle();

        if (to.CanAdd(from.lastCircle))
        {
            Circle c = from.GetFromTower();
            to.AddInTower(c);

        }
    }

    public void MoveTowerToTower(int from, int to)
    {
        MoveTowerToTower(towers[from], towers[to]);

    }

    private void SolveHanoi(int amountOfDisks, int from, int to)
    {
        if(amountOfDisks == 1)
        {
            MoveTowerToTower(from, to);
        }
        else
        {
            int aux = 0;
            for (int i = 0; i < 3; i++)
            {
                if (i != from && i != to)
                {
                    aux = i;
                    break;
                }
            }

            SolveHanoi(amountOfDisks - 1, from, aux);
            MoveTowerToTower(from, to);
            SolveHanoi(amountOfDisks - 1, aux, to);

        }






    }
}
