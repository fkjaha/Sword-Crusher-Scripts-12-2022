using System;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPool : MonoBehaviour
{
    [SerializeField] private int poolSize;
    [SerializeField] private List<GameObject> coinPrefabs;
    [SerializeField] private Transform coinsParent;

    private Queue<GameObject> _pool;

    private void Start()
    {
        InitializePool();
    }

    public GameObject GetCoin()
    {
        GameObject coin = _pool.Dequeue();
        _pool.Enqueue(coin);
        return coin;
    }

    private void InitializePool()
    {
        _pool = new();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject coin = Instantiate(coinPrefabs[i % coinPrefabs.Count], coinsParent);
            _pool.Enqueue(coin);
            coin.SetActive(false);
        }
    }
}
