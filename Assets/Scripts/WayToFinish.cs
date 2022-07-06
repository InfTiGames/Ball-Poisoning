using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayToFinish : MonoBehaviour
{
    private Transform _player;   
    private Vector3 _scale;    

    private void Start()
    {
        _player = PlayerController.singleton.transform;        
    }

    private void Update()
    {
        _scale = new Vector3(_player.lossyScale.x / 10, 1, 3.9f);
        transform.localScale = _scale;
    }
}