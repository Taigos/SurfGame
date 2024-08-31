using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCamera_NoDestroy : MonoBehaviour
{
    SpawnManager spawnManager;

    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.back * spawnManager.GetCurrentSpeed() * Time.deltaTime, Space.World);
    }
}
