using UnityEngine;

public class Tower : MonoBehaviour
{
    public static Tower Instance;
    public LayerMask EntityLayer;

    void Awake()
    {
        Instance = this;
    }
}
