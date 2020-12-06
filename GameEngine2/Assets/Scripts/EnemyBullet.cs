using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage;

    Player player;

    //private void Awake()
    //{
    //    player = GameObject.Find("Player").GetComponent<Player>();
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Floor")
    //    {
    //        Destroy(gameObject);
    //    }
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        player.hp -= damage;
    //        Debug.Log("1");
    //        Destroy(gameObject);
    //    }
    //}
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);

        }
    }
}