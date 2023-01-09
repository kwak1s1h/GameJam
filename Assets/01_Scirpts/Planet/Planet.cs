using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float OnPlanetDistance;
    public float Gravity;
    public bool OnLeft = false;

    [SerializeField] private float _gravityArea = 3f;

    private PlayerMovement player;

    private void Start()
    {
        player = GameManager.Instance.Player;
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
            player.NextPlanet = null;
        }
    }

    private void ApplyGravity()
    {
        
        if(Vector2.Distance(transform.position, player.transform.position) <= _gravityArea && player.NextPlanet == this && !player.OnPlanet)
        {
            player.GetComponent<Rigidbody2D>().velocity = (transform.position - player.transform.position) * Gravity;
        }
        else player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
