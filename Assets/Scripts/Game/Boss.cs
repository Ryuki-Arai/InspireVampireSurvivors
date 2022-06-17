using UnityEngine;

public class Boss : MonoBehaviour,IObjectPool
{
    [SerializeField] int HP;
    [SerializeField] float _speed = 10;
    [SerializeField] Exp _prefab = default;
    Transform _root = default;

    int _hp;
    Rigidbody2D _rb2d;
    Animator _anim = default;

    ObjectPool<Exp> _expPool = new ObjectPool<Exp>();
    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _expPool.SetBaseObj(_prefab, GameObject.Find("ExpRoot").transform);
        _expPool.SetCapacity(1000);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsActive) return;

        Vector2 verocity = new Vector2();
        if (_hp < HP * 0.8)
        {
            verocity = -((GameManager.Player.transform.position - transform.position).normalized);
        }
        else
        {
            verocity = (GameManager.Player.transform.position - transform.position).normalized;
            
        }

        _rb2d.velocity = verocity * _speed;
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
        if (collision.gameObject.tag == "Bullet") _anim.SetTrigger("Damage");
    }

    public void Damage()
    {
        _hp -= GameManager.Player.UpdateVal.atk;
        if (_hp <= 0) Delete();
    }

    void Delete()
    {
        for(int i = 0; i < 10; i++)
        {
            var script = _expPool.Instantiate();
            var pos = new Vector3(Random.Range(0.0f, 10.0f), Random.Range(0.0f, 10.0f));
            script.transform.position = this.transform.position + pos;
        }
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
        _hp = HP;
        gameObject.SetActive(true);
        _isActrive = true;
    }
    public void Destroy()
    {
        gameObject.SetActive(false);
        GameManager.EnemyCount = 1;
        GameObject.Find("GameObject").GetComponent<Spawner>().enabled = true;
        _isActrive = false;
    }
}
