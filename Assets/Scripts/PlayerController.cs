using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Singleton { get; private set; }

    [SerializeField] LayerMask _obstacleMask;

    enum State { idle, move, finish }
    State _state;

    #region SizeBarFields
    [SerializeField] SizeBar _sizeBar;
    [SerializeField] float _currentSize;
    static float _maxSize = 3.6f;
    float _minimalSize = 1f;
    Vector3 _scaleFactor;
    #endregion

    #region BulletFields
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _bulletPosition;
    GameObject _bullet;
    #endregion

    Transform _finish;  
    bool _isClicked;   
    GameManager _gameManager;
    Animator _animator;

    void Awake()
    {
        Singleton = this;
    }

    void Start()
    {
        _animator = GetComponentInParent<Animator>();
        _finish = Finish.Singleton.transform;
        _gameManager = GameManager.Singleton;
        _currentSize = _maxSize;
        _sizeBar.SetMaxSize(_maxSize);
        transform.rotation = Quaternion.LookRotation(_finish.position);
        _scaleFactor = new Vector3(0.4f, 0.4f, 0.4f);
    }

    void FixedUpdate()
    {
        _animator.SetInteger(nameof(State), (int)_state);
        if (_gameManager.IsGameActive == true)
        {
            
            if (_isClicked)
            {
                GameChanges();
                if (transform.localScale.x <= _minimalSize)
                    _gameManager.GameOver();
            }
            else
            {
                MoveBullet();
                CheckIfWayIsClear();
            }
        }
    }

    void CheckIfWayIsClear()
    {
        RaycastHit hit;
        float radius = transform.localScale.x;
        Vector3 position = new Vector3(transform.position.x, transform.position.y/2, transform.position.z);
        if (!Physics.SphereCast(position, radius / 2, transform.forward, out hit, 4f, _obstacleMask))
        {
            MoveToFinish();
        }
        else
        {
            _state = State.idle;
        }
    }

    void GameChanges()
    {
        transform.localScale -= _scaleFactor / 2 * Time.fixedDeltaTime;
        if (_bullet != null)
        {
            _bullet.transform.localScale += _scaleFactor * Time.fixedDeltaTime;
        }       
        _currentSize -= _scaleFactor.x / 2 * Time.fixedDeltaTime;
        _sizeBar.SetSize(_currentSize);        
    }

    void MoveBullet()
    {
        if (_bullet != null)
        {
            _bullet.transform.position = Vector3.MoveTowards(_bullet.transform.position, _finish.position, 10f * Time.fixedDeltaTime);
        }
    }

    void MoveToFinish()
    {
        transform.position = Vector3.MoveTowards(transform.position, _finish.position, 5f * Time.fixedDeltaTime);
        _state = State.move;
    }

    void OnMouseDown()
    {
        if (_bullet == null && _gameManager.IsGameActive == true && _state != State.move)
        {
            _isClicked = true;
            _bullet = Instantiate(_bulletPrefab, _bulletPosition.position, Quaternion.identity);            
        }
    }

    void OnMouseUp()
    {
        _isClicked = false;        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ObstacleControl obstacle))
        {
            gameObject.SetActive(false);
            _gameManager.GameOver();
            return;
        }
        if (other.gameObject.TryGetComponent(out Finish door))
        {
            _gameManager.WinGame();
            _state = State.finish;            
            return;
        }
    }
}