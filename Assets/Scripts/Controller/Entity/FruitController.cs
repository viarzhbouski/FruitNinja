using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitController : EntityController
{
    private void Update()
    {
        UpdateEntity();
        
        if (EntityCanCut)
        {
            FruitCut();
        }
    }
    
    private void FruitCut()
    {
        var config = (FruitConfig)EntityConfig;
        
        EntityControllersProvider.ScoreCountController.AddScore(config.Score * EntityControllersProvider.ComboController.ComboMultiplier, transform.position);
        EntityControllersProvider.ComboController.FruitCutEvent.Invoke();
        SpawnSprayEffect(config);
        SpawnCutEffect(config);
        SpawnFruitFragments();
        EntityDestroy();
    }

    private void SpawnFruitFragments()
    {
        var offsetY = spriteRenderer.sprite.texture.height / 2;
        var startY = 0;
        var pivot = new Vector2(0.5f, 0.75f);
        var fragmentConfig = GameConfig.FruitFragment;

        for (var i = 0; i < 2; i++)
        {
            var texture = spriteRenderer.sprite.texture;
            var rect = new Rect(0, startY, texture.width, offsetY);
            var fragmentSprite = Sprite.Create(texture, rect, pivot);
            
            var x = Random.Range(-fragmentConfig.Speed, fragmentConfig.Speed);
            var y = Random.Range(-fragmentConfig.Speed, fragmentConfig.Speed);
            
            var directionVector = new Vector3(x, y, 0);
            EntityControllersProvider.EntitySpawnController.SpawnEntity(directionVector, transform.position, fragmentConfig, fragmentSprite);
            
            startY += offsetY;
            pivot.y -= 0.5f;
        }
    }

    private void SpawnCutEffect(FruitConfig config)
    {
        var cutEffect = GameConfig.CutEffect;
        var main = cutEffect.main;
        var trail = cutEffect.trails;
        
        main.startColor = config.FruitColor;
        trail.colorOverLifetime = config.FruitColor;
        trail.colorOverTrail = config.FruitColor;
        Instantiate(cutEffect.gameObject, transform.position, Quaternion.identity, transform.parent);
    }
    
    private void SpawnSprayEffect(FruitConfig config)
    {
        var sprayEffect = GameConfig.SprayEffect;
        var main = sprayEffect.main;
        
        main.startColor = config.FruitColor;
        
        var effect = Instantiate(sprayEffect.gameObject, transform.position, Quaternion.identity, transform.parent);
        var position = effect.transform.position;
        var zIndex = position.z + 2.0f;
        position = new Vector3(position.x, position.y, Random.Range(zIndex, zIndex + 3.0f));
        effect.transform.position = position;
    }
}
