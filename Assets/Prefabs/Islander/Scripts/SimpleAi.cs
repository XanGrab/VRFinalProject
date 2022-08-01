using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SimpleAi : MonoBehaviour { 
    public NavMeshAgent agent;
    public LayerMask whatIsGround;

    public Vector3 target;
    bool walkPointSet = false;
    public float lookRadius;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        Wander();
    }

    private void Wander() {
        if (!walkPointSet) { SearchWalkPoint(); return; }

        agent.SetDestination(target);

        Vector3 dist = transform.position - target;
        if (dist.magnitude < 1f) {
            // Debug.Log("Reached Walk Point!");
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint() {
        Vector3 randDirection = Random.insideUnitSphere * lookRadius;
        randDirection += transform.position;
        // Debug.Log("rd: " + randDirection);

        NavMeshHit navHit;
        if( NavMesh.SamplePosition(randDirection, out navHit, lookRadius, NavMesh.AllAreas) ) {
            target = navHit.position;

            // Debug.Log("Walk Point Set: " + target);
            walkPointSet = true;
        } else {
            // Debug.Log("Sample position not found.");
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}