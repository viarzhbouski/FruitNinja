using UnityEngine;

[CreateAssetMenu(fileName = "New Extra Block", menuName = "ExtraBlock", order = 54)]
public class ExtraBlockConfig : ScriptableObject
{
    [SerializeField]
    private GameObject extraBlockPrefab;

    [SerializeField]
    private float fruitSpeed;
    
    [SerializeField]
    private float fruitRotateSpeed;
    
    [SerializeField]
    private float fragmentSpeed;

    [SerializeField]
    private float fragmentRotateSpeed;

    public GameObject ExtraBlockPrefab
    {
        get { return extraBlockPrefab; }
    }
    
    public float FruitSpeed
    {
        get { return fruitSpeed; }
    }
    
    public float FruitRotateSpeed
    {
        get { return fruitRotateSpeed; }
    }

    public float FragmentSpeed
    {
        get { return fragmentSpeed; }
    }
    
    public float FragmentRotateSpeed
    {
        get { return fragmentRotateSpeed; }
    }
}