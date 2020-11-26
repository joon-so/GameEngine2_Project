using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    AudioSource aSource;
    
    // 발사 위치
    public Transform firePosition;
    public int attackPower = 2;

    // Start is called before the first frame update
    void Start()
    {
        firePosition = GetComponent<Transform>();
        //ps = bulletEffect.GetComponent<ParticleSystem>();
        //aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Raycast(ray, out hitInfo))
            {
                print(hitInfo.transform.name);

                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    eFSM.HitEnemy(attackPower);
                }
                //bulletEffect.transform.position = hitInfo.point;
                //bulletEffect.transform.foward = hitInfo.normal;
                //ps.Play();
            }
        }
        //aSource.Play();
    }
}
