using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
   // text fields
    public Text maxHPText, strengthText, agilityText, damageText, numHelPotText;

    // logic
    public Image weaponSprite;

    // update charcter information
    public void UpdateMenu()
    {
        // Weapon
        weaponSprite.sprite = GameManager.instance.weapons[GameManager.instance.weaponNum];
        // Meta
        maxHPText.text = "МаксОЗ: " + GameManager.instance.player.maxHitpoint.ToString();
        strengthText.text = "Сила: " + GameManager.instance.strength.ToString();
        agilityText.text = "Ловк: " + GameManager.instance.agility.ToString();
        damageText.text = "Урон: " + GameManager.instance.WeaponDamage.ToString();
        numHelPotText.text = GameManager.instance.healPotions.ToString();
    }
}
