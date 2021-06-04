using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectBonus : MonoBehaviour
{
    public float Score = 0f;
    public float speed = 0.3f;
    public float lifeTime = 20f;
    void Start()
    {
        // transform.position = new Vector3(Random.Range(-2.3f, 2.3f), Random.Range(4.5f, 4.6f), transform.position.z);
        
    }

    private void Update()
    {
        transform.Translate(Vector2.down * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))   
        {
            
            Debug.Log("bonus toplandı");
            other.GetComponent<PlayerData>().CheckPassivityBothWingsForIncrement();
            Destroy(gameObject);
        }
    }

    void CheckLifeTime()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
