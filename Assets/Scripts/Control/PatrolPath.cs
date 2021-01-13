using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Control {
    public class PatrolPath : MonoBehaviour
    {

        float pathPointGizmosRadious = 0.3f;
        private void OnDrawGizmos() {
            Gizmos.color = Color.white;
            for (int i = 0; i < transform.childCount; i++)
            {
                Vector3 currentPoint = GetPoint(i);

                Gizmos.DrawSphere(currentPoint, pathPointGizmosRadious);

                Vector3 nextPoint = GetPoint(GetNextPointIndex(i));

                Gizmos.DrawLine(currentPoint, nextPoint);
            }
        }

        public int GetNextPointIndex(int i) {
            if(i + 1 == transform.childCount) {
                return 0;
            } else {
                return i + 1;
            }
        }

        public Vector3 GetPoint(int i) {
            return transform.GetChild(i).position;
        }
    }
}

