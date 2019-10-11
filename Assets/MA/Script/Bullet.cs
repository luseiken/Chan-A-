﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed;
    public float MaxDistance;

    private GameObject triggeringEnemy;

    public float Damage;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * BulletSpeed);
        MaxDistance += 1 * Time.deltaTime;
        if (MaxDistance >= 3)
            Destroy(this.gameObject);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<EnemyControl>().Health -= Damage;
            Destroy(this.gameObject);
        }

    }
}
