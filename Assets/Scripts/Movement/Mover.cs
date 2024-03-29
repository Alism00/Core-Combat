using RPG.Core;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows.Speech;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField]
        private float maxSpeed = 7 ;
        private Animator _animation;
        private NavMeshAgent agent;
        
        ActionScheduler actionScheduler;
        // Start is called before the first frame update
        void Start()
        {
            actionScheduler = GetComponent<ActionScheduler>();
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

        public void StartMoveAction(Vector3 destination,float speedFraction)
        {
            actionScheduler.StartAction(this);
            MoveTo(destination,speedFraction);
            
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            agent.isStopped = false;
            agent.destination = destination;
            agent.speed = maxSpeed * speedFraction;

        }
        public void Cancel()
        {
            agent.isStopped = true;
        }
        public void DisableNavMesh()
        {
            agent.enabled = false;
        }

    }
}

