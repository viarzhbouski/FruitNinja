using UnityEngine;

[CreateAssetMenu(fileName = "New Bomb", menuName = "Bomb", order = 54)]
public class BombConfig : EntityConfig
{
    [SerializeField]
    private int explodeForce;
    [SerializeField]
    [Range(0, 1)]
    private float chance;

    public int ExplodeForce
    {
        get { return explodeForce; }
    }
    
    public float Chance
    {
        get { return chance; }
    }
}