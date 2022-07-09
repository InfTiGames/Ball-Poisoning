using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton { get; private set; }
    
    //// Эффекты   

    // По коду сделай обджект пул, и адекватно используй синглтон
    // Рефакторинг

    //Небольшие анимации передвижения шарика, и анимация взрыва пули и смерти преград

    [SerializeField] SizeBar _sizeBar;
    [SerializeField] float _currentSize;
    static float _maxSize = 3.6f;  

    [SerializeField] GameObject _bulletPrefab;
    GameObject _bullet;
    Transform _finish;
    Vector3 _scaleFactor;
    [SerializeField] Transform _bulletPosition;
    bool _isClicked;    
    float _minimalSize = 1f;      
    [SerializeField] LayerMask _obstacleMask;
    GameManager _gameManager;

    Animator _animator;

    void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        _animator = GetComponentInParent<Animator>();
        _finish = Finish.singleton.transform;
        _gameManager = GameManager.singleton;
        _currentSize = _maxSize;
        _sizeBar.SetMaxSize(_maxSize);
        transform.rotation = Quaternion.LookRotation(_finish.position);
        _scaleFactor = new Vector3(0.4f, 0.4f, 0.4f);
    }

    void FixedUpdate()
    {       
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
            _animator.SetBool("Moving", false);
        }
    }

    void GameChanges()
    {
        transform.localScale -= _scaleFactor / 2 * Time.fixedDeltaTime;
        _bullet.transform.localScale += _scaleFactor * Time.fixedDeltaTime;
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
        _animator.SetBool("Moving", true);

        if (Vector3.Distance(transform.position, _finish.position) <= 5f)
        {

        }
    }

    void OnMouseDown()
    {
        if (_bullet == null && _gameManager.IsGameActive == true)
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
        if (other.gameObject.TryGetComponent(out EnemiesController enemy))
        {
            gameObject.SetActive(false);
            _gameManager.GameOver();
            return;
        }
        if (other.gameObject.TryGetComponent(out Finish door))
        {
            _gameManager.WinGame();
            return;
        }
    }
}