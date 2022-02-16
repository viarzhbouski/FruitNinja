using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private SaveScoreController saveScoreController;
    [SerializeField]
    private TextMeshProUGUI bestScoreUI;
    [SerializeField]
    private Button startGameButton;
    [SerializeField]
    private Animation loadSceneAnimation;
    [SerializeField]
    private AnimationClip loadSceneClip;
    [SerializeField]
    private float loadSceneDelayTime;
    
    void Start()
    {
        startGameButton.onClick.AddListener(StartGameOnClick);
        saveScoreController.LoadBestScore(bestScoreUI);
    }

    private void StartGameOnClick()
    {
        startGameButton.enabled = false;
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        loadSceneAnimation.Play(loadSceneClip.name);
        yield return new WaitForSeconds(loadSceneDelayTime);
        SceneManager.LoadScene(1);
    }
}
