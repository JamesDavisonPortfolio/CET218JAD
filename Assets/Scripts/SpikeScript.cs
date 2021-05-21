using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public float force;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        force = 800f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Blade Trigger C");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Blade Trigger D");
            other.GetComponent<PlayerController>().TakeDamage(5, (other.transform.position - transform.position) * force);
        }
    }
}
