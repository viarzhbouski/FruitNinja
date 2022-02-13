using UnityEngine;

[CreateAssetMenu(fileName = "New Bomb", menuName = "Bomb", order = 54)]
public class BombConfig : ScriptableObject
{
    [SerializeField]
    private BombController bombController;

    [SerializeField]
    private float bombSpeed;
    
    [SerializeField]
    private float bombRotateSpeed;
    
    [SerializeField]
    [Range(0, 1)]
    private float chance;
    
    [SerializeField]
    private ParticleSystem explodeEffect;
    
    public BombController BombController
    {
        get { return bombController; }
    }
    
    public float BombSpeed
    {
        get { return bombSpeed; }
    }
    
    public float BombRotateSpeed
    {
        get { return bombRotateSpeed; }
    }
    
    public float Chance
    {
        get { return chance; }
    }
    
    public ParticleSystem ExplodeEffect
    {
        get { return explodeEffect; }
    }
}