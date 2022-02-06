using System.Collections;
using UnityEngine;

public class DestroyByDelayController : MonoBehaviour
{
    [SerializeField]
    private float delay;
    
    void Start()
    {
        StartCoroutine(DestroyByDelay());
    }

    IEnumerator DestroyByDelay()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
