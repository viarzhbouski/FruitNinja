using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LifeCountController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lifeUI;
    [SerializeField]
    private GameConfigController gameConfigController;

    private UnityEvent gameOverEvent;
    
    private int currentLifeCount;
    private bool gameOver;

    public bool GameOver
    {
        get { return gameOver; }
    }
    
    public UnityEvent GameOverEvent
    {
        get { return gameOverEvent; }
        set { gameOverEvent = value; }
    }
    
    void Start()
    {
        currentLifeCount = gameConfigController.GameConfig.LifeCount;
        lifeUI.text = currentLifeCount.ToString();
    }
    
    public void DecreaseLife()
    {
        currentLifeCount = int.Parse(lifeUI.text) - 1;
        lifeUI.text = currentLifeCount.ToString();
        gameOver = currentLifeCount == 0;
        
        if (gameOver)
        {
            GameOverEvent.Invoke();
        }
    }

    public void ResetLifeCount()
    {
        gameOver = false;
        currentLifeCount = gameConfigController.GameConfig.LifeCount;
    }
}
