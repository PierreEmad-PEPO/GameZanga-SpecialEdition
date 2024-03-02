using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Vector3 startPos;
    Vector3 endPos;
    Cell tagetCell;

    // Update is called once per frame
    void Update()
    {

    }

    public void startAttack(Vector3 stapos)
    {
        this.startPos = stapos;
        GetRandomCell();
    }

    void GetRandomCell()
    {
        List<GameObject> cells = new List<GameObject>();
        foreach (var list in GridManager.Grid)
        {
            foreach (var cell in list)
            {
                if (cell.GetComponent<Cell>().IsPlanting)
                    cells.Add(cell);
            }
        }

        GameObject tagetObject = cells[Random.Range(0, cells.Count)];
        endPos = tagetObject.transform.position;
        tagetCell = tagetObject.GetComponent<Cell>();

    }


}
