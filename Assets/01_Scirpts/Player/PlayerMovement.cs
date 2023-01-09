using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(-180f, 180f)] [SerializeField] private float _angle = 0f;

    [SerializeField] private float _speed = 5f;

    [SerializeField] private Vector2 _center = Vector2.zero;
    [SerializeField] private float _distance = 1.2f;
    private float _timer = 0f;

    private Rigidbody2D rb2d;

    public bool OnPlanet { get; set; } = true;

    private Planet nowPlanet;
    public Planet NowPlanet
    {
        get => nowPlanet;
        set
        {
            nowPlanet = value;
            if(nowPlanet == null)
            {
                OnPlanet = false;
                return;
            }
            _distance = nowPlanet.OnPlanetDistance;
            _center = nowPlanet.transform.position;
            OnPlanet = true;
        }
    }
    public Planet NextPlanet;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        NowPlanet = GameObject.Find("Planet").GetComponent<Planet>();
        transform.position = _center + Vector2.up * _distance;
    }

    private void Update()
    {
        if(OnPlanet)
            Revolution();
        else 
            transform.position += transform.up * Time.deltaTime * _speed * 0.02f;
    }

    private void Revolution()
    {
        float angle = 90f - _angle;
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * _distance;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * _distance;

        _timer += Time.deltaTime * _speed;
        if (OnPlanet) 
            transform.position = new Vector2(x, y) + _center;
        transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(transform.position.x, transform.position.y) * Mathf.Rad2Deg - 90f);

        _angle += Time.deltaTime * _speed;
        if (_angle >= 180f) _angle -= 360f;
        
    }
}
