using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float OnPlanetDistance = 3.15f;
    public float Gravity = 0.1f;
    public bool OnLeft = false;

    public Planet NextPlanet = null;

    [SerializeField] private float _gravityArea = 6f;

    private PlayerMovement player;
    private Rigidbody2D playerRigid;
    [SerializeField] private float radius = 1.5f;

    private void Start()
    {
        player = GameManager.Instance.Player;
        playerRigid = player.GetComponent<Rigidbody2D>();

        transform.localScale = Vector3.one * (radius * 2f);
        OnPlanetDistance = (radius * 2f) + (radius * 2f) * 0.05f;
        _gravityArea = radius * 4f;
        Gravity = 1 + (radius - 1.5f) * 0.5f;
    }
    
    private void Update()
    {
        ApplyGravity();
        CheckInner();
    }

    private void CheckInner()
    {
        if(Vector2.Distance(transform.position, player.transform.position) <= OnPlanetDistance && player.NextPlanet == this)
        {
            player.NowPlanet = this;
        }
    }

    private void ApplyGravity()
    {
        if(Vector2.Distance(transform.position, player.transform.position) <= _gravityArea && player.NextPlanet == this && !player.OnPlanet)
        {
            playerRigid.velocity = (Vector2)(transform.position - player.transform.position).normalized * Gravity;
        }
        if(Vector2.Distance(transform.position, player.transform.position) > _gravityArea && player.NextPlanet == this)
        {
            playerRigid.velocity = Vector2.zero;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, OnPlanetDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _gravityArea);
            Gizmos.color = Color.white;
        }
    }
#endif
}
