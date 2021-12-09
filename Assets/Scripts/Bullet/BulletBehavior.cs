using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private float _shootForce;
    private float _lifeTimer;
    private float _destroyQueue;
    private bool _isVisible;
    private Rigidbody _rb;

    private void Awake()
    {
        _shootForce = 10f;
        _lifeTimer = 0f;
        _destroyQueue = 5f;
        _isVisible = false;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isVisible)
        {
            _lifeTimer += Time.deltaTime;
        }

        if (_lifeTimer > _destroyQueue)
        {
            _isVisible = false;
            _lifeTimer = 0f;
            ObjectPool.instance.ObjectPoolReturn(this.gameObject);
        }
    }

    private void OnBecameVisible()
    {
        _isVisible = true;
        _rb.AddForce(0, 0, _shootForce, ForceMode.Impulse);
    }
}
