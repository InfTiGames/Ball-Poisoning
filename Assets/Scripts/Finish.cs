using UnityEngine;

public class Finish : MonoBehaviour
{
    public static Finish singleton { get; private set; }

    void Awake()
    {
        singleton = this;   
    }
}