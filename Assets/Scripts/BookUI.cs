using ArabicSupport;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BookUI : MonoBehaviour
{
    [SerializeField] List<GameObject> plants;

    VisualElement root;
    VisualElement img;
    Label name;
    Label price;
    Label description;
    int index = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        SetVisualElement();
    }

    void SetVisualElement()
    {
        VisualElement data = root.Q<VisualElement>("Data");
        root.Q<Button>("Book").clicked += () => {
            if (data.style.display == DisplayStyle.None ) 
            {
                data.style.display = DisplayStyle.Flex;
                SetData();
            }
            else
                data.style.display = DisplayStyle.None;

        };
        img = root.Q<VisualElement>("Img");
        name = root.Q<Label>("Name");
        description = root.Q<Label>("Description"); 
        price = root.Q<Label>("Price");
        root.Q<Button>("Prev").clicked += () => {
            if (index > 0)
            {
                index--;
                SetData();
            }

        };
        root.Q<Button>("Next").clicked += () => {
            if (index < plants.Count -1)
            {
                index++;
                SetData();
            }
        };
        data.style.display = DisplayStyle.None;
    }

    void SetData()
    {
        BasePlant p = plants[index].GetComponent<BasePlant>();
        img.style.backgroundImage = new StyleBackground(plants[index].GetComponent<SpriteRenderer>().sprite);
        name.text =  ArabicFixer.Fix( p.Name);
        price.text =  ArabicFixer.Fix("السعر" + " : " + p.Price.ToString() );
        description.text = ArabicFixer.Fix(p.Description);

    }
}
