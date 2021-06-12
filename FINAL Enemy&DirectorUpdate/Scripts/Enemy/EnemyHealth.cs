using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 1;

    private Collider2D coll;
    private Enemy enemy;
    private Animator animator;

    void Start()
    {
        coll = GetComponent<Collider2D>();
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DeathState();
        }
    }

    private void DeathState()
    {
        if (enemy.enabled)
        {
            UserInterface.Instance.AddScore(1);

            coll.enabled = false;
            enemy.enabled = false;

            animator.SetTrigger("Death");
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
