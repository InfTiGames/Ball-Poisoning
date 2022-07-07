using UnityEngine;

public class WayToFinish : MonoBehaviour
{
    private Transform _player;   
    private Vector3 _scale;
    private GameManager _gameManager;

    private void Start()
    {
        _player = PlayerController.singleton.transform;
        _gameManager = GameManager.singleton;
    }

    private void Update()
    {
        if (_player != null)
        {
            _scale = new Vector3(_player.lossyScale.x / 10, 1, 3.75f);
            transform.localScale = _scale;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out EnemiesController enemy))
        {
            _gameManager._goButton.gameObject.transform.localScale = new Vector3(2, 2, 2);
        }
    }
}