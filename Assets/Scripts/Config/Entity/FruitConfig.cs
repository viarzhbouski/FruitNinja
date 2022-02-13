using UnityEngine;

[CreateAssetMenu(fileName = "New Fruit", menuName = "Fruit", order = 53)]
public class FruitConfig : EntityConfig
{
    [SerializeField]
    private int score;
    
    [SerializeField]
    private Color fruitColor;

    public int Score
    {
        get { return score; }
    }
    
    public Color FruitColor
    {
        get { return fruitColor; }
    }
}