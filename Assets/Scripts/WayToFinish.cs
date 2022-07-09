using UnityEngine;

public class WayToFinish : MonoBehaviour
{
    Transform _player;    
    LineRenderer _way;
    float _width;
    Vector3 _endPos;

    void Start()
    {
        _way = GetComponent<LineRenderer>();
        _player = PlayerController.Singleton.transform;        
        _endPos = new Vector3(8f, 0.5f, 23f);
        _way.SetPosition(1, _endPos);
    }

    void FixedUpdate()
    {
        UpdateLine();
    }

    void UpdateLine()
    {
        _width = _player.localScale.x;
        Vector3 position = new Vector3(_player.position.x, 0.8f, _player.position.z + 0.05f);
        if (_player != null)
        {
            if (Vector3.Distance(_player.position, _endPos) <= 2)
            {
                gameObject.SetActive(false);
            }
            _way.startWidth = _width;
            _way.endWidth = _width;
            _way.SetPosition(0, position);
        }
    }
}