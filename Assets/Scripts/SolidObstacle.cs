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
            _rb.AddExplosionForce(3f, point, 2f, 0.1f, ForceMode.Impulse);
            _rb.AddForce(3f * Vector3.forward, ForceMode.Impulse);
        }
    }
}
