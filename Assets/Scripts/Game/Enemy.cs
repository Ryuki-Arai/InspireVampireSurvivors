using UnityEngine;

public class Enemy : MonoBehaviour, IObjectPool
{
    [SerializeField] Exp _prefab = default;
    Transform _root = default;
    SpriteRenderer _sr;
    public EnemyStatus _status;

    public EnemyStatus SetStatus
    {
        set
        {
            _status = value;
        }
    }

    Rigidbody2D _rb2d;
    Animator _anim = default;

    ObjectPool<Exp> _expPool = new ObjectPool<Exp>();

    void Awake()
    {
        
    }
    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _expPool.SetBaseObj(_prefab, GameObject.Find("ExpRoot").transform);
        _expPool.SetCapacity(1000);

    }

    void Update()
    {
        if (!IsActive) return;
        if (!_anim.runtimeAnimatorController)
        {
            _anim.runtimeAnimatorController = _status.animetion;
        }

        Vector2 verocity = (GameManager.Player.transform.position - transform.position).normalized;
        _rb2d.velocity = verocity * _status.speed;

        if (_status.hp <= 0) Delete();       
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            _sr.color = Color.red;
            //_anim.SetTrigger("Damage");
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _sr.color = Color.white;
    }


    public void Damage()
    {
        _status.hp -= GameManager.Player.UpdateVal.atk;
    }

    void Delete()
    {
        var script = _expPool.Instantiate();
        script.transform.position = this.transform.position;
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
        _anim.runtimeAnimatorController = null;
        _sr.color = Color.white;
        GameManager.EnemyCount = 1;
        _isActrive = false;
    }
}
