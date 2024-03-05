using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlant : MonoBehaviour
{
    [SerializeField]
    protected int price;
    [SerializeField]
    protected int growDuration;
    [SerializeField]
    protected int corruptionDecrease;
    [SerializeField]
    protected int money;
    [SerializeField]
    protected int moneyDuration;
    [SerializeField]
    protected List<Sprite> sprites;
    [SerializeField]
    protected int health = 100;
    [SerializeField]
    protected int damagePreSecond = 10;
    [SerializeField, TextArea]
    protected string name;
    [SerializeField, TextArea]
    protected string description;

    protected int moneyTimer = 0;

    protected int corruptionCount = 0;

    protected bool isCorruption = false;
    protected SpriteRenderer spriteRn;

    protected int row;
    protected int col;


    public int Price { get { return price; } }
    public string Name { get { return name; } }
    public bool IsCorruption { get { return isCorruption; } set { isCorruption = value; } }

    public int CorruptionCount { get { return corruptionCount; } set {  corruptionCount = value; } }
    public int Health { get { return health; } }
    public int Row {  get { return row; } set { row = value; } }
    public int Col {  get { return col; } set { col = value; } }

    public string Description { get { return description; } }

    protected virtual void Start()
    {
        spriteRn = GetComponent<SpriteRenderer>();
        StartCoroutine(Grow());
    }

    protected IEnumerator Grow()
    {
        for (int i = 0; i < growDuration; i++) 
        {
            health -= (damagePreSecond * corruptionCount);
            if (health <= 0)
                DestroyPlant();
            spriteRn.sprite = sprites[(i * sprites.Count / growDuration)];

            yield return new WaitForSeconds(1) ;
        }
        yield return StartAbility();
    }
    public abstract IEnumerator StartAbility();

    public void DestroyPlant()
    {
        GridManager.Vis[row][col] = false;
        Destroy(gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision.GetComponent<Enemy>().TargetPlant == gameObject)
            corruptionCount++;
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag("Enemy") && other.GetComponent<Enemy>().TargetPlant == gameObject)
        {
            corruptionCount--;
            if (corruptionCount < 0) corruptionCount = 0;
            Debug.Log("55555555555");
        }
    }
}
