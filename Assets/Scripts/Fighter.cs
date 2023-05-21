using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hitpoint = 100;
    public int maxHitpoint = 100;

    public virtual void RecieveDamage(Damage dmg)
    {
        Vector3 pos = transform.position;
        hitpoint -= dmg.damageAmount;
        pos.y += 0.2f;
        GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, pos, Vector3.up * 35, 1f);

        if (hitpoint <= 0)
        {
            hitpoint = 0;
            Death();
        }
    }

    public virtual void Death()
    {
        Debug.Log(gameObject.name + " DEAD");
    }

    

}
