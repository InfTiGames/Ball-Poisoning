using UnityEngine;

public class Finish : MonoBehaviour
{
    public static Finish Singleton { get; private set; }
    Animator _animator;
    Transform _player;

    void Awake()
    {
        Singleton = this;
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _player = PlayerController.Singleton.transform;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, _player.position) <= 20f)
        {
            _animator.SetBool("OpenDoor",true);
        }
    }
}