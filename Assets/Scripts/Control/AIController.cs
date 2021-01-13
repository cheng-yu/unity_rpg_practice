using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Movement;

namespace RPG.Control {
     public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Fighter fighter;
        GameObject player;
        private void Start() {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }

        private void Update() {
            if(PlayerInAttackRange() && fighter.CanAttack(player)) {
                fighter.Attack(player);
            } else {
                fighter.Cancel();
            }
        }

        private bool PlayerInAttackRange() {
            return DistanceToPlayer() < chaseDistance;
        }

        private float DistanceToPlayer() {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(player.transform.position, transform.position);
        }
    }
}
