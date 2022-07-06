using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public static Finish singleton { get; private set; }

    private void Awake()
    {
        singleton = this;   
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
