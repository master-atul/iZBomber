﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Photon.MonoBehaviour {
    public float money;
    public float noOfContactless;
    public float gdprCount;
    public float ecommCount;

    private GameController gc;
    private PlayerControls pc;
    private readonly float powerupDuration = 7f;

    private void SetValues()
    {
        if (GetComponent<PhotonView>().isMine)
        {
            gc.SetMoney(money);
            gc.SetGDPRCount(gdprCount);
            gc.SetEcommCount(ecommCount);
            gc.SetContactLessCount(ecommCount);
        }
    }

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
        pc = gameObject.GetComponent<PlayerControls>();
        SetValues();
    }

     public void Kill()
    {
        if (GetComponent<PhotonView>().isMine)
        {
            PhotonNetwork.Destroy(gameObject);
             gc.SetLobbyActive(true);
        }
    }

    [PunRPC]
    public void CollectMoney(float amount)
    {
        money += amount;
        SetValues();
    }

    [PunRPC]
    public void ActivatePaypal()
    {
        money += 100;
        StartCoroutine(TempEffect());
        SetValues();
    }

    private IEnumerator TempEffect()
    {
        pc.speed = 14f;
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / powerupDuration;
            yield return null;
        }
        pc.speed = 7f;
    }
}
