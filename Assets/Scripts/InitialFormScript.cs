using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialFormScript : MonoBehaviour
{
    public GameObject spawnPrefab;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnChild", 10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnChild()
    {
        Instantiate(spawnPrefab, spawnPoint.position, spawnPrefab.transform.rotation);
    }
}
