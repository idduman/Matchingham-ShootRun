using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

namespace ShootRun
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _muzzle;
        [SerializeField] private BulletBehaviour _bulletPrefab;
        [SerializeField] private float _shootInterval;
        [SerializeField] private float _shootVelocity;

        public bool Shooting;
        
        private PlayerController _controller;
        // Start is called before the first frame update
        void Awake()
        {
            _controller = GetComponent<PlayerController>();
            Shooting = false;
            StartCoroutine(ShootRoutine());
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IObstacle>(out var obs))
            {
            }
        }

        private void Fail()
        {
            GameManager.Instance.FinishGame(false);
        }

        private void Shoot()
        {
            var bullet = Instantiate(_bulletPrefab, _muzzle.position, Quaternion.identity);
            bullet.Shoot(_shootVelocity);
        }

        private IEnumerator ShootRoutine()
        {
            while (true)
            {
                if (Shooting)
                {
                    yield return new WaitForSeconds(_shootInterval);
                    Shoot();
                }
                yield return null;
            }
        }
    }
}

