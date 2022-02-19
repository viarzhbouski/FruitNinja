using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverPopupController : MonoBehaviour
{
    [SerializeField]
    private GameConfigController gameConfigController;
    [SerializeField]
    private LifeCountController lifeCountController;
    [SerializeField]
    private ScoreCountController scoreCountController;
    [SerializeField]
    private EntityRepositoryController entityRepositoryController;
    [SerializeField]
    private RectTransform gameOverPopup;
    [SerializeField]
    private Text yourScoreUI;
    [SerializeField]
    private Button gameOverPopupRestartButton;
    [SerializeField]
    private Button gameOverPopupMainMenuButton;
    [SerializeField]
    private Image imageForAnim;
    
    private bool _popupIsClosed;
    
    private GameConfig GameConfig => gameConfigController.GameConfig;
    
    private void Start()
    {
        imageForAnim.color = Color.black;
        imageForAnim.DOFade(0f, 0.5f);
        gameOverPopupRestartButton.onClick.AddListener(RestartGameOnClick);
        gameOverPopupMainMenuButton.onClick.AddListener(MainMenuOnClick);
        _popupIsClosed = true;
    }

    private void Update()
    {
        if (lifeCountController.GameOver && entityRepositoryController.Entities.Count == 0 && _popupIsClosed)
        {
            OpenGameOverPopup();
        }
    }

    private void OpenGameOverPopup()
    {
        _popupIsClosed = false;
        gameOverPopup.DOScale(Vector3.one / 2f, GameConfig.PopupOpenCloseSpeed);
        yourScoreUI.DOText(scoreCountController.ScoreUI.text, GameConfig.ScoreCountSpeed, false, ScrambleMode.Numerals);
    }

    private void RestartGameOnClick()
    {
        gameOverPopupRestartButton.enabled = false;
        gameOverPopupMainMenuButton.enabled = false;
        
        gameOverPopup.transform.DOScale(Vector3.zero, GameConfig.PopupOpenCloseSpeed);
        lifeCountController.ResetLifeCount();
        scoreCountController.ResetScore();
        _popupIsClosed = true;
    }
    
    private void MainMenuOnClick()
    {
        gameOverPopupRestartButton.enabled = false;
        gameOverPopupMainMenuButton.enabled = false;
        StartCoroutine(MainMenu());
    }

    IEnumerator MainMenu()
    {
        gameOverPopup.transform.DOScale(Vector3.zero, GameConfig.PopupOpenCloseSpeed);
        imageForAnim.DOFade(1f, 0.5f);
        yield return new WaitForSeconds(GameConfig.LoadMainMenuSceneDelay);
        SceneManager.LoadScene(0);
    }
}
