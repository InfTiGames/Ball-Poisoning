using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    private GameObject _bullet;
    private Transform _finish;
    private Vector3 _scaleFactor = new Vector3(0.5f, 0.5f, 0.5f);
    private Vector3 _bulletSpawnPosition = new Vector3(1.4f, 1.5f, 4.5f);
    private bool _isClicked;
    private bool _move;

    private float _minimalSize = 0.6f;

    public static PlayerController singleton { get; private set; }

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        _finish = Finish.singleton.transform;        
    }

    private void FixedUpdate()
    {
        if (_isClicked)
        {
            transform.localScale -= _scaleFactor/2 * Time.fixedDeltaTime;            
            _bullet.transform.localScale += _scaleFactor * Time.fixedDeltaTime;
            if (transform.localScale.x <= _minimalSize)
            {
                Time.timeScale = 0;
            }
        }
        else
        {
            if (_bullet != null)
            {
                _bullet.transform.position = Vector3.MoveTowards(_bullet.transform.position, _finish.position, 10f * Time.fixedDeltaTime);
            }            
        }

        if (_move)
        {
            MoveToFinish();
        }
    }

    public void SetTrueForMove()
    {
        _move = true;
    }

    private void MoveToFinish()
    {
        transform.position = Vector3.MoveTowards(transform.position, _finish.position, 10f * Time.fixedDeltaTime);
    }

    private void OnMouseDown()
    {
        _isClicked = true;
        _bullet = Instantiate(_bulletPrefab, _bulletSpawnPosition, Quaternion.identity);
    }

    private void OnMouseUp()
    {
        _isClicked = false;        
    }
}