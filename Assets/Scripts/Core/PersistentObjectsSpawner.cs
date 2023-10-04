using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject persistentObjectPrefab;

    static bool hasSpawned ;

    private void Awake()
    {
        if(hasSpawned) return;

        SpawnPersistentObject();

        hasSpawned = true;
    }

    private void SpawnPersistentObject()
    {
        GameObject Persistentobject = Instantiate(persistentObjectPrefab);
        DontDestroyOnLoad(Persistentobject);
    }
}
