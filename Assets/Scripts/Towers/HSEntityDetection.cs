using UnityEngine;

public class HSEntityDetection : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Entity entity = other.GetComponent<Entity>();

        if(entity != null)
        {
            TowerEvents.HiddenSpikeAttack();
        }
    }
}
