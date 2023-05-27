using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    protected override void Start()
    {
        base.Start();
    }

    public override void MyUpdate()
    {
        base.MyUpdate();
    }

    public override void Attack()
    {
        base.Attack();
    }
    public override void RecieveDamage(Damage dmg)
    {
        base.RecieveDamage(dmg);
    }

    public override void Death()
    {
        base.Death();
        Invoke("DestroyBoss",1f);
    }

    public void DestroyBoss()
    {
        Destroy(gameObject);
    }    
}
