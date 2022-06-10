using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUP : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI button1;
    [SerializeField] TextMeshProUGUI button2;
    [SerializeField] TextMeshProUGUI button3;

    private void OnEnable()
    {
        button1.text = "Atk GradeUp";
        button2.text = "EXP GradeUp";
        button3.text = "col GradeUp";
        Time.timeScale = 0;
    }

    public void OnButton1Clecked()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    public void OnButton2Clecked()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    public void OnButton3Clecked()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
}
