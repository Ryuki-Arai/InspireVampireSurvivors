﻿using System.Collections;
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

    List<Exp> _exps = new List<Exp>();

    static public List<Exp> ExpList => _instance._exps;
    public void SetList()
    {
        //ObjectPoolに依存している
        _enemies = GameObject.FindObjectsOfType<Enemy>(true).ToList();
        _exps = GameObject.FindObjectsOfType<Exp>(true).ToList();
    }
}
