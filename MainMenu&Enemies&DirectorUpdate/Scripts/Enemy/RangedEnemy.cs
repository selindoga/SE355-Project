using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public float shooting_cooldown = 2f;
    public float max_shooting_distance = 5f;
    public GameObject bullet;
    public Transform bullet_spawn_point;

    private bool shooting_ready = true;
    private WaitForSeconds shooting_cooldown_time;

    void Start()
    {
        Init();
        shooting_cooldown_time = new WaitForSeconds(shooting_cooldown);
    }

    void Update()
    {
        if (player != null)
        {
            LookAtPlayer();
            Move();

            if (shooting_ready && player_distance <= max_shooting_distance)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot");

        Instantiate(bullet, bullet_spawn_point.position, transform.rotation);

        StartCoroutine(ShootingWait());
    }

    private IEnumerator ShootingWait()
    {
        shooting_ready = false;

        yield return shooting_cooldown_time;

        shooting_ready = true;
    }
}
