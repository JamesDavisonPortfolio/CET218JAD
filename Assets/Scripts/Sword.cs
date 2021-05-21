using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    bool swinging;
    bool returning;
    float count;
    float countPoint;
    Vector3 startPosition;
    Vector3 endPosition;
    public Collider swordCollider;
    // Start is called before the first frame update
    void Start()
    {
        count = 20;
        countPoint = 0;
        swinging = false;
        returning = false;
        startPosition = transform.localEulerAngles;
        endPosition = startPosition + new Vector3(0, 80, 40);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !swinging && !returning)
        {
            swordCollider.enabled = true;
            swinging = true;
            StartCoroutine(SwingSword());
        } 
    }
    //private void FixedUpdate()
    //{
    //    if (swinging)
    //    {
    //        SwingSword(1 / countPoint);
    //        if(countPoint == count)
    //        {
    //            swinging = false;
    //            returning = true;
    //        }
    //        else
    //        {
    //            countPoint++;
    //        }
    //    }
    //    else if (returning)
    //    {
    //        SwingSword(1 / countPoint);
    //        if (countPoint == 0)
    //        {
    //            returning = false;
    //        }
    //        else
    //        {
    //            countPoint--;
    //        }
    //    }
    //}

    IEnumerator SwingSword()
    {
        
        while (swinging)
        {
            transform.localEulerAngles = Vector3.Slerp(endPosition, startPosition, 1 / countPoint);
            yield return new WaitForSeconds(0.5f / count);
            if (countPoint == count)
            {
                swinging = false;
                returning = true;
                countPoint = 0;
                swordCollider.enabled = false;
            }
            else
            {
                countPoint++;
            }
        }
        while (returning)
        {
            transform.localEulerAngles = Vector3.Slerp(startPosition, endPosition, 1 / countPoint);
            yield return new WaitForSeconds(1 / count);
            if (countPoint == count)
            {
                returning = false;
                countPoint = 0;
            }
            else
            {
                countPoint++;
            }
        }
    }
}

