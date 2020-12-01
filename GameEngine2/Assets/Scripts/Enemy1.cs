using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public float Hp = 100f;
    float playerDistance;
    float detectDistance = 8f;
    float attackDistance = 6f;

    Vector3 moveVec;

    GameObject player;
    Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        playerDistance = Vector3.Distance(player.transform.position, transform.position);
        anim.SetFloat("PlayerDistance", playerDistance);

        if (playerDistance > attackDistance && playerDistance < detectDistance)
        {
            moveVec = (player.transform.position - transform.position).normalized;
            transform.position += moveVec * speed * Time.deltaTime;
            transform.LookAt(transform.position + moveVec);
        }

        else if (playerDistance < attackDistance )
        {
            moveVec = (player.transform.position - transform.position).normalized;
            transform.LookAt(transform.position + moveVec);
        }
    }
}
