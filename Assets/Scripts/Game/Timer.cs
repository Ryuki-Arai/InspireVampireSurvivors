using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _tmpro;

    static _Time _time;

    public static _Time GetTime
    {
        get => _time;
    }

    public struct _Time
    {
        public float second;
        public int minute;
        public int hour;
    }

    // Start is called before the first frame update
    void Start()
    {
        _time.second = 0;
        _time.minute = 0;
        _time.hour = 0;
        _tmpro.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _time.second += Time.deltaTime;
        if (_time.second > 59)
        {
            _time.minute++;
            _time.second = 0;
        }
        if (_time.minute > 59)
        {
            _time.hour++;
            _time.minute = 0;
        }
        _tmpro.text = $"{((int)_time.hour).ToString("D2")}:{((int)_time.minute).ToString("D2")}:{ ((int)_time.second).ToString("D2")}";
    }
}
