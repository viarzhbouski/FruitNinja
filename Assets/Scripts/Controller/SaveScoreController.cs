using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class SaveScoreController : MonoBehaviour
{
    public void SaveBestScore(TextMeshProUGUI scoreUI)
    {
        var scoreModel = new ScoreModel();
        scoreModel.Score = scoreUI.text;
        string json = JsonConvert.SerializeObject(scoreModel);
        File.WriteAllText("Assets/Save/score.json", json);
    }
    
    public void LoadBestScore(TextMeshProUGUI scoreUI)
    {
        var json = File.ReadAllText("Assets/Save/score.json");
        var scoreModel = JsonConvert.DeserializeObject<ScoreModel>(json);
        scoreUI.text = scoreModel.Score;
    }
}
