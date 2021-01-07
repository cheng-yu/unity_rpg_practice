using UnityEngine;

namespace RPG.Core
{
	public class ActionScheduler : MonoBehaviour {

		MonoBehaviour prevAction;

		public void startAction(MonoBehaviour action) {
			if(action == prevAction) return;
			if(prevAction != null) {
				print("stop action" + prevAction);
			}

			prevAction = action;
		}
	}
}
