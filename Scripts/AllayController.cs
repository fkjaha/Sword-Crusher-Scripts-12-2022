using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AllayController : AAutoJumper
{
    [SerializeField] private int wallLayerIndex;
    [SerializeField] private float fallGravityMultiplier;
    [SerializeField] private Vector3 minJumpDirection;
    [SerializeField] private Vector3 maxJumpDirection;

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
        MoveY();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == wallLayerIndex)
            BounceTheWall(collision.contacts[0].normal);
    }

    private protected override void Jump(float force)
    {
        // _rigidbody.velocity = MirrorVector3(_rigidbody.velocity.normalized, normal) * force;
        _rigidbody.velocity = GetRandomJumpDirection() * force;
    }

    private void BounceTheWall(Vector3 wallNormal)
    {
        _rigidbody.velocity = MirrorVector3(_rigidbody.velocity.normalized, wallNormal) * jumpForce;
    }
    
    private void MoveY()
    {
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity += Vector3.up * Physics.gravity.y * Time.deltaTime * fallGravityMultiplier;
        }
    }

    private Vector3 GetRandomJumpDirection()
    {
        return new Vector3(Random.Range(minJumpDirection.x, maxJumpDirection.x), Random.Range(minJumpDirection.y, maxJumpDirection.y),
            Random.Range(minJumpDirection.z, maxJumpDirection.z));
    }
    
    private Vector3 MirrorVector3(Vector3 inputVector, Vector3 rotationAxis)
    {
        return Quaternion.AngleAxis(180, Vector2.Perpendicular(rotationAxis)) * inputVector;
    }

    private void OnDrawGizmos()
    {
        try
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, _rigidbody.velocity.normalized);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
