using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitSystem;
    private bool _allow = true;

    private void OnCollisionEnter(Collision collision)
    {
        if(_allow) StartCoroutine(HitSystem(collision.contacts[0].point));
    }

        
    private IEnumerator HitSystem(Vector3 position)
    {
        _allow = false;
        ParticleSystem a = ParticlesPool.Instance.GetPS();
        a.gameObject.SetActive(true);
        a.transform.position = position;
        a.Play(true);
        yield return new WaitForSeconds(.2f);
        _allow = true;
    }
}
