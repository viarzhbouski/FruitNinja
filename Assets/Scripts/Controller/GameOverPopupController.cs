using System.Collections;
using UnityEngine;
using UnityEngine.Events;
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
    private Animation gameOverPopupAnimation;
    [SerializeField]
    private AnimationClip openClip;
    [SerializeField]
    private AnimationClip closeClip;
    [SerializeField]
    private Button gameOverPopupRestartButton;
    
    // Start is called before the first frame update
    void Start()
    {
        gameOverPopupRestartButton.onClick.AddListener(RestartGameOnClick);
        lifeCountController.GameOverEvent = new UnityEvent();
        lifeCountController.GameOverEvent.AddListener(GameOverEventHandler);
    }

    private void GameOverEventHandler()
    {
        gameOverPopup.SetActive(true);
        gameOverPopupAnimation.Play(openClip.name);
    }

    private void RestartGameOnClick() => StartCoroutine(RestartGame());

    IEnumerator RestartGame()
    {
        gameOverPopupAnimation.Play(closeClip.name);
        yield return new WaitForSeconds(0.3f);
        
        gameOverPopup.SetActive(false);
        SceneManager.LoadScene(0);
        lifeCountController.ResetLifeCount();
        scoreCountController.ResetScore();
    }
}
