using UnityEngine;

[CreateAssetMenu(fileName = "New Bomb", menuName = "Bomb", order = 54)]
public class BombConfig : EntityConfig
{
    [SerializeField]
    private int explodeForce;

    public int ExplodeForce
    {
        get { return explodeForce; }
    }
}