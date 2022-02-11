using UnityEngine;

public class EntityOnGameFieldChecker : MonoBehaviour
{
    [SerializeField]
    private GameConfigController gameConfigManager;

    public GameConfigController GameConfigManager
    {
        get { return gameConfigManager; }
    }
    
    public bool EntityOnGameField(float x, float y)
    {
        // if (x >= gameConfigManager.GameConfig.XMinBorder && x <= gameConfigManager.GameConfig.XMaxBorder &&
        //     y >= gameConfigManager.GameConfig.YMinBorder && y <= gameConfigManager.GameConfig.YMaxBorder)
        // {
        //     return true;
        // }

        //return true;
        
        Debug.Log($"X={x}; Y={y}");
        
        if (x >= 0 && x <= 1 && 
            y >= 0 && y <= 1)
        {
             return true;
        }

        return false;
    }
}
