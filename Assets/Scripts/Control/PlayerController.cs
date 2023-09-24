using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{

    public class PlayerController : MonoBehaviour
    {
        Ray ray;
        Health health;
        private Mover move;
        private Fighter fighter;
        // Start is called before the first frame update
        void Start()
        {
            health = GetComponent<Health>();
            move = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        // Update is called once per frame
        void Update()
        {
            if (health.isDead) {
                move.DisableNavMesh();
                return; }
            if (MouseCombat()) { return; }
            if (MouseMovment()) { return; }
            print("nothing");

        }

        private bool MouseCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null)
                {
                    continue;
                }
                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }
                if (Input.GetMouseButton(0))
                {
                    fighter.Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool MouseMovment()
        {

            ray = GetMouseRay();
            RaycastHit hit;
            bool ishit = Physics.Raycast(ray, out hit);
            if (ishit)
            {
                if (Input.GetMouseButton(0))
                {
                    move.StartMoveAction(hit.point);
                    Debug.DrawRay(ray.origin, ray.direction * 100);
                }
                return true;
            }
            return false;
        }


        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

