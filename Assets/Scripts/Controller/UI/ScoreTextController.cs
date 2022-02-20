using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ScoreTextController : MonoBehaviour
{
    [SerializeField]
    private float showSpeed;
    [SerializeField]
    private float hideSpeed;
    [SerializeField]
    private float delayTime;
    
    private void Start()
    {
        StartCoroutine(PlayDestroyScoreClipAnimation());
    }

    IEnumerator PlayDestroyScoreClipAnimation()
    {
        transform.DOScale(Vector3.one, showSpeed);
        yield return new WaitForSeconds(delayTime);
        
        transform.DOScale(Vector3.zero, hideSpeed).onComplete += () =>
        {
            Destroy(gameObject);
        };
    }
}
