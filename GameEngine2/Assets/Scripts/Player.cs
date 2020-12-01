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
    float fireDelay;

    bool wDown;
    bool jDown;
    bool fDown;
    bool sDown1;
    bool sDown2;

    bool isJump;
    bool isFireReady;

    public GameObject[] weapons;
    Rigidbody rigid;
    Animator anim;
    GameObject equipWeapon;

    Vector3 moveVec;
    
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
        Swap();
        Attack();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        fDown = Input.GetButtonDown("Fire1");

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
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    void Swap()
    {
        int weaponIndex = 0;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;


        if ((sDown1 || sDown2) && !isJump)
        {
            if (equipWeapon != null)
                equipWeapon.SetActive(false);
            equipWeapon = weapons[weaponIndex];
            equipWeapon.SetActive(true);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        isJump = false;
    }

    void Attack()
    {
        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if (fDown && isFireReady && !isJump)
        {
            equipWeapon.Use();
            anim.SetTrigger("doFire");
            fireDelay = 0;
        }
    }
}