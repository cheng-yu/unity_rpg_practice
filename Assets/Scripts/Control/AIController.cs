using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Control {
     public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] float potralOnWayPointTime = 3f;
        [SerializeField] PatrolPath potralPath;
        [SerializeField] float waypointTolerance = 1f;

        Mover mover;
        Fighter fighter;
        Health health;
        GameObject player;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        int currentWaypointIndex = 0;
        float timeSinceMoveToCurrentWayPoint = Mathf.Infinity;
        private void Start() {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");

            guardPosition = transform.position;
        }

        private void Update()
		{
			if (health.isDead()) return;
			if (PlayerInAttackRange() && fighter.CanAttack(player))
			{
				AttackBehaviour();
			}
			else if (timeSinceLastSawPlayer < suspicionTime)
			{
				SuspiciousBehaviour();
			}
			else
			{
				PatrolBehaviour();
			}

			UpdateTimer();
		}

		private void UpdateTimer()
		{
			timeSinceLastSawPlayer += Time.deltaTime;
			timeSinceMoveToCurrentWayPoint += Time.deltaTime;
		}

		private void PatrolBehaviour()
		{
            Vector3 nextPosition = guardPosition;

            if(potralPath != null) {
                if(AtWaypoint()) {
                    timeSinceMoveToCurrentWayPoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if(timeSinceMoveToCurrentWayPoint > potralOnWayPointTime) {
			    mover.StartMoveAction(nextPosition);
            }
		}

		private Vector3 GetCurrentWaypoint()
		{
			return potralPath.GetPoint(currentWaypointIndex);
		}

		private void CycleWaypoint()
		{
			currentWaypointIndex = potralPath.GetNextPointIndex(currentWaypointIndex);
		}

		private bool AtWaypoint()
		{
			float distance = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distance < waypointTolerance;
		}

		private void SuspiciousBehaviour()
		{
			GetComponent<ActionScheduler>().cancelCurentAction();
		}

		private void AttackBehaviour()
		{
            timeSinceLastSawPlayer = 0;
			fighter.Attack(player);
		}

		private bool PlayerInAttackRange() {
            return DistanceToPlayer() < chaseDistance;
        }

        private float DistanceToPlayer() {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(player.transform.position, transform.position);
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
