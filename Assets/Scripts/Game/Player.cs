using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 0.1f;
    [SerializeField] float _shootTime = 0.3f;
    [SerializeField] Bullet _prefab = null;
    [SerializeField] Transform _root = null;
    Rigidbody2D _rb2d;
    Animator _anim = default;
    Vector2 verocity;

    float _timer = 0.0f;

    ObjectPool<Bullet> _bulletPool = new ObjectPool<Bullet>();

    void Awake()
    {
        GameManager.Instance.SetPlayer(this);
    }

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _bulletPool.SetBaseObj(_prefab, _root);
        _bulletPool.SetCapacity(100);
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 dir = new Vector2(h, v);
        verocity = dir.normalized * _speed;
        _rb2d.velocity = verocity;

        //transform.position += new Vector3(h * _speed * Time.deltaTime, v * _speed * Time.deltaTime, 0);

        _timer += Time.deltaTime;
        if (_timer > _shootTime)
        {
            var script = _bulletPool.Instantiate();
            script.transform.position = this.transform.position;
            script.Shoot();
            _timer -= _shootTime;
        }
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
}
