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

    protected int moneyTimer = 0;

    protected int corruptionCount = 0;

    protected bool isCorruption = false;
    protected SpriteRenderer spriteRn;

    protected int row;
    protected int col;

    public int Price { get { return price; } }
    public bool IsCorruption { get { return isCorruption; } set { isCorruption = value; } }

    public int CorruptionCount { get { return corruptionCount; } set {  corruptionCount = value; } }
    public int Health { get { return health; } }
    public int Row {  get { return row; } set { row = value; } }
    public int Col {  get { return col; } set { col = value; } }

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
}
