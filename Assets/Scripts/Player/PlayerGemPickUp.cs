using UnityEngine;

public class PlayerGemPickUp : MonoBehaviour
{
    [SerializeField]float Radius = 5f;
    public int GemCounter = 0;
    public LayerMask GemLayer;
    void Update()
    {
        Debug.Log(GemCounter);
        Collider[] colliders = Physics.OverlapSphere(transform.position,Radius, GemLayer);
        foreach(var collider in colliders)
        {
            GemCounter++;
            Destroy(collider.gameObject);
        }
    }
}
