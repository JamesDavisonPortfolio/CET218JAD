using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordStrike : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LaserCore"))
        {
            transform.root.GetComponent<PlayerController>().ChargeCannon();
            Destroy(other.transform.root.gameObject);
        }
        if (other.CompareTag("BossLaserCore"))
        {
            transform.root.GetComponent<PlayerController>().ChargeCannon();
            other.SendMessage("Smashed");
        }if (other.CompareTag("SpikeSpawn"))
        {
            Destroy(other.transform.root.gameObject);
        }
    }
}
