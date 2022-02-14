using TMPro;
using UnityEngine;

public class SaveScoreController : MonoBehaviour
{
    public void SaveBestScore(TextMeshProUGUI scoreUI) => PlayerPrefs.SetString("Score", scoreUI.text);
    
    public void LoadBestScore(TextMeshProUGUI scoreUI)
    {
        var score = PlayerPrefs.GetString("Score");
        scoreUI.text = !string.IsNullOrEmpty(score) ? score : "0";
    }
}
