﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramming : MonoBehaviour
{

    [SerializeField] float bumpForce = 10f;
    [SerializeField] float bumpExplosionRaduis = 2f;
    [SerializeField] float bumpDamage = 200f;

    float newSpeedForce;

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetType() != typeof(SphereCollider) && other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
        {
            Debug.Log("entered collision");
            Rigidbody otherRb = other.gameObject.GetComponentInParent<Rigidbody>();

            if (!otherRb)
            {
                return;
            }

            if (SpeedCollisionCheck(rb, otherRb))
            {
                Debug.Log("doing collision check: " + rb.gameObject.name);

                Vector3 extraForce = rb.gameObject.transform.forward * 3;

                extraForce += Vector3.up * 2;

                Vector3 extraRot = rb.gameObject.transform.right * 45;

                if (otherRb.GetComponent<Enemy>())
                {
                    otherRb.gameObject.GetComponent<Enemy>().extraForce = extraForce;
                    otherRb.gameObject.GetComponent<Enemy>().extraRotation = extraRot;
                }
                if (otherRb.GetComponent<PlayerController>())
                {
                    otherRb.gameObject.GetComponent<PlayerController>().extraForce = extraForce;
                    otherRb.gameObject.GetComponent<PlayerController>().extraRotation = extraRot;
                }

                other.GetComponentInParent<Health>().DealDamage(bumpDamage);
            }
        }
    }

    bool SpeedCollisionCheck(Rigidbody player, Rigidbody enemy)
    {

        var speedDifference = player.velocity.sqrMagnitude - enemy.velocity.sqrMagnitude;

        return speedDifference >= 0;
    }
}
