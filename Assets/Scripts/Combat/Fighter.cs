using RPG.Movement;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour, IAction {

        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        Health target;
        float timeSinceLastAttack = 0;

        private void Update()
        {

            timeSinceLastAttack += Time.deltaTime;

            if(target == null) return;
            if(target.isDead()) return;

            if (!getIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour() {
            transform.LookAt(target.transform);
            if(timeSinceLastAttack > timeBetweenAttacks) {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack() {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool getIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget) {
            if(combatTarget == null) {
                return false;
            }
            Health health = combatTarget.GetComponent<Health>();
            return health != null && !health.isDead();
        }

        public void Attack(GameObject combatTarget) {
            GetComponent<ActionScheduler>().startAction(this);
            target = combatTarget.GetComponent<Health>();
            print("Take that you short, squat peasant!!");
        }

        public void Cancel() {
            TriggerStopAttack();
            target = null;
        }

        private void TriggerStopAttack() {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        //Animation event 
        void Hit() {
            if(target == null) return;

            target.TakeDamage(weaponDamage);
        }
    }
}
