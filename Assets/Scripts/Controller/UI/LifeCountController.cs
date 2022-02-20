using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.UI;

public class LifeCountController : MonoBehaviour
{
    [SerializeField]
    private LifeController lifeImagePrefab;
    [SerializeField]
    private RectTransform lifeGrid;
    [SerializeField]
    private GridLayoutGroup gridLayoutGroup;
    [SerializeField]
    private GameConfigController gameConfigController;
    
    private Stack<LifeController> _lifes = new Stack<LifeController>();
    private UnityEvent _gameOverEvent = new UnityEvent();
    private int _currentLifeCount;
    private bool _gameOver;
    
    private const float DefaultGridCellSize = 100f;
    private const int DefaultLifeCountStep = 10;
    
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
    
    private void Start()
    {
        _currentLifeCount = gameConfigController.GameConfig.StartLifeCount;
        Init();
    }
    
    private void Init()
    {
        ResizeLifeGrid();
        for (int i = 0; i < _currentLifeCount; i++)
        {
            var life = Instantiate(lifeImagePrefab, lifeGrid);
            life.PlayInitAnimation();
            _lifes.Push(life);
        } 
    }
    
    public void ResizeLifeGrid()
    {
        if (_currentLifeCount > DefaultLifeCountStep)
        {
            gridLayoutGroup.constraintCount = (int)Mathf.Sqrt(_currentLifeCount) + DefaultLifeCountStep;
            var size = DefaultGridCellSize / gridLayoutGroup.constraintCount * DefaultLifeCountStep;
            if (size <= DefaultGridCellSize)
            {
                gridLayoutGroup.cellSize = new Vector2(size, size);
            }
        }
        else
        {
            gridLayoutGroup.cellSize = new Vector2(DefaultGridCellSize, DefaultGridCellSize);
            gridLayoutGroup.constraintCount = _currentLifeCount;
        }
    }

    public void DecreaseLife()
    {
        if (_lifes.Count == 0)
        {
            return;
        }
        
        var life = _lifes.Pop();
        life.PlayDestroyAnimation(this);
        
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
        ResizeLifeGrid();
    }

    public void ResetLifeCount()
    {
        _gameOver = false;
        _currentLifeCount = gameConfigController.GameConfig.StartLifeCount;
        Init();
    }
}
