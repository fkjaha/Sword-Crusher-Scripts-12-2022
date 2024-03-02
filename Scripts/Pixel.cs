using UnityEngine;

public class Pixel : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Rigidbody pixelRigidbody;
    [SerializeField] private PhysicMaterial physicMaterial;

    [SerializeField] private DestructiveShape parentShape;
    
    private bool _isReleased;

    public void Initialize(DestructiveShape shape, Material newMaterial)
    {
        parentShape = shape;
        material = newMaterial;
        UpdateVisuals();
    }
    
    public void UpdateVisuals()
    {
        meshRenderer.material = material;
    }

    public void DetachPixel()
    {
        if(_isReleased) return;
        
        parentShape.ReleasePixel(this);
        pixelRigidbody = gameObject.AddComponent<Rigidbody>();
        pixelRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX |
                                     RigidbodyConstraints.FreezeRotationY;
        
        _isReleased = true;
        
        // pixelRigidbody.isKinematic = false;
    }
}
