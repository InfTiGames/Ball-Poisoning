using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemiesPrefab;
    int _enemiesCount = 100;
 
    void Awake()
    {
        for (int i = 0; i < _enemiesCount; i++)
        {
            Instantiate(_enemiesPrefab, RandomPos(), Quaternion.identity, gameObject.transform);
        }
    }

    Vector3 RandomPos()
    {
        float vertBorder = Random.Range(8f, 30f);
        float horBorder = Random.Range(-5f, 13f);

        Vector3 randomPos = new Vector3(horBorder, 1.5f, vertBorder);
        return randomPos;
    }
}