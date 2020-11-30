using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{
    // Enemy 상태 상수
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    // Enemy 상태 변수
    EnemyState enemyState;

    GameObject player;
    
    CharacterController cc;
    Animator anim;

    public float findDistance = 8f;
    public float attackDistance = 2f;
    public float moveSpeed = 5f;
    public float attackDelayTime = 2f;
    float currentTime = 0;
    public int attackPower = 2;
    public int maxHp = 5;
    int currentHp;
    float attackCooltime = 1.0f;
    // 초기 위치 저장
    Vector3 originPos;
    // 이동 가능한 거리
    public float moveDistance = 20f;

    public Slider hpSlider;


    void Start()
    {
        enemyState = EnemyState.Idle;
        player = GameObject.Find("Player");
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        originPos = transform.position;
        currentHp = maxHp;
    }

    void Update()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;
        }

        //hpSlider.value = (float)currentHp / (float)maxHp;
    }

    void Idle()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= findDistance)
        {
            enemyState = EnemyState.Move;
        }
    }

    void Move()
    {
        if (Vector3.Distance(originPos, transform.position) > moveDistance)
        {
            anim.SetBool("FindPlayer", true);
            enemyState = EnemyState.Return;
        }

        else if (Vector3.Distance(player.transform.position, transform.position) > attackDistance)
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            anim.SetBool("FindPlayer", true);

            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            enemyState = EnemyState.Attack;
            currentTime = attackDelayTime;
            anim.SetBool("FindPlayer", true);
        }
    }

    void Attack()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= attackDistance)
        {
    
            if (currentTime >= attackDelayTime)
            {
                currentTime = 0;
                Player pm = player.GetComponent<Player>();
                //anim.SetTrigger("AttackPlayer");
                //pm.OnDamage(attackPower);
            }
            else
                currentTime += Time.deltaTime;
        }
        else
        {
            enemyState = EnemyState.Move;
        }
    }

    void Return()
    {
        if (Vector3.Distance(originPos, transform.position) > 0.1)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = originPos;
            enemyState = EnemyState.Idle;
            currentHp = maxHp;
        }
    }

    void Damaged()
    {
        StartCoroutine(DamageProcess());
    }

    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(2f);
        enemyState = EnemyState.Move;
    }

    void Die()
    {
        StopAllCoroutines();
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        cc.enabled = false;

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public void HitEnemy(int value)
    {
        if (enemyState == EnemyState.Damaged || enemyState == EnemyState.Die || enemyState == EnemyState.Return)
        {
            return;
        }
        currentHp -= value;

        if (currentHp > 0)
        {
            enemyState = EnemyState.Damaged;
            Damaged();
        }
        else
        {
            enemyState = EnemyState.Die;
            Die();
        }
    }

}
