//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Animations;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float _time = 0.05f;
    [SerializeField] Enemy _prefab = null;
    [SerializeField] Transform _root = null;
    [SerializeField] int _distance;
    [SerializeField] EnemyStatus[] _enemyStatus = new EnemyStatus[5];

    float _timer = 0.0f;
    float _cRad = 0.0f;
    Vector3 _popPos = new Vector3(0, 0, 0);

    ObjectPool<Enemy> _enemyPool = new ObjectPool<Enemy>();

    private void Start()
    {
        _enemyPool.SetBaseObj(_prefab, _root);
        _enemyPool.SetCapacity(1000);
        GameManager.Instance.SetList();

        //for (int i = 0; i < 900; ++i) Spawn();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > _time)
        {
            Spawn();
            _timer -= _time;
        }
    }

    void Spawn()
    {
        var script = _enemyPool.Instantiate();
        script.SetStatus = _enemyStatus[Random.Range(0,_enemyStatus.Length)];
        var randX = Random.Range(0.0f, 1.0f);
        var randY = Random.Range(0.0f, 1.0f);
        var pointX = Mathf.Sqrt(-2 * Mathf.Log(randX)+_distance) * Mathf.Cos(2 * Mathf.PI *randY);
        var pointY = Mathf.Sqrt(-2 * Mathf.Log(randX)+_distance) * Mathf.Sin(2 * Mathf.PI *randY);
        _popPos.x = GameManager.Player.transform.position.x + pointX;
        _popPos.y = GameManager.Player.transform.position.y + pointY;
        script.transform.position = _popPos;
        _cRad += 0.1f;
    }
}
