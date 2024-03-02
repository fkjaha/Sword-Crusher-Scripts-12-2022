using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfigurator : MonoBehaviour
{
    [SerializeField] private Renderer viewTarget;
    [SerializeField] private Camera targetCamera;

    
    [ContextMenu("Configure")]
    private void ConfigureCamera()
    {
        Bounds bounds = new Bounds(  viewTarget.transform.position, Vector3.zero);
        bounds.Encapsulate(viewTarget.bounds);
        Vector3 boundsSize = bounds.size;
        float diagonal =
            Mathf.Sqrt(Mathf.Pow(boundsSize.x, 2) + Mathf.Pow(boundsSize.y, 2));
        targetCamera.orthographicSize = diagonal/2;
    }
}
