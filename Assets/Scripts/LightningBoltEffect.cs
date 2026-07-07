using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightningBoltEffect : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetupLine(Vector3 start, Vector3 end, float duration, Color color, float width = 0.15f)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width * 0.5f;

        
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        
        Destroy(gameObject, duration);
    }
}