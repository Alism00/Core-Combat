using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool alreadyTriggerd;
        private void OnTriggerEnter(Collider other)
        {
            if(!alreadyTriggerd && other.CompareTag("Player"))
            {
                GetComponent<PlayableDirector>().Play();
                alreadyTriggerd = true;
            }
        }
    }
}