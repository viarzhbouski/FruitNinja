using UnityEngine.Events;
using UnityEngine;
using TMPro;

public class ComboController : MonoBehaviour
{
    [SerializeField]
    private GameConfigController gameConfigController;
    [SerializeField]
    private TextMeshProUGUI comboUI;
    [SerializeField]
    private Animation comboAnimation;
    [SerializeField]
    private AnimationClip showComboClip;
    [SerializeField]
    private AnimationClip increaseComboClip;
    [SerializeField]
    private AnimationClip destroyComboClip;
    
    private UnityEvent _fruitCutEvent = new UnityEvent();
    private int _comboMultiplier;
    private int _combo;
    private float _currentTime;
    private float _currentMultiplierTime;
    
    public UnityEvent FruitCutEvent
    {
        get { return _fruitCutEvent; }
    }
    
    public int ComboMultiplier
    {
        get { return _comboMultiplier; }
    }

    void Start()
    {
        _comboMultiplier = 1;
        _combo = 1;
        _currentTime = 0;
        _currentMultiplierTime = 0;
        _fruitCutEvent.AddListener(FruitOnCut);
    }

    void Update()
    {
        if (_currentMultiplierTime > 0) 
        {
            _currentMultiplierTime -= Time.deltaTime;
        }
        else if (_currentMultiplierTime <= 0 && _comboMultiplier > 1)
        {
            _comboMultiplier = 1;
            comboAnimation.Play(destroyComboClip.name);
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
        _currentTime = gameConfigController.GameConfig.ComboTime;
        
        if (_combo == gameConfigController.GameConfig.ComboMax)
        {
            _currentMultiplierTime = gameConfigController.GameConfig.ComboMultiplierTime;
            _combo = 1;
            _comboMultiplier++;

            if (comboUI.text == string.Empty)
            {
                comboAnimation.Play(showComboClip.name);
            }
            else
            {
                comboAnimation.Play(increaseComboClip.name);
            }
            
            comboUI.text = $"x{_comboMultiplier}";
        }
        else
        {
            _combo++;
        }
    }
}
