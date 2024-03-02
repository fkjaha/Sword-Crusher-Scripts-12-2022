using UnityEngine;
using UnityEngine.Events;

public class AutoJumpDetector : MonoBehaviour
{
    public event UnityAction onJumpDetected;
    
    [SerializeField] private AAutoJumper autoJumper;

    private void OnTriggerEnter(Collider other)
    {
        autoJumper.CallJump(this);
        onJumpDetected?.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        autoJumper.CallJump(this);
        onJumpDetected?.Invoke();
    }
}
