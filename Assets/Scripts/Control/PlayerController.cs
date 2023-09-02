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
            MouseMovment();
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
                    fighter.Attack();
                    
                }
                return true;
            }
            return false;
        }

        private void MouseMovment()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCurser();
            }
        }

        private void MoveToCurser()
        {
            ray = GetMouseRay();
            RaycastHit hit;
            bool ishit = Physics.Raycast(ray, out hit);
            Debug.DrawRay(Camera.main.transform.position,hit.point, Color.yellow);
            if (ishit)
            {
                move.MoveTo(hit.point);
               
            }

        }
        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

