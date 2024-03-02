using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Tuple<int, int> pos = InputManager.MouseToGrid();
            GridManager.Grid[pos.Item1][pos.Item2].GetComponent<Cell>().click(PlantEnum.gazr);
            GridManager.Grid[pos.Item1][pos.Item2].GetComponent<Cell>().IsCorruption = true;
        }
    }
}