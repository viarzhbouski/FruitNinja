using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField]
    private Animation animation;
    [SerializeField]
    private AnimationClip increaseClip;
    [SerializeField]
    private AnimationClip decreaseClip;
    
    public void PlayInitAnimation() => animation.Play(increaseClip.name);
    
    public void PlayDestroyAnimation() => animation.Play(decreaseClip.name);

    public void DeleteLifeImage() => Destroy(gameObject);
}
