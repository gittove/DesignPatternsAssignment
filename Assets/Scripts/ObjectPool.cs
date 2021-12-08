using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance = null;
    
    [SerializeField] [Range(0, 1000)] private int _poolSize;
    [SerializeField] private GameObject _BulletPrefab;
    private Queue<GameObject> BulletObjectPool;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        ObjectPoolWarmUp();
    }

    private void ObjectPoolWarmUp()
    {
        BulletObjectPool = new Queue<GameObject>();

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject go = Instantiate(_BulletPrefab, this.transform);
            go.SetActive(false);
            BulletObjectPool.Enqueue(go);
        }
    }

    public GameObject ObjectPoolSpawn()
    {
        if (BulletObjectPool.Count > 0)
        {
            GameObject spawn = BulletObjectPool.Dequeue();
            spawn.SetActive(true);
            return spawn;
        }

        else
        {
            return Instantiate(_BulletPrefab);
        }
    }

    public void ObjectPoolReturn(GameObject go)
    {
        go.SetActive(false);
        BulletObjectPool.Enqueue(go);
    }
}