using UnityEngine;

[CreateAssetMenu(fileName = "New BonusLife", menuName = "Bonus Life", order = 55)]
public class BonusLifeConfig : EntityConfig
{
    [SerializeField]
    [Range(0, 1)]
    private float chance;
    
    public float Chance
    {
        get { return chance; }
    }
}