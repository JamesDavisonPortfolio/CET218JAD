using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPodScript : MonoBehaviour
{
    public LineRenderer connection;
    // Start is called before the first frame update
    void Start()
    {
        connection = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        connection.SetPosition(0, transform.position);
        connection.SetPosition(1, transform.root.position);
    }

    void ShotDown()
    {
        Debug.Log("Pod script Ran");
        gameObject.SetActive(false);
        gameObject.transform.root.GetComponent<BossScript>().PodDestroyed();
    }
}
