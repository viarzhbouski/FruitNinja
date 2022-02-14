using UnityEngine;

[CreateAssetMenu(fileName = "New Bonus Freeze", menuName = "Bonus Freeze", order = 56)]
public class BonusFreezeConfig : EntityConfig
{
    [SerializeField]
    private float freezeForce;
    [SerializeField]
    private float freezeTime;
    
    public float FreezeForce
    {
        get { return freezeForce; }
    }
    
    public float FreezeTime
    {
        get { return freezeTime; }
    }
}