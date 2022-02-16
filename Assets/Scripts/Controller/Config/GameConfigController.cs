using UnityEngine;

public class GameConfigController : MonoBehaviour
{
    [SerializeField]
    private GameConfig gameConfig;
    
    public GameConfig GameConfig
    {
        get { return gameConfig; }
    }
}
