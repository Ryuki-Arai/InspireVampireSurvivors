using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[System.Serializable]
public struct EnemyStatus
{
    public AnimatorController ac;
    public int Hp;
    public float speed;
}
