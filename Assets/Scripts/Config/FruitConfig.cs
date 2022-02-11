using UnityEngine;

[CreateAssetMenu(fileName = "New Fruit", menuName = "Fruit", order = 53)]
public class FruitConfig : ScriptableObject
{
    [SerializeField]
    private FruitController fruitController;
    
    [SerializeField]
    private Sprite fruitSprite;
    
    [SerializeField]
    private float fruitSpeed;
    
    [SerializeField]
    private float fruitRotateSpeed;
    
    [SerializeField]
    private float fragmentSpeed;

    [SerializeField]
    private float fragmentRotateSpeed;

    [SerializeField]
    private Color cutEffectColor;
    
    public FruitController FruitController
    {
        get { return fruitController; }
    }
    
    public Sprite FruitSprite
    {
        get { return fruitSprite; }
    }
    
    public float FruitSpeed
    {
        get { return fruitSpeed; }
    }
    
    public float FruitRotateSpeed
    {
        get { return fruitRotateSpeed; }
    }

    public float FragmentSpeed
    {
        get { return fragmentSpeed; }
    }
    
    public float FragmentRotateSpeed
    {
        get { return fragmentRotateSpeed; }
    }
    
    public Color CutEffectColor
    {
        get { return cutEffectColor; }
    }
}