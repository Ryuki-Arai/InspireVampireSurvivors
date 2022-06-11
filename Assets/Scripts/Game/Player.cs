using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 0.1f;
    [SerializeField] float _shootTime = 0.3f;
    [SerializeField] Bullet _prefab = default;
    [SerializeField] Transform _root = default;
    [SerializeField] Slider _EXPslider = default;
    [SerializeField] Slider _HPslider = default;
    [SerializeField] TextMeshProUGUI _levelText;
    [SerializeField] Status _init_val;
    [SerializeField] GameObject LevelUpPanel;
    Status _update_val;
    Rigidbody2D _rb2d;
    Animator _anim = default;

    public int EXP { set => _update_val.exp += value; }
    int _level = 1;
    public int Level
    {
        get => _level;
    }

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
        _HPslider = GameObject.Find(_HPslider.name).GetComponent<Slider>();
        _bulletPool.SetBaseObj(_prefab, _root);
        _bulletPool.SetCapacity(1000);
        _update_val.hp = _init_val.hp;
        _update_val.exp = _init_val.exp;
        _update_val.col = _init_val.col;
        _HPslider.maxValue = _init_val.hp;
        _HPslider.value = _init_val.hp;
        _EXPslider.maxValue = 1000;
        _EXPslider.value = 0;
        _levelText.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 dir = new Vector2(h, v);
        Vector2 verocity = dir.normalized * _speed;
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
        if (_update_val.exp >= 1000)
        {
            _level++;
            _update_val.exp = 0;
            _EXPslider.maxValue = 1000;
            LevelUpPanel.SetActive(true);
        }
        _EXPslider.value = _update_val.exp;
        _levelText.text = "Level" + _level;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") _update_val.hp--;
        _HPslider.value = _update_val.hp;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    [Serializable]
    public struct Status
    {
        public int hp;
        public int exp;
        public float col;
    }
}
