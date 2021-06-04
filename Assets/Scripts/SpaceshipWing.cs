using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipWing : MonoBehaviour
{
    public bool CollisionMade;
    public int Damage;
    public Collision Enemy;
    public SpaceshipWing[] further_wings;
    

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.isStatic)
        {
            foreach (SpaceshipWing wing in further_wings)
            {
                wing.gameObject.SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }

    
    
}
