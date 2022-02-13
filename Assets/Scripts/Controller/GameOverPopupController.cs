using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPopupController : MonoBehaviour
{
    [SerializeField]
    private LifeCountController lifeCountController;
    [SerializeField]
    private ScoreCountController scoreCountController;
    [SerializeField]
    private GameObject gameOverPopup;
    [SerializeField]
    private TextMeshProUGUI yourScoreUI;
    [SerializeField]
    private Animation gameOverPopupAnimation;
    [SerializeField]
    private Animation loadSceneAnimation;
    [SerializeField]
    private AnimationClip openClip;
    [SerializeField]
    private AnimationClip closeClip;
    [SerializeField]
    private AnimationClip loadSceneClip;
    [SerializeField]
    private Button gameOverPopupRestartButton;
    [SerializeField]
    private Button gameOverPopupMainMenuButton;
    [SerializeField]
    private float openPopupDelayTime;
    [SerializeField]
    private float loadSceneDelayTime;
    [SerializeField]
    private Animation restartButtonAnimation;
    [SerializeField]
    private Animation mainMenuButtonAnimation;
    [SerializeField]
    private float showRestartButtonDelayTime;
    [SerializeField]
    private float showMainMenuButtonDelayTime;
    
    void Start()
    {
        gameOverPopupRestartButton.onClick.AddListener(RestartGameOnClick);
        gameOverPopupMainMenuButton.onClick.AddListener(MainMenuOnClick);
        lifeCountController.GameOverEvent.AddListener(GameOverEventHandler);
    }

    private void GameOverEventHandler()
    {
        gameOverPopup.SetActive(true);
        gameOverPopupAnimation.Play(openClip.name);
        yourScoreUI.text = scoreCountController.ScoreUI.text;
        StartCoroutine(ShowRestartButton());
        StartCoroutine(ShowMainMenuButton());
    }

    private void RestartGameOnClick()
    {
        gameOverPopupRestartButton.enabled = false;
        gameOverPopupMainMenuButton.enabled = false;
        StartCoroutine(RestartGame());
    }
    
    private void MainMenuOnClick()
    {
        gameOverPopupRestartButton.enabled = false;
        gameOverPopupMainMenuButton.enabled = false;
        StartCoroutine(MainMenu());
    }

    IEnumerator MainMenu()
    {
        gameOverPopupAnimation.Play(closeClip.name);
        loadSceneAnimation.Play(loadSceneClip.name);
        yield return new WaitForSeconds(loadSceneDelayTime);
        SceneManager.LoadScene(0);
    }
    
    IEnumerator RestartGame()
    {
        gameOverPopupAnimation.Play(closeClip.name);
        yield return new WaitForSeconds(openPopupDelayTime);
        gameOverPopup.SetActive(false);
        SceneManager.LoadScene(1);
        lifeCountController.ResetLifeCount();
        scoreCountController.ResetScore();
    }
    
    IEnumerator ShowRestartButton()
    {
        yield return new WaitForSeconds(showRestartButtonDelayTime);
        restartButtonAnimation.Play();
    }
    
    IEnumerator ShowMainMenuButton()
    {
        yield return new WaitForSeconds(showMainMenuButtonDelayTime);
        mainMenuButtonAnimation.Play();
    }
}
