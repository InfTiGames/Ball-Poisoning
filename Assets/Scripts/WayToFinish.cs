using UnityEngine;

public class WayToFinish : MonoBehaviour
{
    Transform _player;
    Transform _finish;
    LineRenderer _way;
    float _width;
    Vector3 zPos;

    private void Start()
    {
        _way = GetComponent<LineRenderer>();
        _player = PlayerController.singleton.transform;
        _finish = Finish.singleton.transform;
        zPos = new Vector3(_finish.position.x, 0.5f, _finish.position.z);
        _way.SetPosition(1, zPos);
    }

    private void FixedUpdate()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        _width = _player.localScale.x;
        Vector3 position = new Vector3(_player.position.x, 0.8f, _player.position.z + 0.05f);
        if (_player != null)
        {            
            _way.startWidth = _width;
            _way.endWidth = _width;
            _way.SetPosition(0, position);
        }
    }
}