using UnityEngine;

public class Bullet : MonoBehaviour, IObjectPool
{
    [SerializeField] float _speed = 255;
    Rigidbody2D _rb2d;
    Enemy _enemyTarget;
    Boss _bossTarget;
    Vector2 _shootVec;

    float _timer = 0.0f;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void Shoot()
    {
        var list = GameManager.EnemyList;
        _enemyTarget = null;
        float len = -1;
        Vector2 vec;
        foreach(var e in list)
        {
            if (!e.IsActive) continue;
            vec = e.transform.position - GameManager.Player.transform.position;
            if(len == -1 || vec.magnitude < len)
            {
                _enemyTarget = e;
                len = vec.magnitude;
            }
        }

        var bossList = GameManager.BossList;
        _bossTarget = null;
        float bossLen = -1;
        Vector2 bossVec;
        foreach(var b in bossList)
        {
            if (!b.IsActive) continue;
            bossVec = b.transform.position - GameManager.Player.transform.position;
            if (bossLen == -1 || bossVec.magnitude < bossLen)
            {
                _bossTarget = b;
                bossLen = bossVec.magnitude;
            }
        }

        if (_enemyTarget == null && _bossTarget == null) return;
        else if(_enemyTarget != null && _bossTarget == null) _shootVec = _enemyTarget.transform.position - GameManager.Player.transform.position;
        else _shootVec = _bossTarget.transform.position - GameManager.Player.transform.position;
    }

    void Update()
    {
        Vector2 velocity = _shootVec.normalized * _speed;
        _rb2d.velocity = velocity;

        _timer += Time.deltaTime;
        if(_timer > 3.0f)
        {
            Delete();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Damage();
            _shootVec = Vector2.zero;
            GetComponent<Animator>().SetTrigger("Hit");
        }
        if(collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<Boss>().Damage();
            _shootVec = Vector2.zero;
            GetComponent<Animator>().SetTrigger("Hit");
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
