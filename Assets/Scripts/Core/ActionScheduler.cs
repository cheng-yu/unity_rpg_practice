using UnityEngine;

namespace RPG.Core
{
	public class ActionScheduler : MonoBehaviour {

		IAction prevAction;

		public void startAction(IAction action) {
			if(action == prevAction) return;
			if(prevAction != null) {
				print("stop" + prevAction);
				prevAction.Cancel();
			}
			prevAction = action;
		}
	}
}
