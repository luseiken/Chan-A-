using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float Health;
    public float LookRadius = 10f;
    private Rigidbody MyRB;
    public float EnemyMoveSpeed;
    public Biology ThePlayer;
    void Start()
    {
        MyRB = GetComponent<Rigidbody>();
        ThePlayer = FindObjectOfType<Biology>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(ThePlayer.transform.position);
        if (Health <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(ThePlayer.transform.position, transform.position) < LookRadius)
            MyRB.velocity = (transform.forward * EnemyMoveSpeed);
    }
    private void Die()
    {
        DestroyImmediate(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
    }
}
