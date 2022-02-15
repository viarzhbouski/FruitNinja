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

    private Stack<LifeController> _lifes = new Stack<LifeController>();
    private UnityEvent _gameOverEvent = new UnityEvent();
    private int _currentLifeCount;
    private bool _gameOver;
    
    public int CurrentLifeCount
    {
        get { return _currentLifeCount; }
    }
    
    public bool GameOver
    {
        get { return _gameOver; }
    }
    
    public UnityEvent GameOverEvent
    {
        get { return _gameOverEvent; }
        set { _gameOverEvent = value; }
    }
    
    void Start()
    {
        _currentLifeCount = gameConfigController.GameConfig.StartLifeCount;
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < _currentLifeCount; i++)
        {
            var life = Instantiate(lifeImagePrefab, lifeGrid);
            life.PlayInitAnimation();
            _lifes.Push(life);
        } 
    }

    public void DecreaseLife()
    {
        var life = _lifes.Pop();
        life.PlayDestroyAnimation();
        
        _currentLifeCount--;
        _gameOver = _currentLifeCount == 0;
        
        if (_gameOver)
        {
            GameOverEvent.Invoke();
        }
    }
    
    public void EncreaseLife()
    {
        if (_currentLifeCount == gameConfigController.GameConfig.MaxLifeCount)
        {
            return;
        }
        
        _currentLifeCount++;
        var life = Instantiate(lifeImagePrefab, lifeGrid);
        life.PlayInitAnimation();
        _lifes.Push(life);
    }

    public void ResetLifeCount()
    {
        _gameOver = false;
        _currentLifeCount = gameConfigController.GameConfig.StartLifeCount;
        Init();
    }
}
