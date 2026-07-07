using System.Collections;
using UnityEngine;

public class RailGunBeamVisual : MonoBehaviour
{
    [Header("Line Renderers")]
    [SerializeField] private LineRenderer coreLine;  
    [SerializeField] private LineRenderer glowLine;  

    [Header("Charge Visuals")]
    [SerializeField] private Transform chargeParticleOrSphere; 

    [Header("Beam Tuning")]
    [SerializeField] private float baseCoreWidth = 0.4f;
    [SerializeField] private float baseGlowWidth = 2.0f;

    public void PlayVisual(Vector3 startPos, Vector3 endPos, 
        float chargeDuration, float laserDisplayDuration)
    {
        StartCoroutine(ExecuteSequence(startPos, endPos, chargeDuration, 
            laserDisplayDuration));
    }

    private IEnumerator ExecuteSequence(Vector3 start, Vector3 end, float chargeTime, 
        float laserTime)
    {
        
        coreLine.enabled = false;
        glowLine.enabled = false;

        if (chargeParticleOrSphere != null)
        {
            chargeParticleOrSphere.gameObject.SetActive(true);
            chargeParticleOrSphere.position = start;
            chargeParticleOrSphere.localScale = Vector3.zero;

            float elapsed = 0f;
            while (elapsed < chargeTime)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / chargeTime;

                
                chargeParticleOrSphere.localScale = Vector3.one * Mathf.Lerp(0f, 1.5f, 
                    progress);
                yield return null;
            }
        }
        else
        {
            yield return new WaitForSeconds(chargeTime);
        }

       
        if (chargeParticleOrSphere != null) chargeParticleOrSphere.gameObject
                .SetActive(false);

        coreLine.enabled = true;
        glowLine.enabled = true;

        coreLine.positionCount = 2;
        coreLine.SetPosition(0, start);
        coreLine.SetPosition(1, end);

        glowLine.positionCount = 2;
        glowLine.SetPosition(0, start);
        glowLine.SetPosition(1, end);

        
        float beamElapsed = 0f;
        while (beamElapsed < laserTime)
        {
            beamElapsed += Time.deltaTime;
            float progress = beamElapsed / laserTime;

            
            coreLine.startWidth = Mathf.Lerp(baseCoreWidth, 0f, progress);
            coreLine.endWidth = coreLine.startWidth;

            glowLine.startWidth = Mathf.Lerp(baseGlowWidth, 0f, progress);
            glowLine.endWidth = glowLine.startWidth;

            yield return null;
        }

        
        Destroy(gameObject);
    }
}
