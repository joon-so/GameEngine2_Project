using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target;
    public float smoothig = 1.0f;
    public float dist = 8f;
    public float height = 20f;

    void Update()
    {
        float currYAngle = Mathf.LerpAngle(transform.eulerAngles.y, target.transform.eulerAngles.y,
            smoothig * Time.deltaTime);

        Quaternion rot = Quaternion.Euler(0, currYAngle, 0);

        transform.position = target.transform.position - (rot * Vector3.forward * dist)
            + (Vector3.up * height);

        transform.LookAt(target.transform);
    }
}
