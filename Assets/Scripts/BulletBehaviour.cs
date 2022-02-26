using System;
using UnityEngine;

namespace ShootRun
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletBehaviour : MonoBehaviour
    {
        private Rigidbody _rb;
        private float _maxDistance;
        private Vector3 _startPos;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if(Vector3.Distance(_startPos, _rb.position) > _maxDistance)
                Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.TryGetComponent<IObstacle>(out var obs))
            {
                obs.Damage(other.contacts[0].point);
            }
            Destroy(gameObject);
        }

        public void Shoot(float speed, float maxDistance)
        {
            _startPos = _rb.position;
            _rb.velocity = speed * Vector3.forward;
            _maxDistance = maxDistance;
        }
    }
}

