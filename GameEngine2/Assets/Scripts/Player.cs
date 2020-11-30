using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public float jumpPower;

    float hAxis;
    float vAxis;

    bool wDown;
    bool jDown;

    bool isJump;

    Vector3 moveVec;

    Rigidbody rigid;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();    
    }

    void Update()
    {
        GetInput();
        Move();
        Trun();
        Jump();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * (wDown ? 1.5f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", wDown);
        anim.SetBool("isWalk", moveVec != Vector3.zero);
    }

    void Trun()
    {
        //transform.Rotate(new Vector3(0, hAxis, 0) * rotateSpeed * Time.deltaTime);
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        anim.SetBool("isJump", false);
        isJump = false;
    }
}