using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton { get; private set; }
    //    ������ �����
    //    �������
    //������ ��� �� ���� �� ������ ����������� ���� ��� �� ���� �����������, ����� ����� ������ �������� ��� �������� ������
    //��������� �������� ��������
    //�� ���� ������ ������� ���, � ��������� ��������� ��������
    //�����������

    public SizeBar SizeBar;
    [SerializeField] private float _currentSize;
    private static float _maxSize = 3.6f;   

    [SerializeField] private GameObject _bulletPrefab;
    private GameObject _bullet;
    private Transform _finish;
    private Vector3 _scaleFactor = new Vector3(0.4f, 0.4f, 0.4f);
    private Vector3 _bulletSpawnPosition = new Vector3(1.4f, 1.5f, 4.5f);
    private bool _isClicked;
    private bool _canMove;
    private float _minimalSize = 1f;

    GameManager _gameManager;    

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        _finish = Finish.singleton.transform;
        _gameManager = GameManager.singleton;
        _currentSize = _maxSize;
        SizeBar.SetMaxSize(_maxSize);
    }

    private void FixedUpdate()
    {
        if (_gameManager.IsGameActive == true)
        {
            if (_isClicked)
            {
                GameChanges();
                if (transform.localScale.x <= _minimalSize)
                {
                    _gameManager.GameOver();
                }             
            }
            else
            {
                MoveBullet();
            }
            MoveToFinish();
        }
    }

    public void SetTrueForMove()
    {
        _canMove = true;
    }

    private void GameChanges()
    {
        transform.localScale -= _scaleFactor / 2 * Time.fixedDeltaTime;
        _bullet.transform.localScale += _scaleFactor * Time.fixedDeltaTime;
        _currentSize -= _scaleFactor.x / 2 * Time.fixedDeltaTime;
        SizeBar.SetSize(_currentSize);        
    }

    private void MoveBullet()
    {
        if (_bullet != null)
        {
            _bullet.transform.position = Vector3.MoveTowards(_bullet.transform.position, _finish.position, 10f * Time.fixedDeltaTime);
        }
    }

    private void MoveToFinish()
    {
        if (_canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, _finish.position, 10f * Time.fixedDeltaTime);
        }
    }

    private void OnMouseDown()
    {
        if (_bullet == null && _gameManager.IsGameActive == true)
        {
            _isClicked = true;
            _bullet = Instantiate(_bulletPrefab, _bulletSpawnPosition, Quaternion.identity);
        }
    }

    private void OnMouseUp()
    {
        _isClicked = false;        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemiesController enemy))
        {
            gameObject.SetActive(false);
            _gameManager.GameOver();
        }

        if (other.gameObject.TryGetComponent(out Finish door))
        {
            _gameManager.WinGame();
        }
    }
}