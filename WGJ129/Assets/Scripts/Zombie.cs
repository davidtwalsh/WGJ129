using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public bool hasAggro;
    public float knockbackForce;
    
    public Transform target;
    public float speed = 5f;
    Vector3[] path;
    int targetIndex;
    Rigidbody2D rb;

    bool stunned = false;
    public float hitByBulletStunTime;
    float hitByBulletTimer = 0f;
    float recalculatePathTimer = 0f;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    private void Update()
    {
        StunRoutine();
        RecalculatePathRoutine();

    }


    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void HitByBullet(Transform _transform)
    {
        StopCoroutine("FollowPath");
        path = null;
        targetIndex = 0;
        rb.AddForce(_transform.right * knockbackForce, ForceMode2D.Impulse);
        stunned = true;
    }

    void StunRoutine()
    {
        if (stunned)
        {
            hitByBulletTimer += Time.deltaTime;

            if (hitByBulletTimer >= hitByBulletStunTime)
            {
                stunned = false;
                hitByBulletTimer = 0f;               
            }
        }
    }

    void RecalculatePathRoutine()
    {
        if (hasAggro && stunned == false)
        {
            recalculatePathTimer += Time.deltaTime;
            if (recalculatePathTimer >= .2f)
            {
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
                recalculatePathTimer = 0f;
            }
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one * .2f);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
