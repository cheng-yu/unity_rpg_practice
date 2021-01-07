using RPG.Movement;
using UnityEngine;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour {

        [SerializeField] float weaponRange = 2f;
        Transform target;
        private void Update()
        {
            if(target == null) return;

            if (!getIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Stop();
            }
        }

        private bool getIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget) {
            target = combatTarget.transform;
            print("Take that you short, squat peasant!!");
        }

        public void Cancel() {
            target = null;
        }

    }
}