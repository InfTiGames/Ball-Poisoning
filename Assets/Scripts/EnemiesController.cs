using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    private void OnDestroy()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, 1f); 
        
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
        EnemiesController enemies = enemy.GetComponent<EnemiesController>();
        if (enemies != null)
        {
            Destroy(enemies.gameObject);
        }
    }
}