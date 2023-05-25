using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : Collidable
{
    public Animator ShopMenuAnnimator;
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            GameManager.instance.ShowText("ֽאזלטעו F", 35, Color.white, transform.position, Vector3.zero, 0.001f);
            if (Input.GetKeyDown(KeyCode.F))
            {
                ShopMenuAnnimator.Play("ShopMenu_active");                
            }
        }
    }
}