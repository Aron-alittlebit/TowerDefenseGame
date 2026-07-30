using System.Collections;
using UnityEngine;

public class burrowEffect : MonoBehaviour
{
    [SerializeField] Transform originalPos;
    [SerializeField] float BurrowDepth;
    
    
    [SerializeField] float transitionDuration;
    public bool IsBurrowing { get; private set; }

    private void Start()
    {
        IsBurrowing = false;
        originalPos = transform;
        
    }

    public void ToggleBurrow()
    {
        if (!IsBurrowing)
        {
            StartCoroutine(Burrow());
            
        }
        else
        {
            StartCoroutine(UnBurrow());
        }
    }


    IEnumerator Burrow()
    {
        IsBurrowing = true;
        Vector3 startPos = originalPos.localPosition;
        Vector3 targetPos = new Vector3(startPos.x, BurrowDepth, startPos.z);
        float time = 0;
        
        while (time <= transitionDuration)
        {

            originalPos.localPosition = Vector3.Lerp(startPos, targetPos, time/transitionDuration);
            time += Time.deltaTime;
            yield return null;
        }

        originalPos.localPosition = targetPos;

    }



    IEnumerator UnBurrow()
    {
        IsBurrowing = false;
        Vector3 startPos = originalPos.localPosition;
        Vector3 targetPos = new Vector3(startPos.x, 0, startPos.z);
        float time = 0;
        //dirtParticles.Play();
        while (time <= transitionDuration)
        {

            originalPos.localPosition = Vector3.Lerp(startPos, targetPos, time / transitionDuration);
            time += Time.deltaTime;
            yield return null;
        }
        originalPos.localPosition = targetPos;
        
    }
}
