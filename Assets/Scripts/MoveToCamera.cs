using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCamera : MonoBehaviour
{
    SpawnManager spawnManager;

    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.back * spawnManager.GetCurrentSpeed() * Time.deltaTime, Space.World);
        if(transform.position.z < spawnManager.boundaryZ)
        {
            Destroy(gameObject);
        }
    }
}
