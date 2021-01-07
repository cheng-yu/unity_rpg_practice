﻿using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;


namespace RPG.Control {
    public class PlayerController : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if(IntersactWithCombat()) return;
            if(IntersactWithMovement()) return;
            print("no where to go");
        }

        private bool IntersactWithCombat() {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach(RaycastHit hit in hits) {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(target == null) continue;

                if(Input.GetMouseButtonDown(0)) {
                    GetComponent<Fighter>().Attack(target);
                }
                return true;
            }

            return false;
        }
        private bool IntersactWithMovement()
        {
            Ray ray = GetMouseRay();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}