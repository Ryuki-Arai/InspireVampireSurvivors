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
    [SerializeField] Bullet _prefab = default;
    [SerializeField] Transform _root = default;
    [SerializeField] Slider _EXPslider = default;
    [SerializeField] Slider _HPslider = default;
    [SerializeField] TextMeshProUGUI _levelText;
    [SerializeField] Status _init_val;
    [SerializeField] GameObject LevelUpPanel;
    [SerializeField] GameObject ResultPanel;
    [SerializeField] TextAsset _lebeluptable;
    [SerializeField] CircleCollider2D _col2d;
    [NonSerialized] public Status UpdateVal;
    Rigidbody2D _rb2d;
    Animator _anim = default;

    public int EXP { set => UpdateVal.exp += value; }
    public int Level { get => UpdateVal.level; }
    public int MaxHP { get => (int)_HPslider.maxValue; }
    public int NowHP { get => UpdateVal.hp; }

    float _timer = 0.0f;

    ObjectPool<Bullet> _bulletPool = new ObjectPool<Bullet>();

    void Awake()
    {
        GameManager.Instance.SetPlayer(this);
        LevelTable.LoadFile(_lebeluptable.name);
    }

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _HPslider = GameObject.Find(_HPslider.name).GetComponent<Slider>();
        _bulletPool.SetBaseObj(_prefab, _root);
        _bulletPool.SetCapacity(1000);
        UpdateVal = _init_val;
        _HPslider.maxValue = _init_val.hp;
        _HPslider.value = _init_val.hp;
        _EXPslider.maxValue = LevelTable.NextLevelEXP(Level);
        _EXPslider.value = 0;
        _levelText.GetComponent<TextMeshProUGUI>();
        _col2d.GetComponent<CircleCollider2D>();
        Debug.Log(_col2d.isTrigger);
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
        if (_timer > UpdateVal.shootTime)
        {
            var script = _bulletPool.Instantiate();
            script.transform.position = this.transform.position;
            script.Shoot();
            _timer -= UpdateVal.shootTime;
        }
        if (UpdateVal.exp >= _EXPslider.maxValue)
        {
            if(!LevelUpPanel.activeSelf)UpdateVal.level++;
            UpdateVal.exp -= (int)_EXPslider.maxValue;
            _EXPslider.maxValue = LevelTable.NextLevelEXP(Level);
            LevelUpPanel.SetActive(true);
        }
        if(UpdateVal.hp <= 0)
        {
            ResultPanel.SetActive(true);
        }
        _EXPslider.value = UpdateVal.exp;
        _HPslider.value = UpdateVal.hp;
        _col2d.radius = UpdateVal.col;
        _levelText.text = $"Level.{UpdateVal.level}";
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
        if (collision.gameObject.tag == "Enemy") UpdateVal.hp--;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    [Serializable]
    public struct Status
    {
        public int hp;
        public int level;
        public int atk;
        public float shootTime;
        public int exp;
        public float col;
    }
}
