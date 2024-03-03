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

    protected int moneyTimer = 0;

    protected int health = 100;
    protected int damagePreSecond = 0;

    protected bool isCorruption = false;
    protected SpriteRenderer spriteRn;

    public int Price { get { return price; } }
    public bool IsCorruption { get { return isCorruption; } }

    protected virtual void Start()
    {
        spriteRn = GetComponent<SpriteRenderer>();
        StartCoroutine(Grow());
    }

    protected IEnumerator Grow()
    {
        for (int i = 0; i < growDuration; i++) 
        {
            if (isCorruption)
            {
                health -= damagePreSecond;
            }
            if (health <= 0)
                Destroy(gameObject);
            spriteRn.sprite = sprites[(i * sprites.Count / growDuration)];

            yield return new WaitForSeconds(1) ;
        }
        yield return StartAbility();
    }
    public abstract IEnumerator StartAbility();
}
