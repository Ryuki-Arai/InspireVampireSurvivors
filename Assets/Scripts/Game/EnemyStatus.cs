using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

[System.Serializable]
public struct EnemyStatus
{
    public AnimatorController animetion;
    public int hp;
    public float speed;
}
