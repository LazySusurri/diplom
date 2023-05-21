using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public Animator fma;
    public GameObject fm;
    public Enemy enemy;
    public SpriteRenderer enemySprite;
    public Animator enemyAnim;
    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player")
        {            
            GameManager.instance.ShowText("ֽאזלטעו F", 30, Color.white, transform.position, Vector3.zero, 0.0001f); 
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameManager.instance.SaveState();
                fma.Play("FightMenuActive");
                fma.Play("EnemyHealthActive");
                GameManager.instance.player.transform.position = new Vector3(0, -9, 0);
                GameManager.instance.player._animator.Play("IdleRight");
                GameManager.instance.RandomEnemy();
                GameManager.instance.enemy.Spawn();
                GameManager.instance.isMoving = false;

                GameManager.instance.UpdateHUD();

                GameManager.instance.EnHP.Play("EnemyHealthActive");

                GameManager.instance.mainCamera.enabled = false;
                GameManager.instance.fightCamera.enabled = true;
            }
        }
    }
}
