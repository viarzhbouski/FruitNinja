using UnityEngine;

[CreateAssetMenu(fileName = "New Bomb", menuName = "Bomb", order = 54)]
public class BombConfig : EntityConfig
{
    [SerializeField]
    private int explodeForce;
    [SerializeField]
    private float explodeRadius;
    
    public int ExplodeForce
    {
        get { return explodeForce; }
    }
    
    public float ExplodeRadius
    {
        get { return explodeRadius; }
    }
}