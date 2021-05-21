using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOffenseScript : MonoBehaviour
{
    public GameObject firingPoints;
    public GameObject regularBullet;

    public BossScript core;

    bool firing;
    // Start is called before the first frame update
    void Start()
    {
        core = gameObject.transform.root.GetComponent<BossScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    IEnumerator fireBullets()
    {
        while(firing)
        {
            
            yield return new WaitForSeconds(Random.Range(1f, 4f));
        }
    }
    
}
