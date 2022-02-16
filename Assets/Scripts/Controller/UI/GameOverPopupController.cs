using System;
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
    private EntityRepositoryController entityRepositoryController;
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
    
    private void Start()
    {
        gameOverPopupRestartButton.onClick.AddListener(RestartGameOnClick);
        gameOverPopupMainMenuButton.onClick.AddListener(MainMenuOnClick);
    }

    private void Update()
    {
        if (lifeCountController.GameOver && entityRepositoryController.Entities.Count == 0 && !gameOverPopup.activeSelf)
        {
            OpenGameOverPopup();
        }
    }

    private void OpenGameOverPopup()
    {
        gameOverPopup.SetActive(true);
        gameOverPopupAnimation.Play(openClip.name);
        yourScoreUI.text = scoreCountController.ScoreUI.text;
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
}
