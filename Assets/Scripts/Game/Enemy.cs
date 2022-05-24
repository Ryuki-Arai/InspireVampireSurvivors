using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IObjectPool
{
    [SerializeField] float _speed = 10;
    
    Rigidbody2D _rb2d;
    Animator _anim = default;
    
    void Awake()
    {
        
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
        Vector2 verocity = sub.normalized * Time.deltaTime;
        _rb2d.velocity += verocity * _speed;
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
        gameObject.SetActive(false);
        _isActrive = false;
    }
    public void Create()
    {
        gameObject.SetActive(true);
        _isActrive = true;
    }
    public void Destroy()
    {
        gameObject.SetActive(false);
        _isActrive = false;
    }
}
