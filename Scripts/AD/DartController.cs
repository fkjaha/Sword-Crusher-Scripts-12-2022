using UnityEngine;

public class DartController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbodyToFollow;

    private void Update()
    {
        transform.LookAt(transform.position - rigidbodyToFollow.velocity.normalized);
    }
}
