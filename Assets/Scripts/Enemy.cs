using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    public int expValue = 5;
    public int goldAmount = 10;
    public int damage = 3;
    public Animator __animator;
    public bool isDead;
    public SpriteRenderer spriteRenderer;
    public float respawnTime = 1.5f;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        __animator = GetComponent<Animator>();
        isDead = false;
    }
    public virtual void MyUpdate()
    {
        if (!isDead)
            return;
    }

    public override void RecieveDamage(Damage dmg)
    {
        base.RecieveDamage(dmg);
        __animator.Play("damaged");
        __animator.Play("spawn");
    }

    public override void Death()
    {
        isDead = true;
        __animator.Play("death");
    }

    public void Respawn()
    {
        transform.position = GameManager.instance.EnemySpawner.transform.position;
        hitpoint = maxHitpoint;
        __animator.Play("spawn");
    }

    public void Spawn()
    {
        isDead = false;
        transform.position = GameManager.instance.EnemyFightPoint.transform.position;
        __animator.Play("spawn");
    }
    public virtual void Attack()
    {
        __animator.Play("attack");
    }

}
