using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUP : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI button1;
    [SerializeField] TextMeshProUGUI button2;
    [SerializeField] TextMeshProUGUI button3;
    [SerializeField] TextMeshProUGUI button4;

    private void OnEnable()
    {
        button1.text = "攻撃力アップ";
        button2.text = "武器強化";
        button3.text = "経験値回収効率増加";
        button4.text = "体力回復";
        Time.timeScale = 0;
    }

    public void OnButton1Clecked()
    {
        Time.timeScale = 1;
        GameManager.Player._update_val.atk++;
        this.gameObject.SetActive(false);
    }

    public void OnButton2Clecked()
    {
        Time.timeScale = 1;
        GameManager.Player._update_val.shootTime += 0.05f;
        this.gameObject.SetActive(false);
    }

    public void OnButton3Clecked()
    {
        Time.timeScale = 1;
        GameManager.Player._update_val.col += 0.01f;
        this.gameObject.SetActive(false);
    }

    public void OnButton4Clecked()
    {
        Time.timeScale = 1;
        GameManager.Player._update_val.hp = GameManager.Player.MaxHP;
        this.gameObject.SetActive(false);
    }
}
