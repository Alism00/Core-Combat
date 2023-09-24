using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] public bool isDead =false;
        [SerializeField] public int healthPoint = 20;
        public bool IsDead() { return isDead; }

        public void GetDamage(int damage)
        {
            healthPoint = Mathf.Max(healthPoint - damage, 0);
            if (healthPoint == 0 && !isDead)
            {
                isDead = true;
                Death();
            }
        }
        void Death()
        {
            GetComponent<Animator>().SetTrigger("Death");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
