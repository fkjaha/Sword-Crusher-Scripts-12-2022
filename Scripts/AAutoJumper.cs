using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class AAutoJumper : MonoBehaviour
{
    [SerializeField] private protected AutoJumpDetector playerJumpDetector;
    [SerializeField] private protected float jumpForce;

    private protected abstract void Jump(float force);

    public void CallJump(AutoJumpDetector jumpDetector)
    {
        if(playerJumpDetector == jumpDetector) Jump(jumpForce);
    }
}
