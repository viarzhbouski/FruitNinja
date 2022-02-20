using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] 
    private Image imageForAnim;
    [SerializeField]
    private SaveScoreController saveScoreController;
    [SerializeField]
    private Text bestScoreUI;
    [SerializeField]
    private Button startGameButton;
    [SerializeField]
    private float loadSceneDelayTime;
    
    private void Start()
    {
        imageForAnim.color = Color.black;
        imageForAnim.DOFade(0f, 0.5f);
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
        imageForAnim.DOFade(1f, 0.5f);
        yield return new WaitForSeconds(loadSceneDelayTime);
        SceneManager.LoadScene(1);
    }
}
