using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : AAutoJumper
{
    [SerializeField] private float sidewaysMoveSpeed;
    [SerializeField] private float fallGravityMultiplier;
    
    private Rigidbody _rigidbody;

    private void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        MoveSideways(TouchInput.Instance.GetInput.x);
        MoveY();
    }
    
    private void MoveSideways(float xInput)
    {
        _rigidbody.velocity = new Vector3(xInput * sidewaysMoveSpeed, _rigidbody.velocity.y);
    }

    private protected override void Jump(float force)
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, force);
    }

    private void MoveY()
    {
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * Time.deltaTime * fallGravityMultiplier;
        }
    }
}
