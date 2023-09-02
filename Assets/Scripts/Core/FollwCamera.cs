using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace RPG.Core
{
    public class FollwCamera : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.position = target.position;
        }
    }
}

