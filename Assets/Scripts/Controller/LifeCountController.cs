using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class LifeCountController : MonoBehaviour
{
    [SerializeField]
    private LifeController lifeImagePrefab;
    [SerializeField]
    private RectTransform lifeGrid;
    [SerializeField]
    private GameConfigController gameConfigController;

    private Stack<LifeController> lifes = new Stack<LifeController>();
    private UnityEvent gameOverEvent = new UnityEvent();
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
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < currentLifeCount; i++)
        {
            var life = Instantiate(lifeImagePrefab, lifeGrid);
            life.PlayInitAnimation();
            lifes.Push(life);
        } 
    }

    public void DecreaseLife()
    {
        var life = lifes.Pop();
        life.PlayDestroyAnimation();
        
        currentLifeCount--;
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
        Init();
    }
}
