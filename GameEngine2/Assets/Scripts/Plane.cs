using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    bool changeTarget;
    Vector3 target1 = new Vector3(-80, 50, -100);
    Vector3 target2 = new Vector3(0, 10, 0);

    private void Start()
    {
        changeTarget = false;
    }


    void Update()
    {
        if (!changeTarget)
            transform.position = Vector3.MoveTowards(transform.position, target1, 1f);
        
        if (transform.position == target1)
        {
            changeTarget = true;
        }

        if (changeTarget)
            transform.position = Vector3.Slerp(transform.position, target2, 0.005f);
    }
}