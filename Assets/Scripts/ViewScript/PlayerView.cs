using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;
    PlayerController _player;
    public LayerMask obstacleMask;
    public LayerMask targets;

    private void Start()
    {
        StartCoroutine("FindObstacles", 0.2f);
        _player = PlayerController.singleton;
    }

    IEnumerator FindObstacles(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleObstacles();
        }
    }

    void FindVisibleObstacles()
    {
        Collider[] obstaclesInView = Physics.OverlapSphere(transform.position, viewRadius, obstacleMask);

        for (int i = 0; i < obstaclesInView.Length; i++)
        {
            Transform obstacle = obstaclesInView[i].transform;
            Vector3 dirToObstacle = (obstacle.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToObstacle) < viewAngle/2)
            {
                float distanceToObstacle = Vector3.Distance(transform.position, obstacle.position);
                if (!Physics.Raycast(transform.position, dirToObstacle,distanceToObstacle, targets))
                {
                    _player.MoveToFinish();
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleDeg, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleDeg += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleDeg * Mathf.Deg2Rad), 0, Mathf.Cos(angleDeg * Mathf.Deg2Rad));
    }
}