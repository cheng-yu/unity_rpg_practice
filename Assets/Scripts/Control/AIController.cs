using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Movement;

namespace RPG.Control {
     public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        private void Update() {
            if(DistanceToPlayer() < chaseDistance) {
                GameObject player = GameObject.FindWithTag("Player");
                if(!GetComponent<Fighter>().CanAttack(player)) {
                    GetComponent<Mover>().StartMoveAction(player.transform.position);
                } else {
                    GetComponent<Fighter>().Attack(player);
                }
            } else {
                GetComponent<Mover>().Cancel();
                GetComponent<Fighter>().Cancel();
            }
        }

        private float DistanceToPlayer() {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(player.transform.position, transform.position);
        }
    }
}
