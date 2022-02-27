using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootRun
{
    public class WeaponBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _followPoint;
        [SerializeField] private float _shootVelocity;
        [SerializeField] private float _shootMaxDistance;
        [SerializeField] private float _multishotAngle;
        [SerializeField] private BulletBehaviour _bulletPrefab;
        [SerializeField] private List<Transform> _weaponModels = new List<Transform>();
        
        private Transform _currentWeaponTransform;

        private int _currentWeapon;
        public int CurrentWeapon
        {
            get => _currentWeapon;
            set
            {
                _currentWeapon = Mathf.Clamp(value, 0, _weaponModels.Count - 1);
                _currentWeaponTransform = _weaponModels[_currentWeapon];
                for (int i = 0; i < _weaponModels.Count; i++)
                {
                    _weaponModels[i].gameObject.SetActive(i == _currentWeapon);
                }
            }
        }

        private void Start()
        {
            CurrentWeapon = 0;
        }

        private void Update()
        {
            if(_followPoint)
                transform.position = _followPoint.position;
        }

        public void Shoot()
        {
            for (int i = 0; i <= CurrentWeapon; i++)
            {
                var initialAngle = (CurrentWeapon / 2f) * _multishotAngle;
                var rotation = Quaternion.Euler(0f, -initialAngle + i * _multishotAngle, 0f);
                var bullet = Instantiate(_bulletPrefab, _currentWeaponTransform.position, rotation);
                bullet.Shoot(_shootVelocity, _shootMaxDistance);
            }

        }
    }
}
