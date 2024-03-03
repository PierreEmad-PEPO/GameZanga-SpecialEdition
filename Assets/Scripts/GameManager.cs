using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] List<Sprite> stages = new List<Sprite>();
    [SerializeField] SpriteRenderer stageRenderer;

    private static int money = 15;
    private static int totalTime = 600;
    private static int elapsedTime = 0;
    private static int corruption = 100;
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
    [SerializeField] GameObject jukaPlant;
    
    Dictionary<PlantEnum, GameObject> plantsDic = new Dictionary<PlantEnum, GameObject>();

    [SerializeField] UIDocument gameplayUI;
    VisualElement root;
    Label timerLabel;
    Label moneyLabel;
    ProgressBar progressBar;
    Button none;
    Button albuca;
    Button drosera;
    Button elixir;
    Button fire;
    Button golden;
    Button juka;
    Button cur;


    public static GameManager Instance {  get 
    { 
            if (instance != null) return instance;
            instance = GameObject.FindObjectOfType<GameManager>();
            return instance; 
    } }
    public static int Money {  get { return money; } set { money = value; } }
    public static int Corruption { get {  return corruption; } set {  corruption = value; } }
    public static int TotalTime { get { return totalTime; } set { totalTime = value; elapsedTime = 0; } }

    private void Start()
    {
        plantEnum = PlantEnum.None;

        plantsDic.Add(PlantEnum.FirePlant, firePlant);
        plantsDic.Add(PlantEnum.AlbucaPlant, albucaPlant);
        plantsDic.Add(PlantEnum.DroseraPlant, droseraPlant);
        plantsDic.Add(PlantEnum.ElixirPlant, elixirPlant);
        plantsDic.Add(PlantEnum.TheGoldenPlant, theGoldenPlant);
        plantsDic.Add(PlantEnum.JukaPlant, jukaPlant);

        root = gameplayUI.rootVisualElement;
        timerLabel = root.Q<Label>("TimerLabel");
        moneyLabel = root.Q<Label>("MoneyLabel");
        progressBar = root.Q<ProgressBar>("Corruption");
        none = root.Q<Button>("None");
        cur = none;
        none.clicked += () => { ReadyNone(none); };
        albuca = root.Q<Button>("Albuca");
        albuca.clicked += () => { ReadyAlbucaPlant(); ButtonPressStyle(albuca); };
        drosera = root.Q<Button>("Drosera");
        drosera.clicked += () => { ReadyDroseraPlant(); ButtonPressStyle(drosera); };
        elixir = root.Q<Button>("Elixir");
        elixir.clicked += () => { ReadyElixirPlant(); ButtonPressStyle(elixir); };
        fire = root.Q<Button>("Fire");
        fire.clicked += () => { ReadyFirePlant(); ButtonPressStyle(fire); };
        golden = root.Q<Button>("Golden");
        golden.clicked += () => { ReadyTheGoldenPlant(); ButtonPressStyle(golden); };
        juka = root.Q<Button>("Juka");
        juka.clicked += () => { ReadyJukaPlant(); ButtonPressStyle(juka); };

        StartCoroutine(UpdateUI());
    }

    void ButtonPressStyle(Button nw)
    {
        cur.RemoveFromClassList("button-pressed");
        cur = nw;
        cur.AddToClassList("button-pressed");
    }

    IEnumerator UpdateUI()
    {
        while (true) 
        {
            int sec = totalTime - elapsedTime;
            int mins = sec / 60;
            sec = sec % 60;
            timerLabel.text = mins.ToString("00") + ":" + sec.ToString("00");
            moneyLabel.text = money.ToString();
            float low = progressBar.lowValue;
            progressBar.value = corruption + low;

            int idx = (int)(corruption * 5f / 99f);
            if (idx > 4) idx = 4;
            if (idx < 0) idx = 0;
            stageRenderer.sprite = stages[idx];

            if (elapsedTime >= totalTime) 
            {
                Debug.Log("Game Over");
                SceneManager.LoadScene("LoseScene");
            }
            else if (corruption <= 0)
            {
                Debug.Log("You Win");
                SceneManager.LoadScene("WinScene");
            }



            yield return new WaitForSeconds(1);
            elapsedTime++;
        }
    }

    public void ReadyNone(Button none)
    {
        plantEnum = PlantEnum.None;
        ButtonPressStyle(none);
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
    public static void ReadyJukaPlant()
    {
        plantEnum = PlantEnum.JukaPlant;
    }

    public void TryBuyPlant()
    {
        if (plantEnum == PlantEnum.None) return;

        BasePlant plant = plantsDic[plantEnum].GetComponent<BasePlant>();
        var T = InputManager.MouseToGrid();
        if (T.Item1>=0 && T.Item1<GridManager.Grid.Count && T.Item2>=0 && T.Item2< GridManager.Grid[0].Count)
        {
            if (money >= plant.Price && !GridManager.Vis[T.Item1][T.Item2]) 
            {
                var p = GridManager.Grid[T.Item1][T.Item2].transform;

                GameObject g = Instantiate(plantsDic[plantEnum], p);
                g.transform.parent = p;
                g.GetComponent<BasePlant>().Row = T.Item1;
                g.GetComponent<BasePlant>().Col = T.Item2;
                GridManager.Vis[T.Item1][T.Item2] = true;
                money -= plant.Price;
                ReadyNone(none);
            }
        }
    }

}
