using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController cc;

    public float moveSpeed = 7f;
    float rotateSpeed = 140f;
    public float jumpPower = 1f;
    public float yVelocity = 0;
    float gravity = -20f;
    public int maxJump = 1;
    int jumpCount = 0;


    public int hp = 10;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        if (cc.collisionFlags == CollisionFlags.Below)
        {
            jumpCount = 0;
            yVelocity = 0;
        }

        if (Input.GetButtonDown("Jump") && jumpCount < maxJump)
        {
            jumpCount++;
            yVelocity = jumpPower;
        }

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        transform.Rotate(new Vector3(0, h, 0) * rotateSpeed * Time.deltaTime);
        cc.Move(transform.forward * dir.z * moveSpeed * Time.deltaTime);
        cc.Move(transform.up * dir.y * moveSpeed * Time.deltaTime);
    }

    public void OnDamage(int value)
    {
        hp -= value;
        if (hp<0)
        {
            hp = 0;
        }
    }
}
