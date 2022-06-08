using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _tmpro;

    float _t_second;
    float _t_minute;
    float _t_hour;

    // Start is called before the first frame update
    void Start()
    {
        _t_second = 0;
        _t_minute = 0;
        _t_hour = 0;
        _tmpro.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _t_second += Time.deltaTime;
        if (_t_second > 60)
        {
            _t_minute++;
            _t_second = 0;
        }
        if (_t_minute > 60)
        {
            _t_hour++;
            _t_minute = 0;
        }
        _tmpro.text = $"{((int)_t_hour).ToString("D2")} : {((int) _t_minute).ToString("D2")} : { ((int)_t_second).ToString("D2")}";
    }
}
