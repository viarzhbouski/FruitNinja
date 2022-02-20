using UnityEngine;

public class GameTimeController : MonoBehaviour
{
    private float _freezeTime;
    private const float DefaultTime = 1f;
    
    private void Start()
    {
        _freezeTime = DefaultTime;
    }

    private void Update()
    {
        if (_freezeTime > DefaultTime)
        {
            Time.timeScale = DefaultTime / _freezeTime;
            _freezeTime -= DefaultTime * Time.unscaledDeltaTime;
        }
        else
        {
            Time.timeScale = DefaultTime;
            _freezeTime = DefaultTime;
        }
    }

    public void FreezeTime(float freezeForce, float freezeTime)
    {
        _freezeTime = freezeTime;
        Time.timeScale = freezeForce;
    }
}
