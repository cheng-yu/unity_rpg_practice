using UnityEngine;

namespace RPG.Core {

	public class Health : MonoBehaviour {

		[SerializeField] float healthPoints = 100f;

		bool alreadyDead = false;

		public bool isDead() {
			return alreadyDead;
		}

		public void TakeDamage(float amount) {

			healthPoints = Mathf.Max(healthPoints - amount, 0);

			if(healthPoints == 0) {
				Die();
			}
			print(healthPoints);
		}

		private void Die() {
			if(alreadyDead) return;

			GetComponent<Animator>().SetTrigger("die");
			alreadyDead = true;
		}
	}
}
