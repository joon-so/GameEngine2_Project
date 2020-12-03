using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);

        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Wall")
    //    {
    //        Destroy(gameObject);

    //    }
    //}
}