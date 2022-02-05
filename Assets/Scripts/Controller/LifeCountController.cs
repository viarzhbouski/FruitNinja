using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeCountController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lifeUI;
    [SerializeField]
    private GameObject gameOverPopup;
    [SerializeField]
    private GameConfigController gameConfigController;
    [SerializeField]
    private ScoreCountController scoreCountController;
    [SerializeField]
    private Button gameOverPopupRestartButton;
    
    private int currentLifeCount;
    private bool gameOver;

    public bool GameOver
    {
        get { return gameOver; }
    }
    
    void Start()
    {
        currentLifeCount = gameConfigController.GameConfig.LifeCount;
        lifeUI.text = currentLifeCount.ToString();
        gameOverPopupRestartButton.onClick.AddListener(RestartGameOnClick);
    }
    
    public void DecreaseLife()
    {
        currentLifeCount = int.Parse(lifeUI.text) - 1;
        lifeUI.text = currentLifeCount.ToString();
        gameOver = currentLifeCount == 0;
        
        if (gameOver)
        {
            gameOverPopup.SetActive(true);
        }
    }
    
    private void RestartGameOnClick()
    {
        SceneManager.LoadScene(0);
        gameOverPopup.SetActive(false);
        gameOver = false;
        currentLifeCount = gameConfigController.GameConfig.LifeCount;
        scoreCountController.ResetScore();
    }
}
