using UnityEngine;

public class BulletController : MonoBehaviour
{
    public static BulletController singleton { get; private set; }

    private float _explosionRadius;

    private void Awake()
    {
        singleton = this;
    }

    private void OnTriggerEnter(Collider other)
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

    private void OnDestroy()
    {
        _explosionRadius = transform.localScale.x + 0.5f;
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

    private void DamageEnemy(Transform enemy)
    {
        Destroy(enemy.gameObject);
    }
}