﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public float jumpPower;

    public Slider hpSlider;
    public int maxHealth;
    public int Health;

    float hAxis;
    float vAxis;

    bool wDown;
    bool jDown;
    bool iDown;
    bool fDown;
    bool mDown;
    bool sDown1;
    bool sDown2;
    bool cDown;

    bool isJump;
    bool isSwap;
    bool isBorder;
    bool isDead = false;
    float deathTime = 5f;
    bool isDamage;

    float fireDelay_L;
    float fireDelay_R;
    bool isFireReady_L = true;
    bool isFireReady_R = true;
    int equipWeaponIndex_L = -1;
    int equipWeaponIndex_R = -1;
    Weapon equipWeapon_L;
    Weapon equipWeapon_R;
    public GameObject[] weapons;
    public bool[] hasWeapons;

    public Camera followCamera;
    Rigidbody rigid;
    Animator anim;
    BoxCollider collid;

    GameObject nearObject;

    public GameObject weapon1;
    public GameObject weapon2;

    Vector3 moveVec;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        collid = GetComponent <BoxCollider>();
    }

     void Start()
    {
        Health = maxHealth;
        weapon1.SetActive(false);
        weapon2.SetActive(false);
    }

    void Update()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run)
            return;
        GetInput();
        Move();
        Trun();
        Jump();
        Swap();
        Interation();
        Wary();
        Attack();
        if (isDead)
        {
            deathTime -= Time.deltaTime;
            if (deathTime < 0)
            {
                SceneManager.LoadScene("Ending");
                GameManager.gm.gState = GameManager.GameState.GameOver;
            }
        }
        hpSlider.value = (float)Health / (float)maxHealth;
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButtonDown("Fire1");
        mDown = Input.GetButton("Fire2");
        iDown = Input.GetButtonDown("Interation");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        cDown = Input.GetButton("Wary");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isSwap || !isFireReady_L || !isFireReady_R)
            moveVec = Vector3.zero;

        if (vAxis == 1)
        {
            if (!isBorder)
            {
                transform.position += transform.forward * moveVec.z * speed * (wDown ? 1.5f : 1f) * Time.deltaTime;
            }
            anim.SetBool("isRun", wDown);
        }
        else if (vAxis == -1)
        {
            if (!isBorder)
            {
                transform.position += transform.forward * moveVec.z * speed * 1f * Time.deltaTime;
            }
            anim.SetBool("isRun", false);
        }
        anim.SetBool("isWalk", moveVec != Vector3.zero);
    }

    void Trun()
    { 
        if (mDown)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 0;
                transform.LookAt(transform.position + nextVec);
            }
        }
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
        if (sDown1 && ((!hasWeapons[0] && !hasWeapons[1]) || (equipWeaponIndex_L == 0) && equipWeaponIndex_R == 1))
            return;
        if (sDown2 && ((!hasWeapons[2] && !hasWeapons[3]) || (equipWeaponIndex_L == 2) && equipWeaponIndex_R == 3))
            return;

        int weaponIndex_L = -1;
        int weaponIndex_R = -1;
        if (sDown1) weaponIndex_L = 0;
        if (sDown1) weaponIndex_R = 1;
        if (sDown2) weaponIndex_L = 2;
        if (sDown2) weaponIndex_R = 3;

        if (sDown1 && !isJump)
        {
            if (equipWeapon_L != null)
                equipWeapon_L.gameObject.SetActive(false);

            equipWeaponIndex_L = weaponIndex_L;
            equipWeapon_L = weapons[weaponIndex_L].GetComponent<Weapon>();
            equipWeapon_L.gameObject.SetActive(true);

            if (equipWeapon_R != null)
                equipWeapon_R.gameObject.SetActive(false);

            equipWeaponIndex_R = weaponIndex_R;
            equipWeapon_R = weapons[weaponIndex_R].GetComponent<Weapon>();
            equipWeapon_R.gameObject.SetActive(true);

            anim.SetTrigger("doSwap");
            isSwap = true;
            Invoke("SwapOut", 0.1f);

            weapon1.SetActive(true);
            weapon2.SetActive(false);
        }

        if (sDown2 && !isJump)
        {
            if(equipWeapon_L != null)
                equipWeapon_L.gameObject.SetActive(false);

            equipWeaponIndex_L = weaponIndex_L;
            equipWeapon_L = weapons[weaponIndex_L].GetComponent<Weapon>();
            equipWeapon_L.gameObject.SetActive(true);

            if (equipWeapon_R != null)
                equipWeapon_R.gameObject.SetActive(false);

            equipWeaponIndex_R = weaponIndex_R;
            equipWeapon_R = weapons[weaponIndex_R].GetComponent<Weapon>();
            equipWeapon_R.gameObject.SetActive(true);

            anim.SetTrigger("doSwap");
            isSwap = true;
            Invoke("SwapOut", 0.1f);

            weapon1.SetActive(false);
            weapon2.SetActive(true);
        }
    }

    void SwapOut()
    {
        isSwap = false;
    }

    void Interation()
    {
        if (iDown && nearObject != null && !isJump)
        {
            if (nearObject.tag == "Weapon" || nearObject.tag == "Punch")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;
                Destroy(nearObject);
            }
        }
    }

    void Attack()
    {
        if (equipWeapon_L == null || equipWeapon_R == null)
            return;

        fireDelay_L += Time.deltaTime;
        isFireReady_L = equipWeapon_L.rate < fireDelay_L;


        if (fDown && isFireReady_L && !isSwap && !isJump)
        {
            equipWeapon_L.Use();
            anim.SetTrigger(equipWeapon_L.type == Weapon.Type.Punch ? "doPunch" : "doFire");
            fireDelay_L = 0;
            if (equipWeapon_L.type == Weapon.Type.Range)
                SoundManager.instance.PlayShootingEffect();
            else
                SoundManager.instance.PlayPunchEffect();
        }

        fireDelay_R += Time.deltaTime;
        isFireReady_R = equipWeapon_R.rate < fireDelay_R;

        if (fDown && isFireReady_R && !isSwap && !isJump)
        {
            equipWeapon_R.Use();
            anim.SetTrigger(equipWeapon_R.type == Weapon.Type.Punch ? "doPunch" : "doFire");
            fireDelay_R = 0;
            if (equipWeapon_R.type == Weapon.Type.Range)
                SoundManager.instance.PlayShootingEffect();
            else
                SoundManager.instance.PlayPunchEffect();
        }
    }

    void Wary()
    {
        anim.SetBool("isWary", cDown);
        if (cDown)
        {
            collid.size = new Vector3(0.35f, 0.6f, 0.6f);
            collid.center = new Vector3(0f, 0.3f, 0f);
            moveVec = new Vector3(0, 0, vAxis).normalized;

            if (vAxis == 1)
            {
                transform.position += transform.forward * moveVec.z * speed * (wDown ? -0.45f : -0.5f) * Time.deltaTime;
                anim.SetBool("isRun", wDown);
            }
            else if (vAxis == -1)
            {
                transform.position += transform.forward * moveVec.z * speed * -0.5f * Time.deltaTime;
                anim.SetBool("isRun", false);
            }
        }
        else
        {
            collid.size = new Vector3(0.35f, 0.95f, 0.6f);
            collid.center = new Vector3(0f, 0.48f, 0f);
        }
        anim.SetBool("isWalk", moveVec != Vector3.zero);
    }

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 1, LayerMask.GetMask("Wall"));
    }

    void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            anim.SetBool("doJump", false);
        isJump = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet")
        {
            if (!isDead)
            {
                if (!isDamage)
                {
                    EnemyBullet enemybullet = other.GetComponent<EnemyBullet>();
                    Health -= enemybullet.damage;
                    anim.SetTrigger("Damage");
                    StartCoroutine(OnDamage());
                }
            }

        }
        if (other.tag == "End")
        {
            SceneManager.LoadScene("Ending");
        }
    }

    IEnumerator OnDamage()
    {
        isDamage = true;

        yield return new WaitForSeconds(1f);

        isDamage = false;

        if (Health <= 0)
            OnDie();
    }

    void OnDie()
    {

        if (!isDead)
        {
            anim.SetTrigger("Death");
            isDead = true;
        }
        deathTime -= Time.deltaTime;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = null;
    }
}