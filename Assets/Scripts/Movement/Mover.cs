using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement {
    public class Mover : MonoBehaviour
    {
        NavMeshAgent agent;

        private void Start() {
            agent = GetComponent<NavMeshAgent>();    
        }
        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination) {
            GetComponent<ActionScheduler>().startAction(this);
            GetComponent<Fighter>().Cancel();
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            agent.destination = destination;
            agent.isStopped = false;
        }

        public void Stop() {
            agent.isStopped = true;
        }

        private void UpdateAnimator() {
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}
