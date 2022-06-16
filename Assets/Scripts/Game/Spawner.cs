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
    [SerializeField] Enemy _enemy = null;
    [SerializeField] Boss _boss = null;
    [SerializeField] Transform _root = null;
    [SerializeField] int _distance;
    [SerializeField] int _bossSpawnLevel;
    [SerializeField] EnemyStatus[] _enemyStatus = new EnemyStatus[5];
    int leveled = 0;
    bool _bossCall;

    float _timer = 0.0f;
    Vector3 _popPos = new Vector3(0, 0, 0);

    ObjectPool<Enemy> _enemyPool = new ObjectPool<Enemy>();
    ObjectPool<Boss> _bossPool = new ObjectPool<Boss>();

    private void Start()
    {
        _enemyPool.SetBaseObj(_enemy, _root);
        _enemyPool.SetCapacity(1000);
        GameManager.Instance.SetList();

        _bossPool.SetBaseObj(_boss, _root);
        _bossPool.SetCapacity(1000);
        GameManager.Instance.SetList();
        //for (int i = 0; i < 900; ++i) Spawn();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(leveled != GameManager.Player.UpdateVal.level)
        {
            leveled = GameManager.Player.UpdateVal.level;
            _bossCall = true;
        }
        if(_timer > _time)
        {
            Spawn(GameManager.Player.UpdateVal.level < 25 ? GameManager.Player.UpdateVal.level / 5 : _enemyStatus.Length);
            _timer -= _time;
        }
        if(GameManager.Player.UpdateVal.level % _bossSpawnLevel == 0 && _bossCall)
        {
            BossSpawn(GameManager.Player.UpdateVal.level / _bossSpawnLevel);
        }
    }

    /// <summary>
    /// 通常の敵を出現させる
    /// </summary>
    /// <param name="_index"></param>
    void Spawn(int _index)
    {
        var script = _enemyPool.Instantiate();
        script.SetStatus = _enemyStatus[Random.Range(0,_index+1)];
        var _randX = Random.Range(0.0f, 1.0f);
        var _randY = Random.Range(0.0f, 1.0f);
        var posX = Mathf.Sqrt(-2 * Mathf.Log(_randX)+_distance) * Mathf.Cos(2 * Mathf.PI *_randY);
        var posY = Mathf.Sqrt(-2 * Mathf.Log(_randX)+_distance) * Mathf.Sin(2 * Mathf.PI *_randY);
        _popPos.x = GameManager.Player.transform.position.x + posX;
        _popPos.y = GameManager.Player.transform.position.y + posY;
        script.transform.position = _popPos;
    }


    /// <summary>
    /// 通常の敵を座標を指定して出現させる
    /// </summary>
    /// <param name="_index"></param>
    /// <param name="posX"></param>
    /// <param name="posY"></param>
    void Spawn(int _index,int posX, int posY)
    {
        var script = _enemyPool.Instantiate();
        script.SetStatus = _enemyStatus[_index];
        _popPos.x = GameManager.Player.transform.position.x + posX;
        _popPos.y = GameManager.Player.transform.position.y + posY;
        script.transform.position = _popPos;
    }

    void BossSpawn(int _num)
    {
        for(int i = 0; i < _num; i++)
        {
            var script = _bossPool.Instantiate();
            var _randX = Random.Range(0.0f, 1.0f);
            var _randY = Random.Range(0.0f, 1.0f);
            var posX = Mathf.Sqrt(-2 * Mathf.Log(_randX) + _distance) * Mathf.Cos(2 * Mathf.PI * _randY);
            var posY = Mathf.Sqrt(-2 * Mathf.Log(_randX) + _distance) * Mathf.Sin(2 * Mathf.PI * _randY);
            _popPos.x = GameManager.Player.transform.position.x + posX;
            _popPos.y = GameManager.Player.transform.position.y + posY;
            script.transform.position = _popPos;
        }
        _bossCall = false;
        this.enabled = false;
        foreach(var enemy in GameManager.EnemyList)
        {
            if(enemy.IsActive) enemy.Destroy();
        }
    }
}
