using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows.Speech;
namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        private Animator _animation;
        private NavMeshAgent agent;
        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            _animation = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 Velocity = agent.velocity;
            Vector3 LocalVelocity = transform.InverseTransformDirection(Velocity);
            float speed = LocalVelocity.z;
            _animation.SetFloat("ForwardSpeed", speed);
            
        }



        public void MoveTo(Vector3 destination)
        {
            agent.destination = destination;
        }
    }
}

