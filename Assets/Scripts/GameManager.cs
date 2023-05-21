using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            return;            
        }
        instance = this;
        //PlayerPrefs.DeleteAll();// deleates all saves
        SceneManager.sceneLoaded += LoadState;  
    }
    

    private void Start()
    {
        mainCamera.enabled = true;
        fightCamera.enabled = false;
        fma.Play("FightMenuHidden");
        player.transform.position = Vector3.zero;
        WeaponDamage = instance.weaponDmgArr[weaponNum];
        hitpoint = instance.player.hitpoint;
        maxHitpoint = instance.player.maxHitpoint;
        instance.UpdateHUD();
        fma.Play("EnemyHealthHidden");
        GameManager.instance.UpdateHUD();
        player.hitpoint = player.maxHitpoint;
    }
    //Resorces
    public List<int> expTable;
    public Enemy[] enemies;
    public Sprite[] weapons;
    public int[] weaponDmgArr = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200 };
    public bool isBow = false;

    //References
    public GameObject fightMenu;
    public Animator fma;
    public Player player;
    public Enemy enemy;
    public GameObject Menu;
    public GameObject HUD;
    public FloatingTextManager floatingTextManager;
    public GameObject EnemySpawner;
    public GameObject EnemyFightPoint;
    public Camera fightCamera;
    public Camera mainCamera;
    public Animator enemyAnimator;
    public Animator EnHP;


    public bool isMoving = true;

    //Logic
    public int gold;
    public int exp;
    public int hitpoint;
    public int maxHitpoint;
    public int WeaponDamage; 
    public int weaponNum;
    public int strength;
    public int agility;
    public int healPotions = 5;
    

    //Floating text
    public void ShowText(string msg, int fontSize, UnityEngine.Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // Exp System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (exp >= add)
        {
            add += expTable[r];
            r++;

            if (r == expTable.Count)
                return r;
        }
        return r;
    }
    public int GetExpToLevel(int level)
    {
        int r = 0;
        int exp = 0;

        while (r < level)
        {
            exp += expTable[r]; 
            r++;
        }
        return exp;
    }

    //Save State
    public void SaveState()
    {
        string s = "";
        s += gold.ToString() + "|";
        s += exp.ToString() + "|";
        s += player.hitpoint.ToString() + "|";
        s += player.maxHitpoint.ToString() + "|";
        s += weaponNum.ToString() + "|";
        s += healPotions.ToString() + "|";

        PlayerPrefs.SetString("SaveState", s);

       // Debug.Log("SaveState");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        gold = int.Parse(data[0]);
        exp = int.Parse(data[1]);
        player.hitpoint = int.Parse(data[2]);
        player.maxHitpoint = int.Parse(data[3]);
        weaponNum = int.Parse(data[4]);
        healPotions = int.Parse(data[5]);

        // Debug.Log("LoadState");
    }

    public void RandomEnemy()
    {
        enemy = enemies[UnityEngine.Random.Range(0, enemies.Count())];
        enemyAnimator = enemy.__animator;
    }

    public Text expText, healthText, levelText, goldText, enemyHpText;
    public RectTransform expBar, healthBar, enemyHealth;

    public void UpdateHUD()
    {
        int currLevel = instance.GetCurrentLevel();
        if (currLevel == instance.expTable.Count)
        {
            expText.text = instance.exp.ToString() + " набранного опыта";
            expBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelExp = instance.GetExpToLevel(currLevel - 1);
            int currLevelExp = instance.GetExpToLevel(currLevel);

            int diff = currLevelExp - prevLevelExp;
            int currExpIntoLevel = instance.exp - prevLevelExp;

            float completionRatio = (float)currExpIntoLevel / (float)diff;
            expBar.localScale = new Vector3(completionRatio, 1, 1);
            expText.text = currExpIntoLevel.ToString() + " / " + diff;
        }        
        float HpRatio = (float)instance.player.hitpoint / (float)instance.player.maxHitpoint;
        healthBar.localScale = new Vector3(HpRatio, 1, 1);

        float EnHpRatio = (float)instance.enemy.hitpoint / (float)instance.enemy.maxHitpoint;
        enemyHealth.localScale = new Vector3(EnHpRatio, 1, 1);

        healthText.text = instance.player.hitpoint.ToString() + " / " + instance.player.maxHitpoint.ToString();
        levelText.text = currLevel.ToString();
        goldText.text = instance.gold.ToString();
        enemyHpText.text = instance.enemy.hitpoint.ToString() + " / " + instance.enemy.maxHitpoint.ToString();
    }
}
