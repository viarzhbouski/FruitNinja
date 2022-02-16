using UnityEngine;

public class EntityOnGameFieldCheckerController : MonoBehaviour
{
    [SerializeField]
    private GameConfigController gameConfigManager;
    [SerializeField]
    private Camera camera;

    public GameConfigController GameConfigManager
    {
        get { return gameConfigManager; }
    }
    
    public bool EntityOnGameField(float x, float y)
    {
        var viewportPosition = camera.WorldToViewportPoint(new Vector2(x, y));
        
        if (viewportPosition.x >= gameConfigManager.GameConfig.XMinBorder && 
            viewportPosition.x <= gameConfigManager.GameConfig.XMaxBorder && 
            viewportPosition.y >= gameConfigManager.GameConfig.YMinBorder)
        {
            return true;
        }

        return false;
    }
}
