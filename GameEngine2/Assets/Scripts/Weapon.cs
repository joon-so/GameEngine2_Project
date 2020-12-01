using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Punch, Range };
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider punchArea;
    public TrailRenderer trailEffect;

    public Transform bulletPos;
    public GameObject bullet;

    public void Use()
    {
        if (type == Type.Punch)
        {
            StopCoroutine("Hit");
            StartCoroutine("Hit");
        }
        if (type == Type.Range)
        {
            StartCoroutine("Shot");
        }
    }

    IEnumerator Hit()
    {
        yield return new WaitForSeconds(0.1f);
        punchArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        punchArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }

    IEnumerator Shot()
    {
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;
        yield return null;
    }
}