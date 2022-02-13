using UnityEngine;

[CreateAssetMenu(fileName = "New Bomb", menuName = "Bomb", order = 54)]
public class BombConfig : EntityConfig
{
    [SerializeField]
    [Range(0, 1)]
    private float chance;

    public float Chance
    {
        get { return chance; }
    }
}