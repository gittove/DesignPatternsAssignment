using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance = null;
    
    [SerializeField] [Range(0, 1000)] private int _poolSize;
    [SerializeField] private GameObject _objectPrefab;
    private Queue<GameObject> _ObjectPool;

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
        _ObjectPool = new Queue<GameObject>();

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject go = Instantiate(_objectPrefab, this.transform);
            go.SetActive(false);
            _ObjectPool.Enqueue(go);
        }
    }

    public GameObject ObjectPoolSpawn()
    {
        if (_ObjectPool.Count > 0)
        {
            GameObject spawn = _ObjectPool.Dequeue();
            Debug.Log("Dequeued");
            spawn.SetActive(true);
            return spawn;
        }

        else
        {
            return Instantiate(_objectPrefab, this.transform);
        }
    }

    public void ObjectPoolReturn(GameObject go)
    {
        go.SetActive(false);
        _ObjectPool.Enqueue(go);
    }
}