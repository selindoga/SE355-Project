using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public int damage = 1;
    public float move_speed = 0.8f;
    public float life_time = 10f;

    private float life_duration;
    private Vector2 velocity;
    private Rigidbody2D rigidb;

    void Start()
    {
        rigidb = GetComponent<Rigidbody2D>();
        SetInitialVelocity();
    }

    void OnEnable()
    {
        life_duration = life_time;
        if (rigidb != null)
            SetInitialVelocity();
    }

    void Update()
    {
        CheckLifeDuration();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))    //used collider to just get the object that collided
        {
            other.gameObject.GetComponent<PlayerData>().Damage(damage);
        }

        gameObject.SetActive(false);
    }

    private void CheckLifeDuration()
    {
        life_duration -= Time.deltaTime;
        if (life_duration <= 0)
            gameObject.SetActive(false);
    }

    private void SetInitialVelocity()
    {
        velocity = move_speed * Vector2.down;
        rigidb.velocity = velocity;
    }
}
