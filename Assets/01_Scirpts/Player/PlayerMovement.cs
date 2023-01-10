using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(-180f, 180f)] [SerializeField] private float _angle = 0f;

    public float speed = 150f;

    [SerializeField] private Vector2 _center = Vector2.zero;
    [SerializeField] private float _distance = 1.2f;
    private float _timer = 0f;
    private float angleIncreas = 0f;

    private Rigidbody2D rb2d;

    private bool onPlanet = true;
    public bool OnPlanet 
    { 
        get => onPlanet; 
        set
        {
            onPlanet = value;
            if(true)
            {

            }
        } 
    }

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
            this.NextPlanet = value.NextPlanet;
            OnPlanet = true;
            Vector2 pos = (Vector2)transform.position - _center;
            _angle = value.OnLeft ? -Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg + 90f : -Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg + 90f;
            if(angleIncreas <= 390f)
                ComboSystem.Instance.AddCombo();
            else if( angleIncreas > 390f)
                ComboSystem.Instance.ResetCombo();
            angleIncreas = 0;
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
            transform.position += transform.up * Time.deltaTime * speed * 0.042f;
    }

    private void Revolution()
    {
        float angle = 90f - _angle;
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * _distance;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * _distance;

        _timer += Time.deltaTime * speed;
        if (OnPlanet) 
            transform.position = new Vector2(x, y) + _center;
        transform.rotation = nowPlanet.OnLeft ? Quaternion.Euler(0, 0, -Mathf.Atan2(_center.x - transform.position.x, _center.y - transform.position.y) * Mathf.Rad2Deg - 90f)
         : Quaternion.Euler(0, 0, -Mathf.Atan2(transform.position.x - _center.x, transform.position.y - _center.y) * Mathf.Rad2Deg - 90f);

        _angle += (nowPlanet.OnLeft ? -Time.deltaTime * speed : Time.deltaTime * speed) + (!nowPlanet.OnLeft ? (_angle >= 180f ? -360f : 0) : (_angle <= -180f ? 360f : 0));
        if (_angle >= 180f) _angle -= 360f;
        if(_angle <= -180f) _angle += 360f;

        angleIncreas += Mathf.Abs(nowPlanet.OnLeft ? -Time.deltaTime * speed : Time.deltaTime * speed) + (!nowPlanet.OnLeft ? (_angle >= 180f ? -360f : 0) : (_angle <= -180f ? 360f : 0));
    }
}
