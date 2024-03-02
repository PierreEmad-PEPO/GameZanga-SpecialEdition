using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GridManager
{
    static List<List<GameObject>> grid;
    static Dictionary<PlantEnum, Sprite[,]> plantSprites = new Dictionary<PlantEnum, Sprite[,]>();
    static int dimensionLength = 2; // for Now
    static Sprite emptySprite;
    public static List<List<GameObject>> Grid { get { return grid; } }
    public static int DimensionLength { get {return dimensionLength;} }
    public static Sprite EmptySprite { get { return emptySprite; } }
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

        emptySprite = Resources.Load<Sprite>("emptySprite");

        foreach(string name in Enum.GetNames(typeof(PlantEnum)))
        {
            Sprite[,] sprites = new Sprite[2, 2];
            for (int i = 0;i < dimensionLength ;i++)
            {
                for (int j = 0; j < dimensionLength; j++)
                {
                    sprites[i,j] = Resources.Load<Sprite>( name + i.ToString() + j.ToString());
                }
            }
            PlantEnum plant;
            
            if (Enum.TryParse(name, out plant))
                plantSprites.Add(plant, sprites);
            else
                Debug.Log("Errrrrrrrrrrrrrrrrror");

        }
    }

    public static Sprite GetPlantSprite(PlantEnum plant, int i, int j)
    {
        return plantSprites[plant][i, j];
    }
}
