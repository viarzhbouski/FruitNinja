using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SaveScoreController : MonoBehaviour
{
    public void SaveBestScore(Text scoreUI) => PlayerPrefs.SetString("Score", scoreUI.text);
    
    public void LoadBestScore(Text scoreUI)
    {
        var score = PlayerPrefs.GetString("Score");
        
        if (!string.IsNullOrEmpty(score))
        {
            scoreUI.DOText(score, 0.2f, false, ScrambleMode.Numerals);
        }
        else
        {
            scoreUI.text = "0";
        }
    }
}
