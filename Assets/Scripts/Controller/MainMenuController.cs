using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
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
    }

    private void StartGameOnClick() => StartCoroutine(LoadScene());

    IEnumerator LoadScene()
    {
        loadSceneAnimation.Play(loadSceneClip.name);
        yield return new WaitForSeconds(loadSceneDelayTime);
        SceneManager.LoadScene(1);
    }
}
