using TMPro;
using UnityEngine;

public class ScoreCountController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreUI;
    
    public void AddScore()
    {
        var currentScore = int.Parse(scoreUI.text) + 1;
        scoreUI.text = currentScore.ToString();
    }
    
    public void ResetScore()
    {
        var currentScore = 0;
        scoreUI.text = currentScore.ToString();
    }
}
