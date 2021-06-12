using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public int damage = 1;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (player != null)
        {
            LookAtPlayer();
            Move();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.collider.GetComponent<PlayerData>().Damage(damage);
        }

        health.Damage(damage);
    }
}
