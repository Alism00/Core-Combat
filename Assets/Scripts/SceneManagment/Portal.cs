using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagment
{
    public class Portal : MonoBehaviour
    {
        
        enum DestinationIdentifier
        {
            A , B , C , D ,E
        }
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeWaitTime = 0.5f ;
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] public Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;

        Fader fader;

        private void Start()
        {
                fader = FindObjectOfType<Fader>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }
        }
        private IEnumerator Transition()
        {
            if(sceneToLoad < 0)
            {
                Debug.Log("Scene to load is not srlecte");

                yield break;
            }
            
            DontDestroyOnLoad(gameObject);
            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            Portal otherportal =  GetOtherPortal();
            UpdatePlayer(otherportal);
            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.Fadein(fadeInTime);
            Destroy(gameObject);

        }

        private void UpdatePlayer(Portal otherportal)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherportal.spawnPoint.position);
            player.transform.rotation = otherportal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal ==  this) continue;

                if (portal.destination != destination) continue;

                return portal;
            }
            return null;
        }
    }
}

