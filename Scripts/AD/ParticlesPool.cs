using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesPool : MonoBehaviour
{
    public static ParticlesPool Instance;
    
    [SerializeField] private int poolSize;
    [SerializeField] private ParticleSystem hitPrefab;
    [SerializeField] private Transform prtclsParent;

    private Queue<ParticleSystem> _pool;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializePool();
    }

    public ParticleSystem GetPS()
    {
        ParticleSystem coin = _pool.Dequeue();
        _pool.Enqueue(coin);
        return coin;
    }

    private void InitializePool()
    {
        _pool = new();
        for (int i = 0; i < poolSize; i++)
        {
            ParticleSystem coin = Instantiate(hitPrefab, prtclsParent);
            _pool.Enqueue(coin);
            coin.gameObject.SetActive(false);
        }
    }
}
