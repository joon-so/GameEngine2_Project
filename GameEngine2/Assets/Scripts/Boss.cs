﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public float hp = 100f;
    float shootCooltime = 1.5f;
    float playerDistance;
    float detectDistance = 10f;
    float attackDistance = 8f;
    bool death = false;

    Vector3 moveVec;

    GameObject player;
    Animator anim;
    public Transform bulletPos;
    public GameObject bullet;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (hp > 0)
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
    }
}
