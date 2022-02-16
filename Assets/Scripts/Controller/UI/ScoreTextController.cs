using System.Collections;
using UnityEngine;

public class ScoreTextController : MonoBehaviour
{
    [SerializeField]
    private Animation animation;
    [SerializeField]
    private AnimationClip destroyScoreClip;
    [SerializeField]
    private float delayTime;
    
    void Start()
    {
        StartCoroutine(PlayDestroyScoreClipAnimation());
    }

    IEnumerator PlayDestroyScoreClipAnimation()
    {
        yield return new WaitForSeconds(delayTime);
        animation.Play(destroyScoreClip.name);
    }
}
