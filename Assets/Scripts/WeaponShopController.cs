using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShopController : MonoBehaviour
{

    public static int selectedWeapon;
    public int wNumber;
    public int price;
    public Text btnText;

    public Image buySkin;

    public Image[] weapons;
    private void Start()
    {
        if (PlayerPrefs.GetInt("Weapon0" + "buy") == 0)
        {
            foreach (Image img in weapons)
            {
                if ("Weapon0" == img.name)
                {
                    PlayerPrefs.SetInt("Weapon0" + "buy", 1);
                    btnText.text = "Выбрано";
                }
                else
                    PlayerPrefs.SetInt(GetComponent<Image>().name + "buy", 0);
            }            
        }
    }

    private void Update()
    {
        if(PlayerPrefs.GetInt(GetComponent<Image>().name + "buy") == 0)
        {
            btnText.text = price.ToString();
        }
        else if(PlayerPrefs.GetInt(GetComponent<Image>().name + "buy") == 1)
        {
            if(PlayerPrefs.GetInt(GetComponent<Image>().name + "equip") == 1)
            {
                btnText.text = "Выбрано";
                btnText.color = new Color(144, 144, 144);
            }
            else if(PlayerPrefs.GetInt(GetComponent<Image>().name + "equip") == 0)
            {
                btnText.text = "Выбрать";
                btnText.color = Color.white;
            }
        }
    }

    public void Buy()
    {
        if(PlayerPrefs.GetInt(GetComponent<Image>().name + "buy") == 0)
        {
            if (GameManager.instance.gold < price)
            {
                GameManager.instance.ShowText("Недостаточно золота", 30, Color.white, GameManager.instance.player.transform.position, UnityEngine.Vector3.up * 30, 1f);
                return;
            }
            else
            {
                btnText.text = "Выбрано";
                btnText.color = new Color(144, 144, 144);
                GameManager.instance.gold -= price;
                PlayerPrefs.SetInt(GetComponent<Image>().name + "buy", 1);
                GameManager.instance.weaponNum = wNumber;
                GameManager.instance.UpdateHUD();
            }

            GameManager.instance.SaveState();

            foreach (Image img in weapons)
            {
                if (GetComponent<Image>().name == img.name)
                {
                    PlayerPrefs.SetInt(GetComponent<Image>().name + "equip", 1);
                }
                else
                {
                    PlayerPrefs.SetInt(img.name + "equip", 0);
                }
            }
        }
        else if (PlayerPrefs.GetInt(GetComponent<Image>().name + "buy") == 1)
        {
            btnText.text = "Выбрано";
            btnText.color = new Color(144, 144, 144);
            GameManager.instance.weaponNum = wNumber;
            GameManager.instance.UpdateHUD();
            GameManager.instance.SaveState();

            foreach (Image img in weapons)
            {
                if (GetComponent<Image>().name == img.name)
                {
                    PlayerPrefs.SetInt(GetComponent<Image>().name + "equip", 1);
                }
                else
                {
                    PlayerPrefs.SetInt(img.name + "equip", 0);
                }
            }
        }
    }
}
