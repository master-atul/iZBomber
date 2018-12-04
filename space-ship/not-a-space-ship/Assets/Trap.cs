﻿using System.Collections;
using UnityEngine;

public class Trap : Photon.MonoBehaviour {

    public float floatHeight;
    public float liftForce;
    public float damping;

    private readonly WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    readonly float trapActivationTime = 1.5f;
    private LineRenderer laserLine;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        laserLine = GetComponent<LineRenderer>();

        StartCoroutine(ActivateTrap());
    }

    void FixedUpdate()
    {
       
    }

    private IEnumerator ActivateTrap()
    {

        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / trapActivationTime;
            yield return null;
        }

        // Cast a ray straight up.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        laserLine.SetPosition(0, transform.position);
        Debug.Log(hit.collider);
        // If it hits something...
        if (hit.collider != null)
        {
            Debug.Log("BOOM");
            StartCoroutine(BoomEffect());
            laserLine.SetPosition(1, hit.point);
            //PhotonNetwork.Instantiate("Trap", hit.point, Quaternion.identity, 0);
        }
        else
        {
            // laserLine.SetPosition(1, new Vector2(10,10));
            // draw maxRange?
        }
    }

    private IEnumerator BoomEffect()
    {
        laserLine.enabled = true;

        yield return shotDuration;

        laserLine.enabled = false;
        PhotonNetwork.Destroy(gameObject);
    }
}
