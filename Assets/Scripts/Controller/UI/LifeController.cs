using UnityEngine;
using DG.Tweening;

public class LifeController : MonoBehaviour
{
    public void PlayInitAnimation() => transform.DOScale(Vector3.one, 0.5f);

    public void PlayDestroyAnimation()
    {
        transform.DOScale(Vector3.zero, 0.5f).onComplete += () =>
        {
            Destroy(gameObject);
        };
    }
}
