using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour, IObjectPool
{
    [SerializeField] float _speed = 255;
    SpriteRenderer _image;
    Enemy _target;
    Vector3 _shootVec;

    float _timer = 0.0f;

    void Awake()
    {
        _image = GetComponent<SpriteRenderer>();
    }

    public void Shoot()
    {
        var list = GameManager.EnemyList;
        _target = null;
        float len = -1;
        Vector3 vec;
        foreach(var e in list)
        {
            if (!e.IsActive) continue;
            vec = e.transform.position - GameManager.Player.transform.position;
            if(len == -1 || vec.magnitude < len)
            {
                _target = e;
                len = vec.magnitude;
            }
        }

        if (_target == null) return;
        _shootVec = _target.transform.position - GameManager.Player.transform.position;
        _shootVec.Normalize();
    }

    void Update()
    {
        transform.position += _shootVec * _speed * Time.deltaTime;

        var list = GameManager.EnemyList;
        _target = null;
        Vector3 vec;
        foreach (var e in list)
        {
            if (!e.IsActive) continue;

            vec = e.transform.position - this.transform.position;
            if (vec.magnitude < 1.0f)
            {
                e.Damage();
                _speed = 0;
                break;
            }
        }

        _timer += Time.deltaTime;
        if(_timer > 3.0f)
        {
            Destroy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            var _anm = GetComponent<Animator>();
            _anm.SetTrigger("Hit");
        }        
    }

    void Delete()
    {
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
        _timer = 0.0f;
        gameObject.SetActive(true);
        _isActrive = true;
    }
    public void Destroy()
    {
        gameObject.SetActive(false);
        _isActrive = false;
    }
}
