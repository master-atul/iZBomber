﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {
    Inventory inventory;
    LootGenerator lg;

    // Use this for initialization
    void Start () {
        inventory = GetComponent<Inventory>();
        lg = FindObjectOfType<LootGenerator>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "trap")
        {
            Destroy(target.gameObject);
            inventory.Kill();  
        } else if (target.gameObject.tag == "money")
        {
            Destroy(target.gameObject);
            lg.currentSpawns--;
            inventory.CollectMoney(10); //TODO get it from other object
        } else if (target.gameObject.tag == "powerup")
        {
            Destroy(target.gameObject);
            lg.currentSpawns--;
            inventory.ActivatePaypal();
        }
    }

}
