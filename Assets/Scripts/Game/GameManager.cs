using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager
{
    static private GameManager _instance = new GameManager();
    static public GameManager Instance => _instance;
    private GameManager() { }

    Player _player = null;
    static public Player Player => _instance._player;
    public void SetPlayer(Player p) { _player = p; }

    List<Enemy> _enemies = new List<Enemy>();
    static public List<Enemy> EnemyList => _instance._enemies;

    List<Boss> _bosss = new List<Boss>();
    static public List<Boss> BossList => _instance._bosss;

    int _enemysCount = 0;
    static public int EnemyCount
    {
        get => _instance._enemysCount;
        set => _instance._enemysCount += value;
    }

    List<Exp> _exps = new List<Exp>();
    static public List<Exp> ExpList => _instance._exps;
    public void SetList()
    {
        //ObjectPoolに依存している
        _enemies = GameObject.FindObjectsOfType<Enemy>(true).ToList();
        _bosss = GameObject.FindObjectsOfType<Boss>(true).ToList();
        _exps = GameObject.FindObjectsOfType<Exp>(true).ToList();
    }
}
