using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;

namespace RPG.Control
{

    public class PlayerController : MonoBehaviour
    {
        Ray ray;
        private Mover move;
        private Fighter fighter;
        // Start is called before the first frame update
        void Start()
        {
            move = GetComponent<Mover>();   
            fighter =GetComponent<Fighter>();
        }

        // Update is called once per frame
        void Update()
        {
            
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
                if (target == null) { continue; }
                if (Input.GetMouseButtonDown(0))
                {
                    fighter.Attack(target);
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
                    Debug.DrawRay(ray.origin,ray.direction *100 );
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

