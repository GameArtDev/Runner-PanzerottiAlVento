using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyController : EnemyController
{
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private List<Transform> waypoints = new List<Transform>();

    [SerializeField]
    private float minDistToWaypoint = 0.5f;
       
    private int counterNextWaypoint = 0;
    private Transform currentWaypoint = null;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        counterNextWaypoint = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {        
        if (waypoints.Count > 0)
        {
            ReachWaypoint();
            
        }
    }

    private void ReachWaypoint()
    {
        if (currentWaypoint == null || Vector2.Distance(transform.position, currentWaypoint.position) < minDistToWaypoint)
        {
            currentWaypoint = waypoints[counterNextWaypoint];
            counterNextWaypoint++;
            if (counterNextWaypoint >= waypoints.Count)
            {
                counterNextWaypoint = 0;
            }
        }

        Vector3 movementVector = currentWaypoint.position - transform.position;

        gameObject.transform.Translate(movementVector.normalized * speed * Time.deltaTime);
    }

}
