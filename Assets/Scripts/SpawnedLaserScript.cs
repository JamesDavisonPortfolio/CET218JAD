using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedLaserScript : MonoBehaviour
{
    public LineRenderer bossBeam;
    public GameObject firePoint;
    public GameObject player;
    public ParticleSystem chargeParticles;
    public ParticleSystem readyParticles;
    public ParticleSystem hitParticles;
    public SpawnCoreScript core;
    public float force;

    public Vector3 firingDirection;
    bool firing;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        core = gameObject.transform.root.GetComponent<SpawnCoreScript>();
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(FireBeam());
    }

    // Update is called once per frame
    void Update()
    {
        if (firing)
        {
            RaycastHit hitObject;
            if (Physics.Raycast(firePoint.transform.position, firingDirection, out hitObject, 200))
            {
                bossBeam.enabled = true;
                bossBeam.SetPosition(0, firePoint.transform.position);
                bossBeam.SetPosition(1, hitObject.point);
                hitParticles.transform.position = hitObject.point;
                hitParticles.Play();
                if (hitObject.collider.CompareTag("Player"))
                {
                    hitObject.collider.GetComponent<PlayerController>().TakeDamage(25, (hitObject.transform.position - hitObject.point).normalized * force);
                }
            }
        }
    }

    IEnumerator FireBeam()
    {
        var mainPart = chargeParticles.main;
        mainPart.simulationSpeed = 0.1f;
        chargeParticles.Play();
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.5f);
            mainPart.simulationSpeed += 0.05f;
        }
        chargeParticles.Stop();
        readyParticles.Play();
        yield return new WaitForSeconds(1f);
        firingDirection = (player.transform.position - firePoint.transform.position).normalized;
        firing = true;
        yield return new WaitForSeconds(10f);
        readyParticles.Stop();
        hitParticles.Stop();
        readyParticles.Clear();
        hitParticles.Clear();
        bossBeam.enabled = false;
        firing = false;
        core.SwapToBlade();
    }
}
