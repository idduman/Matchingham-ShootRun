using System.Collections;
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
        [SerializeField] private float _shootDistance;
        
        public bool Shooting;
        private Transform _finish;
        
        private PlayerController _controller;

        private bool _finished;
        
        void Start()
        {
            _controller = GetComponent<PlayerController>();
            _finish = GameObject.FindGameObjectWithTag("Finish").transform;
            Shooting = false;
            StartCoroutine(ShootRoutine());
        }
        private void OnDestroy()
        {
            StopAllCoroutines();
        }
        
        void Update()
        {
            if (_finished)
                return;
            
            if (transform.position.z > _finish.position.z)
                Finish(true);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IObstacle>(out var obs))
            {
                Finish(false);
            }
        }
        
        private void Finish(bool success)
        {
            if (_finished)
                return;
            
            _finished = true;
            _controller.Active = false;
            StopAllCoroutines();
            GameManager.Instance.FinishGame(success);
        }

        private void Shoot()
        {
            var bullet = Instantiate(_bulletPrefab, _muzzle.position, Quaternion.identity);
            bullet.Shoot(_shootVelocity, _shootDistance);
        }

        private IEnumerator ShootRoutine()
        {
            while (true)
            {
                if (Shooting && !_finished)
                {
                    yield return new WaitForSeconds(_shootInterval);
                    Shoot();
                }
                yield return null;
            }
        }
    }
}

