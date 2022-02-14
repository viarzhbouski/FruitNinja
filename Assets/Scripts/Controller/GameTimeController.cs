using System;
using UnityEngine;

public class GameTimeController : MonoBehaviour
{
    private float _freezeTime;

    private void Start()
    {
        _freezeTime = 1f;
    }

    private void Update()
    {
        Time.timeScale += (1f / _freezeTime) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void FreezeTime(float freezeForce, float freezeTime)
    {
        _freezeTime = freezeTime;
        Time.timeScale = freezeForce;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
