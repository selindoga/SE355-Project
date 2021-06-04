using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int health = 3;

    public SpaceshipWing[] LeftWings;
    public SpaceshipWing[] RightWings;
    public void Damage(int damage)
    {
        health -= damage;
    }

    private void CheckPassivityLeftWingForIncrement()
    {
        for (int i = 0; i < LeftWings.Length; i++)
        {
            if (!LeftWings[i].gameObject.activeSelf)
            {
                LeftWings[i].gameObject.SetActive(true);
                break;
            }
        }
    }
    private void CheckPassivityRightWingForIncrement()
    {
        for (int i = 0; i < RightWings.Length; i++)
        {
            if (!RightWings[i].gameObject.activeSelf)
            {
                RightWings[i].gameObject.SetActive(true);
                break;
            }
        }
    }

    
    public void CheckPassivityBothWingsForIncrement()
    {
        CheckPassivityLeftWingForIncrement();
        CheckPassivityRightWingForIncrement();
    }
}
