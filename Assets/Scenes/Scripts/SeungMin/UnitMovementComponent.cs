using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMovementComponent : MonoBehaviour
{
    private GameObject player;
    private UnitAttackComponent attackComponent;
    private Vector3 attackPosition;
    [SerializeField]
    private float speed=5f;
    [SerializeField]
    private float recognitionDistance = 30f;
    private NavMeshAgent navMeshAgent;

    private IEnumerator ChasePlayer()
    {
        while (true)
        {
            if((transform.position - player.transform.position).magnitude <= recognitionDistance)
            {
                attackPosition = player.transform.position + (transform.position - player.transform.position).normalized * attackComponent.attackRange;
                if (navMeshAgent.SetDestination(attackPosition))
                    navMeshAgent.isStopped = false;
                else
                   navMeshAgent.isStopped =  true;
            }
        }
    }


    private void OnEnable()
    {
        player = GameObject.Find("Player"); 
        attackComponent = GetComponent<UnitAttackComponent>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        navMeshAgent.isStopped = true;
    }
}
