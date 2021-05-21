using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnedBlades : MonoBehaviour
{
    public GameObject player;
    public SpawnCoreScript core;
    public float timeAsForm;
    public float speed;
    public float rotationSpeed;
    

    private NavMeshAgent navAgent;
    private float timeTransformed;
    private float timeUntilTransform;
    // Start is called before the first frame update
    private void Awake()
    {
        navAgent = gameObject.transform.root.GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        core = gameObject.transform.root.GetComponent<SpawnCoreScript>();
        navAgent = gameObject.transform.root.GetComponent<NavMeshAgent>();
        timeTransformed = Time.time;
        navAgent.speed = speed;
        timeUntilTransform = Random.Range(10, 20);
    }
    private void OnEnable()
    {
        timeTransformed = Time.time;
        timeUntilTransform = Random.Range(10, 20);
        navAgent.isStopped = false;
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
        if(Time.time >= timeTransformed + timeUntilTransform)
        {
            navAgent.isStopped = true;
            core.SwapToLaserFromBlade();
        }
    }

    void Movement()
    {
        navAgent.SetDestination(player.transform.position);
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    

    
}
