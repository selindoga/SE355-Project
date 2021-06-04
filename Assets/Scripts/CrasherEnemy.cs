using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CrasherEnemy : Enemy
{
    private void Start()
   {
       Init();
       
   }
   private void Update()
   {
       LookAtPlayer();
       Move();
   }

   public void OnCollisionEnter2D(Collision2D other)
   {
       gameObject.SetActive(false);
   }

    
  

}

