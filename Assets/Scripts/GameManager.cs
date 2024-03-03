using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private static int money = 5000;
    private static int corruption = 5000;
    public static bool hasRedSeed;
    public static bool hasGreenSeed;
    public static bool hasBlueSeed;
    public static bool hasYellowSeed;

    private static PlantEnum plantEnum = PlantEnum.None;

    [SerializeField] GameObject firePlant;
    [SerializeField] GameObject albucaPlant;
    [SerializeField] GameObject droseraPlant;
    [SerializeField] GameObject elixirPlant;
    [SerializeField] GameObject theGoldenPlant;
    
    Dictionary<PlantEnum, GameObject> plantsDic = new Dictionary<PlantEnum, GameObject>();


    public static GameManager Instance {  get 
    { 
            if (instance != null) return instance;
            instance = GameObject.FindObjectOfType<GameManager>();
            return instance; 
    } }
    public static int Money {  get { return money; } set { money = value; } }
    public static int Corruption { get {  return corruption; } set {  corruption = value; } }

    private void Start()
    {
        plantsDic.Add(PlantEnum.FirePlant, firePlant);
        plantsDic.Add(PlantEnum.AlbucaPlant, albucaPlant);
        plantsDic.Add(PlantEnum.DroseraPlant, droseraPlant);
        plantsDic.Add(PlantEnum.ElixirPlant, elixirPlant);
        plantsDic.Add(PlantEnum.TheGoldenPlant, theGoldenPlant);
    }

    public static void ReadyFirePlant()
    {
        plantEnum = PlantEnum.FirePlant;
    }
    public static void ReadyAlbucaPlant()
    {
        plantEnum = PlantEnum.AlbucaPlant;
    }
    public static void ReadyDroseraPlant()
    {
        plantEnum = PlantEnum.DroseraPlant;
    }
    public static void ReadyElixirPlant()
    {
        plantEnum = PlantEnum.ElixirPlant;
    }
    public static void ReadyTheGoldenPlant()
    {
        plantEnum = PlantEnum.TheGoldenPlant;
    }

    public void TryBuyPlant()
    {
        BasePlant plant = plantsDic[plantEnum].GetComponent<BasePlant>();
        var T = InputManager.MouseToGrid();
        if (money >= plant.Price && !GridManager.Vis[T.Item1][T.Item2]) 
        {
            var p = GridManager.Grid[T.Item1][T.Item2].transform;
            Instantiate(plantsDic[plantEnum], p).transform.parent = p;
            GridManager.Vis[T.Item1][T.Item2] = true;
        }
    }

}
