using UnityEngine;

namespace ShootRun
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private float _steerSpeed = 5f;
        [SerializeField] private float _responsiveness = 10f;
        [SerializeField] private float _clampX = 3f;

        private bool _active;
        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                _anim.SetBool(_runningParam, _active);
            }
        }
        
        private bool _started;
        private float _offsetX;
        private Animator _anim;
        private PlayerBehaviour _player;
        
        private static readonly int _offsetDeltaParam = Animator.StringToHash("OffsetDelta");
        private static readonly int _runningParam = Animator.StringToHash("Running");
        private static readonly int _weaponParam = Animator.StringToHash("Weapon");

        private void Start()
        {
            _player = GetComponent<PlayerBehaviour>();
            _anim = GetComponentInChildren<Animator>();
            _started = false;
            Subscribe();
        }
    
        private void OnDestroy()
        {
            Unsubscribe();
        }
    
        private void Update()
        {
            if (!Active)
                return;
            
            var pos = transform.position;
            pos.z += _moveSpeed * Time.deltaTime;
            pos.x = Mathf.Lerp(pos.x, _offsetX, _responsiveness * Time.deltaTime);
            _anim.SetFloat(_offsetDeltaParam, _offsetX - pos.x);
            transform.position = pos;
        }

        private void Subscribe()
        {
            InputController.Instance.Pressed += OnPressed;
            InputController.Instance.Moved += OnMoved;
        }
    
        private void Unsubscribe()
        {
            if (!InputController.Instance)
                return;
            
            InputController.Instance.Pressed -= OnPressed;
            InputController.Instance.Moved -= OnMoved;
        }
        
        private void OnPressed(Vector3 pos)
        {
            if (_started)
                return;
    
            _started = true;
            Active = true;
            _player.Shooting = true;
            UIController.Instance.ToggleTutorialPanel(false);
        }
    
        private void OnMoved(Vector3 inputDelta)
        {
            _offsetX = Mathf.Clamp(_offsetX + inputDelta.x * _steerSpeed, -_clampX, _clampX);
        }

        public void ChangeWeaponAnimation(int weapon)
        {
            _anim.SetInteger(_weaponParam, weapon);
        }
    }
}
