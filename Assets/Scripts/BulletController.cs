using UnityEngine;

public class BulletController : MonoBehaviour
{
    public static BulletController singleton { get; private set; }
    [SerializeField] GameObject _hitFx;
    [SerializeField] GameObject _deathFx;


    float _explosionRadius;

    void Awake()
    {
        singleton = this;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemiesController enemy))
        {
            Destroy(gameObject);            
        }

        if (other.TryGetComponent(out Finish door))
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        _explosionRadius = transform.localScale.x/2 + 0.5f;
        Collider[] collider = Physics.OverlapSphere(transform.position, _explosionRadius);
        if (collider.Length > 0)
        {
            foreach (Collider col in collider)
            {
                if (col.TryGetComponent(out EnemiesController enemiess))
                {
                    DamageEnemy(enemiess.transform);                
                }
            }
        }
    }

    void DamageEnemy(Transform enemy)
    {
        GameObject fx = Instantiate(_hitFx, transform.position, Quaternion.identity);
        Destroy(fx, 1f);
        GameObject enemyDeathFx = Instantiate(_deathFx, enemy.position, Quaternion.identity);
        Destroy(enemyDeathFx, 1f);

        Destroy(enemy.gameObject);
    }
}