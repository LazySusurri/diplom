using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Action : MonoBehaviour
{
    public GameObject FightMenu;
    public Player player;
    public Enemy enemy;
    public GameObject HUD;
    public GameObject Menu;
    public Animator fightMenuAnimator;    
    private bool isActive = false;
    private bool canAttack = true;


    private void Update()
    {
        if (!GameManager.instance.isMoving)
        {            
            enemy = GameManager.instance.enemy;
            if (enemy != null)
            {
                enemy.MyUpdate();
            }
            if (canAttack == false)
            {
                Invoke("EnemyAttack", 1);
                canAttack = true;
            }
            if (isActive == true)
            {
                Invoke("FMA", 1);
                isActive = false;
            }

            if (enemy.isDead == true)
            {
                enemy.__animator.Play("death");
                GameManager.instance.isMoving = true;
                Invoke("LoadMain", enemy.respawnTime);
                GameManager.instance.EnHP.Play("EnemyHealthHidden");
            }

            if (player.isDead)
            {
                player._animator.Play("Death");
                isActive = false;
                Invoke("PlayerDeath",1f);
                fightMenuAnimator.Play("FightMenuHidden");
                player.isDead = false;                
            }
        }
        else
        {
            fightMenuAnimator.Play("FightMenuHidden");
            GameManager.instance.EnHP.Play("EnemyHealthHidden");
        }
        GameManager.instance.WeaponDamage = GameManager.instance.weaponDmgArr[GameManager.instance.weaponNum];
    }

    public void PlayerAttack()
    {
        double attack = UnityEngine.Random.Range(0, 10);
        if (attack > 2)
        {
            Damage dmg = new Damage
            {
                damageAmount = Random.Range((int)(GameManager.instance.WeaponDamage - (0.2 * GameManager.instance.WeaponDamage)), (int)(GameManager.instance.WeaponDamage +
            (0.2 * GameManager.instance.WeaponDamage))) * (1 + (GameManager.instance.player.damageBoost / 100))
            };

            enemy.RecieveDamage(dmg);
        }
        else
            GameManager.instance.ShowText("Промах", 25, Color.white, GameManager.instance.enemy.transform.position, Vector3.up * 35, 1f);
        player._animator.Play("attack_1");
        canAttack = false;
        fightMenuAnimator.Play("FightMenuHidden");

        GameManager.instance.UpdateHUD();
    }

    public void PlayerHeal()
    {
        if (GameManager.instance.healPotions > 0)
        {
            GameManager.instance.healPotions -= 1;
            player.Heal((int)(0.3 * player.maxHitpoint + 15));
        }
        else
            GameManager.instance.ShowText("Нет зелий здоровья", 30, Color.white, player.transform.position, Vector3.up * 25, 3f);

        GameManager.instance.UpdateHUD();
    }

    public void PlayerRun()
    {
        double run = UnityEngine.Random.Range(0, 10);
        //Debug.Log(run);
        if (run >= 4) 
        {
            GameManager.instance.enemy.Respawn();

            player.transform.position = new Vector3(0, 5.5f, 0);            

            GameManager.instance.fightCamera.enabled = false;
            GameManager.instance.mainCamera.enabled = true;

            GameManager.instance.healPotions = 5;

            GameManager.instance.isMoving = true;

            isActive = true;
            canAttack = true;

            fightMenuAnimator.Play("FightMenuHidden");

            GameManager.instance.ShowText("Вы смогли сбежать", 30, Color.black, player.transform.position, Vector3.up * 25, 2f);

            GameManager.instance.SaveState();
        }
        else
        {
            canAttack = false;
            fightMenuAnimator.Play("FightMenuHidden");
        }

        GameManager.instance.UpdateHUD();
    }

    public void PlayerDeath()
    {
        Invoke("FMH",0.01f);

        GameManager.instance.gold -= (int)(0.05 * GameManager.instance.gold);
        GameManager.instance.exp -= (int)(0.05 * GameManager.instance.exp);

        player.transform.position = Vector3.zero;

        GameManager.instance.enemy.Respawn();

        GameManager.instance.fightCamera.enabled = false;
        GameManager.instance.mainCamera.enabled = true;

        GameManager.instance.healPotions = 5;

        GameManager.instance.isMoving = true;

        GameManager.instance.EnHP.Play("EnemyHealthHidden");

        player.isDead = false;
        isActive = false;
        canAttack = true;

        player.Heal(player.maxHitpoint);
        player._animator.Play("IdleRight");

        GameManager.instance.SaveState();

        GameManager.instance.UpdateHUD();
    }

    public void EnemyAttack()
    {
        double attack = UnityEngine.Random.Range((GameManager.instance.player.evadeChance / 100), 10);
        if (attack <= 8)
        {
            if (enemy.isDead == false)
            {                
                Damage dmg = new Damage
                {
                    damageAmount = Random.Range((int)(enemy.damage - (0.2 * enemy.damage)), (int)(enemy.damage + (0.2 * enemy.damage)))
                };
                player.RecieveDamage(dmg);                
            }
        }
        else
            GameManager.instance.ShowText("Промах", 25, Color.white, GameManager.instance.player.transform.position, Vector3.up * 35, 1f);

        enemy.Attack();
        isActive = true;

        GameManager.instance.UpdateHUD();
    }

    private void FMA()
    {
        fightMenuAnimator.Play("FightMenuActive");
    }

    private void FMH()
    {
        fightMenuAnimator.Play("FightMenuHidden");
    }

    private void LoadMain()
    {
        player.transform.position = new Vector3(0, 5.5f, 0);
        GameManager.instance.exp += enemy.expValue;
        GameManager.instance.gold += enemy.goldAmount;
        GameManager.instance.enemy.Respawn();

        GameManager.instance.fightCamera.enabled = false;
        GameManager.instance.mainCamera.enabled = true;
        GameManager.instance.ShowText("+" + enemy.expValue.ToString() + " опыта", 30, new Color(185, 141, 36), player.transform.position, Vector3.up * 30, 1f);
        Invoke("GoldShow",1f);
        GameManager.instance.healPotions = 5;

        GameManager.instance.EnHP.Play("EnemyHealthHidden");

        GameManager.instance.SaveState();

        GameManager.instance.UpdateHUD();
    }

    private void GoldShow()
    {        
        GameManager.instance.ShowText("+" + enemy.goldAmount.ToString() + " золота", 30, Color.yellow, player.transform.position, Vector3.up* 30, 1f);

        GameManager.instance.UpdateHUD();
    }

    private void Resp()
    {
        enemy.Respawn();
    }
}
