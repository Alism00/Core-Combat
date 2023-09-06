using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] float weaponRange = 5f;
        private Transform target;
        private Mover mover;
        ActionScheduler actionScheduler;
        private void Start()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            mover = GetComponent<Mover>();
        }

        private void Update()
        {

            if (target != null)
            {
                if (!GetIsInRange())
                {
                    mover.MoveTo(target.position);
                }
                
            }

        }
        // Start is called before the first frame update
        public void Attack(CombatTarget combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.transform;
            print("damage");
        }
        public void Cancel()
        {
            target = null;
        }
        bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) <= weaponRange;
        }
    }
}
