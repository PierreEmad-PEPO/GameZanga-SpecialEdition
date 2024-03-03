using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BookUI : MonoBehaviour
{
    [SerializeField] List<GameObject> Plants;

    VisualElement root;

    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
                
    }

    void SetVisualElement()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
    }

}
