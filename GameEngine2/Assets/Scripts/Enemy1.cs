using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    float detectDistance = 8f;

    Vector3 moveVec;

    Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("isWork", moveVec != Vector3.zero);

        transform.LookAt(transform.position + moveVec);
    }
}
