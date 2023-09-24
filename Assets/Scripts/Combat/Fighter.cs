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
        [SerializeField] float attackRateTime = 1f;
        [SerializeField] float weaponRange = 3f;
        private Health target;
        private Mover mover;
        private float TimeSinceLastAttack = Mathf.Infinity;
        ActionScheduler actionScheduler;
        [SerializeField]
        private int weaponDamage = 5;
        Health health;
        Animator animator;
        private void Start()
        {
            
            actionScheduler = GetComponent<ActionScheduler>();
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            TimeSinceLastAttack += Time.deltaTime;
            if (target == null) { return; }
            if (target.IsDead()) { return; }
            if (target != null)
            {
                if (!IsInRange())
                {
                    mover.MoveTo(target.transform.position);
                }
                else if (IsInRange())
                {
                    AttackBehaviour();
                    mover.Cancel();
                }
                
            }

        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform.position);
            if (TimeSinceLastAttack >= attackRateTime)
            {
                // this will trigger  Hit() Event
                animator.ResetTrigger("StopAttack");
                animator.SetTrigger("Attack");
                TimeSinceLastAttack = 0f;
            }
            
        }
        // Animation Event 
        private void Hit()
        {
            if (target == null) return;
            target.GetDamage(weaponDamage);
        }
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targettotest = combatTarget.GetComponent<Health>();
            return targettotest != null && !targettotest.IsDead();
        }
        public void Attack(GameObject combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }
        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("StopAttack");
        }

        bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) <= weaponRange;
        }
        
    }
}
