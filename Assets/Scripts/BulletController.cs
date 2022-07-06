using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public static BulletController singleton { get; private set; }

    private void Awake()
    {
        singleton = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemiesController enemy))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
