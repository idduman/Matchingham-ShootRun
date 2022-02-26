using UnityEngine;

namespace ShootRun
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private float _steerSpeed = 5f;
        [SerializeField] private float _responsiveness = 10f;
        [SerializeField] private float _clampX = 3f;
    
        public bool Active;
        
        private bool _started;
        private float _offsetX;
        
        private PlayerBehaviour _player;
    
        private void Awake()
        {
            _player = GetComponent<PlayerBehaviour>();
        }
    
        // Start is called before the first frame update
        private void Start()
        {
            _started = false;
            Active = true;
            Subscribe();
        }
    
        private void OnDestroy()
        {
            Unsubscribe();
        }
    
        private void Update()
        {
            if (!_started || !Active)
                return;
            
            var pos = transform.position;
            pos.z += _moveSpeed * Time.deltaTime;
            pos.x = Mathf.Lerp(pos.x, _offsetX, _responsiveness * Time.deltaTime);
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
            _player.Shooting = true;
        }
    
        private void OnMoved(Vector3 inputDelta)
        {
            _offsetX = Mathf.Clamp(_offsetX + inputDelta.x * _steerSpeed, -_clampX, _clampX);
        }
    }
}
