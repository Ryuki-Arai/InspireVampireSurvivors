using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IObjectPool
{
    [SerializeField] float _speed = 10;
    SpriteRenderer _image;
    Rigidbody2D _rb2d;
    Animator _anim = default;
    
    void Awake()
    {
        _image = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!IsActive) return;

        Vector2 sub = GameManager.Player.transform.position - transform.position;
        //sub.Normalize();
        //transform.position += sub * _speed * Time.deltaTime;
        _rb2d.velocity += sub.normalized * _speed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (_anim)
        {
            Vector2 vector = _rb2d.velocity;
            _anim.SetFloat("VectorX", vector.x);
            _anim.SetFloat("VectorY", vector.y);
        }
    }

    public void Damage()
    {
        //TODO
        Destroy();
    }

    //ObjectPool
    bool _isActrive = false;
    public bool IsActive => _isActrive;
    public void DisactiveForInstantiate()
    {
        _image.enabled = false;
        _isActrive = false;
    }
    public void Create()
    {
        _image.enabled = true;
        _isActrive = true;
    }
    public void Destroy()
    {
        _image.enabled = false;
        _isActrive = false;
    }
}
