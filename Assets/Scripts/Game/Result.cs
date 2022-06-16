using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _resultTime;
    [SerializeField] TextMeshProUGUI _resultLevel;
    [SerializeField] TextMeshProUGUI _resultEnemy;
    Timer._Time _time;
    int _level;
    int _enemycount;

    void Start()
    {
        _resultTime = GetComponent<TextMeshProUGUI>();
        _resultLevel = GetComponent<TextMeshProUGUI>();
        _resultEnemy = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _time = Timer.GetTime;
        _resultTime.text = $"�������ԁF {_time.hour}:{_time.minute}:{(int)_time.second}";
        _level = GameManager.Player.Level;
        _resultLevel.text = $"���B���x���FLevel.{_level}";
        _enemycount = GameManager.EnemyCount;
        _resultEnemy.text = $"�|�����G�̐��F{_enemycount}";
    }

    public void OnQuit()
    {

    }
}
