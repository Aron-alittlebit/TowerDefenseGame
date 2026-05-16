using Mono.Cecil.Cil;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    float gravity = -2 * 9.81f;
    Vector3 velocity;
    Vector3 pos;
    private void Start()
    {
        pos = transform.position;
    }

    private void Update()
    {
        Debug.Log(velocity.y);
        Debug.Log(pos);
        if (velocity.y < 0 && pos.y <= 0)
            velocity.y = -2;

        if (pos.y >= 0)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        pos.y -= velocity.y;
    }
}
