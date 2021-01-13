using UnityEngine;

namespace RPG.Core
{
	public class ActionScheduler : MonoBehaviour {

		IAction currentAction;

		public void startAction(IAction action) {
			if(action == currentAction) return;
			if(currentAction != null) {
				print("stop" + currentAction);
				currentAction.Cancel();
			}
			currentAction = action;
		} 

		public void cancelCurentAction() {
			startAction(null);
		}
	}
}
