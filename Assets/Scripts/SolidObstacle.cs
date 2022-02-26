using System;
using UnityEngine;

namespace ShootRun
{
    [RequireComponent(typeof(Rigidbody))]
    public class SolidObstacle : MonoBehaviour, IObstacle
    {
        private Rigidbody _rb;
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Damage(Vector3 point)
        {
            _rb.AddExplosionForce(6f, point, 2f, 0.2f, ForceMode.Impulse);
            _rb.AddForce(2f * Vector3.forward, ForceMode.Impulse);
        }
    }
}
