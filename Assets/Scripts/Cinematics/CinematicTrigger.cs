using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool isPlayed;
        private void OnTriggerEnter(Collider other)
        {
            if(!isPlayed && other.CompareTag("Player"))
            {
                GetComponent<PlayableDirector>().Play();
                isPlayed = true;
            }
        }
    }
}