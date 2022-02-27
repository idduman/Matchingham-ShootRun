using System.Collections;
using UnityEngine;
namespace ShootRun
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] private float _shootInterval;
        [SerializeField] private WeaponBehaviour _weapon;

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

            if (other.CompareTag("Door1"))
            {
                _weapon.CurrentWeapon = 1;
            }
            else if (other.CompareTag("Door2"))
            {
                _weapon.CurrentWeapon = 2;
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

        private IEnumerator ShootRoutine()
        {
            while (true)
            {
                if (Shooting && !_finished)
                {
                    yield return new WaitForSeconds(_shootInterval);
                    _weapon.Shoot();
                }
                yield return null;
            }
        }
    }
}

