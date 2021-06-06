using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 0.5f;
    public float rotation_speed = 1f;
    public float min_player_distance;
    public float direction_calculation_cooldown;
    public float mass = 10f;
    public float slowing_down_rate = 10f;
    public Transform player;

    protected float player_distance;
    protected Vector2 player_direction;
    protected Rigidbody2D rgdbody;

    private const float rotation_speed_multiplier = 100f;
    private WaitForSeconds direction_calculation_time;

    protected void Init()
    {
        rgdbody = GetComponent<Rigidbody2D>();
        direction_calculation_time = new WaitForSeconds(direction_calculation_cooldown);

        rgdbody.freezeRotation = true;
        rgdbody.mass = mass;
        rgdbody.drag = slowing_down_rate;

        StartCoroutine(CalculatePlayerDirection());
    }

    protected void Move()
    {
        player_distance = Vector2.Distance(transform.position, player.position);

        if (player_distance > min_player_distance)
        {
            rgdbody.AddForce(speed * transform.right, ForceMode2D.Impulse);
        }
    }

    protected void LookAtPlayer()
    {
        float angle = Mathf.Atan2(player_direction.y, player_direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, (rotation_speed * rotation_speed_multiplier * Time.deltaTime));
    }

    private IEnumerator CalculatePlayerDirection()
    {
        while (true)
        {
            player_direction = (player.position - transform.position).normalized;

            yield return direction_calculation_time;
        }
    }
}
