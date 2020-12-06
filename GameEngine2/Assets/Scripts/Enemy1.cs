using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1 : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    float shootCooltime = 1.5f;
    float playerDistance;
    float detectDistance = 8f;
    float attackDistance = 6f;
    bool death = false;

    public int maxHealth;
    int curHealth;

    Vector3 moveVec;

    GameObject player;
    Animator anim;

    public Transform bulletPos;
    public GameObject bullet;

    Rigidbody rigid;

    public Slider hpSlider;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();

        anim = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player");
    }

    void Start()
    {
        curHealth = maxHealth;
    }

    void Update()
    {
        if (curHealth > 0)
        {

            playerDistance = Vector3.Distance(player.transform.position, transform.position);
            anim.SetFloat("PlayerDistance", playerDistance);

            if (playerDistance > attackDistance && playerDistance < detectDistance)
            {
                moveVec = (player.transform.position - transform.position).normalized;
                transform.position += moveVec * speed * Time.deltaTime;
                transform.LookAt(transform.position + moveVec);
            }

            else if (playerDistance < attackDistance)
            {
                moveVec = (player.transform.position - transform.position).normalized;
                transform.LookAt(transform.position + moveVec);

                shootCooltime -= Time.deltaTime;
                if (shootCooltime < 0)
                {
                    GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
                    Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
                    bulletRigid.velocity = bulletPos.forward * 50;
                    SoundManager.instance.PlayShootingEffect();

                    shootCooltime = 1.5f;
                }
            }

        }
        else
        {
            if (!death)
            {
                anim.SetTrigger("Death");
                death = true;
            }
        }
        hpSlider.value = (float)curHealth / (float)maxHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Punch")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage;
            Vector3 reactVec = transform.position - other.transform.position;

            StartCoroutine(OnDamage(reactVec));
        }
        else if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            curHealth -= bullet.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            Destroy(other.gameObject);

            StartCoroutine(OnDamage(reactVec));
        }
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        yield return new WaitForSeconds(0.1f);

        if (curHealth < 0)
        {
            gameObject.layer = 11;

            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);
            Destroy(gameObject, 2);
        }
    }
}
