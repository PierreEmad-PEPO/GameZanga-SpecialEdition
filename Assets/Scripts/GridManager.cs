using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridManager
{
    static List<List<GameObject>> grid;
    static List<List<bool>> vis;
    static List<GameObject> eneymies;
    static List<GameObject> Bigeneymies;
    public static List<List<GameObject>> Grid { get { return grid; } }
    public static List<List<bool>> Vis { get { return vis; } }

    public static List<GameObject> Enemies {  get { return eneymies; } }
    public static List<GameObject> BigEnemies { get { return eneymies; } }

    public static void Initialize()
    {
        grid = new List<List<GameObject>>();
        vis = new List<List<bool>>();
        var G = GameObject.Find("Grid");
        for (int i = 0; i < G.transform.childCount; i++) 
        {
            List<GameObject> row = new List<GameObject>();
            List<bool> boolRow = new List<bool>();
            int l = G.transform.GetChild(i).childCount;
            for (int j = 0; j < l; j++) 
            {
                var ch = G.transform.GetChild(i).GetChild(j);
                row.Add(ch.gameObject);
                boolRow.Add(false);
            }
            vis.Add(boolRow);
            grid.Add(row);
        }
    }
}
