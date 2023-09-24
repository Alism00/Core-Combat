using RPG.Combat;
using RPG.Control;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] float chaseDistance = 5f;
    [SerializeField] private float suspicionTime = 2.5f;
    [SerializeField] private PatrolPath patrolPath;
    [SerializeField] private float wayPointDwellTime = 2f;
    [Range(0f, 1f)]
    [SerializeField] private float patrolSpeedFraction =Mathf.Clamp01(0.2f);

    float wayPointRange = 1f;
    private Health health;
    private GameObject target;
    private Fighter fighter;
    private Mover mover;
    private Vector3 guardPosition;
    private float wayPointReachedTime = Mathf.Infinity;
    private float lastSawTime = Mathf.Infinity;
    ActionScheduler actionScheduler;
    int currentWayPointIndex  = 0 ;

    // Start is called before the first frame update
    void Start()
    {
        guardPosition = transform.position;
        mover = GetComponent<Mover>();
        health = GetComponent<Health>();
        fighter = GetComponent<Fighter>();
        target = GameObject.FindWithTag("Player");
        actionScheduler = GetComponent<ActionScheduler>();
    }

    // Update is called once per frame
    void Update()
    {

        if (health.isDead)
        {
            mover.DisableNavMesh();
            return;
        }
        if (IsInAttackRange() && fighter.CanAttack(target))
        {
            
            AttackBehaviour();
        }
        else if (lastSawTime < suspicionTime)
        {
            SuspiciosBehavior();
        }
        else
        {
            PatrolBehaviour();
        }
        UpdateTimer();
        
    }

    private void UpdateTimer()
    {
        lastSawTime += Time.deltaTime;
        wayPointReachedTime += Time.deltaTime;
        Debug.Log(wayPointReachedTime);
    }

    private void SuspiciosBehavior()
    {
        actionScheduler.CancelCurrentAction();
    }

    private void AttackBehaviour()
    {
        lastSawTime = 0;
        fighter.Attack(target);
    }
void PatrolBehaviour()
    {
       
        Vector3 nextPosition = guardPosition;
        if (patrolPath != null)
        {
            if (AtWayPoint())
            {
                wayPointReachedTime = 0;
                    CycleWayPoint();
            }
            nextPosition = GetCurrentWayPoint();
        }
        if (wayPointReachedTime > wayPointDwellTime)
        {
            mover.StartMoveAction(nextPosition,patrolSpeedFraction);
        }
        
    }

    private Vector3 GetCurrentWayPoint()
    {
        return patrolPath.GetWayPoint(currentWayPointIndex);
    }

    private void CycleWayPoint()
    {
        currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
    }

    private bool AtWayPoint()
    {
        float distance = Vector3.Distance(GetCurrentWayPoint(),transform.position);
        return distance < wayPointRange ;
    }

    private bool IsInAttackRange()
    {
        float DistanceWithPlayer = Vector3.Distance(this.transform.position, target.transform.position);
        return DistanceWithPlayer < chaseDistance;
    }
    
    
    // Unity Calls
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(this.transform.position , chaseDistance);
    }
}
