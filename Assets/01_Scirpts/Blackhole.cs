using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    [SerializeField] private float activeArea = 5f;
    [SerializeField] private float gravity = 2f;

    private PlayerMovement player;
    private Rigidbody2D playerRigid;

    private void Start()
    {
        player = GameManager.Instance.Player;
        playerRigid = player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PullPlayer();
    }

    private void PullPlayer()
    {
        if(Vector2.Distance(transform.position, player.transform.position) <= activeArea)
        {
            playerRigid.velocity = (transform.position - player.transform.position).normalized * gravity;
        }
    }
}
