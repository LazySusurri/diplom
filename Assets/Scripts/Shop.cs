using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : Collidable
{
    private bool active = false;
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            GameManager.instance.ShowText("ֽאזלטעו F", 35, Color.white, transform.position, Vector3.zero, 0.001f);
            if (Input.GetKeyDown(KeyCode.F) && !active)
            {
                active = true;
                Debug.Log("Opening Shop");                
            }
        }
    }
}