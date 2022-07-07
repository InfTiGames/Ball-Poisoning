using UnityEngine;

public class Finish : MonoBehaviour
{
    public static Finish singleton { get; private set; }

    private void Awake()
    {
        singleton = this;   
    }
}