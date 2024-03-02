using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridManager
{
    static List<List<GameObject>> grid;

    public static List<List<GameObject>> Grid { get { return grid; } }

    public static void Initialize()
    {
        grid = new List<List<GameObject>>();
        var G = GameObject.Find("Grid");
        for (int i = 0; i < G.transform.childCount; i++) 
        {
            List<GameObject> row = new List<GameObject>();
            foreach (Transform ch in G.transform.GetChild(i)) 
            {
                row.Add(ch.gameObject);
            }
            grid.Add(row);
        }
    }
}
