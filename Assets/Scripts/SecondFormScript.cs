using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFormScript : MonoBehaviour
{
    public LineRenderer[] lasers;
    public GameObject[] cores;
    public Transform[] firePoints;
    public ParticleSystem[] hitParticles;
    public float force;
    public float rotationSpeed;

    public int coreCount;
    bool firing;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireLasers());
        coreCount = cores.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (firing)
        {
            for (int i = 0; i < firePoints.Length; i++)
            {
                if(firePoints[i] != null)
                {
                    lasers[i].enabled = true;
                    RaycastHit hitObject;
                    if (Physics.Raycast(firePoints[i].transform.position, firePoints[i].forward, out hitObject, 200))
                    {
                        lasers[i].enabled = true;
                        lasers[i].SetPosition(0, firePoints[i].transform.position);
                        lasers[i].SetPosition(1, hitObject.point);
                        hitParticles[i].transform.position = hitObject.point;
                        hitParticles[i].Play();
                        if (hitObject.collider.CompareTag("Player"))
                        {
                            hitObject.collider.GetComponent<PlayerController>().TakeDamage(25, (hitObject.transform.position - hitObject.point).normalized * force);
                        }
                    }
                }
            }
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }

    IEnumerator FireLasers()
    {
        yield return new WaitForSeconds(10f);
        firing = true;
        yield return new WaitForSeconds(5f);
        firing = false;
        for(int i = 0; i < firePoints.Length; i++)
        {
            if(lasers[i] != null)
            {
                lasers[i].enabled = false;
                hitParticles[i].Stop();
                hitParticles[i].Clear();
            }
        }
        CheckCores();
        StartCoroutine(FireLasers());
    }

    void CheckCores()
    {
        if(coreCount <= 0)
        {
            for(int i = 0; i < cores.Length; i++)
            {
                cores[i].SetActive(true);
                coreCount = cores.Length;
            }
        }
    }
}
