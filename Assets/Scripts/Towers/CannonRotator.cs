using UnityEngine;

public class CannonRotator : MonoBehaviour
{
    [SerializeField] Transform Pivotpoint;
    [SerializeField] float rotationSpeed = 90f;
    void Update()
    {
        Pivotpoint.Rotate(0f,rotationSpeed*Time.deltaTime,0f);
    }
}
