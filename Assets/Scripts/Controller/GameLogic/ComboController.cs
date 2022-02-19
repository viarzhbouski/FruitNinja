using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ComboController : MonoBehaviour
{
    [SerializeField]
    private GameConfigController gameConfigController;
    [SerializeField]
    private Text comboUI;

    private UnityEvent _fruitCutEvent = new UnityEvent();
    private int _comboMultiplier;
    private int _combo;
    private float _currentTime;
    private float _currentMultiplierTime;

    private GameConfig GameConfig => gameConfigController.GameConfig;
    
    public UnityEvent FruitCutEvent
    {
        get { return _fruitCutEvent; }
    }
    
    public int ComboMultiplier
    {
        get { return _comboMultiplier; }
    }

    private void Start()
    {
        _comboMultiplier = 1;
        _combo = 1;
        _currentTime = 0;
        _currentMultiplierTime = 0;
        _fruitCutEvent.AddListener(FruitOnCut);
    }

    private void Update()
    {
        if (_currentMultiplierTime > 0) 
        {
            _currentMultiplierTime -= Time.deltaTime;
        }
        else if (_currentMultiplierTime <= 0 && _comboMultiplier > 1)
        {
            _comboMultiplier = 1;
            comboUI.rectTransform.DOScale(Vector3.zero, GameConfig.ComboScaleSpeed);
        }
        
        if (_currentTime > 0) 
        {
            _currentTime -= Time.deltaTime;
        }
        else if (_currentTime <= 0 && _combo > 1) 
        {
            _combo = 1;
        }
    }

    private void FruitOnCut()
    {
        if (_comboMultiplier == 1)
        {
            comboUI.text = string.Empty;
        }
        
        _currentTime = GameConfig.ComboTime;
        
        if (_combo == GameConfig.ComboMax)
        {
            _currentMultiplierTime = GameConfig.ComboMultiplierTime;
            _combo = 1;
            
            if (_comboMultiplier < GameConfig.ComboMultiplierMax)
            {
                _comboMultiplier++;
            }

            if (comboUI.text == string.Empty)
            {
                comboUI.rectTransform.DOScale(Vector3.one, GameConfig.ComboScaleSpeed);
            }
            else if (comboUI.rectTransform.localScale.x == Vector3.one.x)
            {
                comboUI.rectTransform.DOPunchScale(GameConfig.ComboPunchScale, GameConfig.ComboPunchScaleSpeed);
            }
            
            comboUI.text = $"x{_comboMultiplier}";
        }
        else
        {
            _combo++;
        }
    }
}
