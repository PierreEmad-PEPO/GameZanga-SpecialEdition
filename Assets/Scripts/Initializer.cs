using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    Vector2 startPoint;

    void Awake()
    {
        GridManager.Initialize();
        InputManager.Initialize(GridManager.Grid[0][0].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
