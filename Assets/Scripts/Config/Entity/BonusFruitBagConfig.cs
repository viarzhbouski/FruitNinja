using UnityEngine;

[CreateAssetMenu(fileName = "New BonusFruitBag", menuName = "Bonus Fruit Bag", order = 57)]
public class BonusFruitBagConfig : EntityConfig
{
    [SerializeField]
    private float fruitSpeed;
    
    [SerializeField]
    private int fruitCountInBag;
    
    [SerializeField]
    private float fruitSwipeDelay;
    
    public float FruitSpeed
    {
        get { return fruitSpeed; }
    }
    
    public int FruitCountInBag
    {
        get { return fruitCountInBag; }
    }
    
    public float FruitSwipeDelay
    {
        get { return fruitSwipeDelay; }
    }
}