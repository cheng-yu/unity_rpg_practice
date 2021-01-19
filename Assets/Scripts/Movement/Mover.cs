using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement {
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] float maxSpeed = 6f;
        NavMeshAgent agent;
        Health health;

        private void Start() {
            agent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }
        // Update is called once per frame
        void Update()
        {
            agent.enabled = !health.isDead();
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction) {
            GetComponent<ActionScheduler>().startAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            agent.destination = destination;
            agent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            agent.isStopped = false;
        }

        public void Cancel()
        {
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
