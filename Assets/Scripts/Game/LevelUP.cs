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
        button1.text = "�U���͑���";
        button2.text = "���틭��(�p�x�㏸)";
        button3.text = "�U���͈͑���";
        button4.text = "�̗͑S��";
        Time.timeScale = 0;
    }

    public void OnButton1Clecked()
    {
        Time.timeScale = 1;
        GameManager.Player.UpdateVal.atk++;
        this.gameObject.SetActive(false);
    }

    public void OnButton2Clecked()
    {
        Time.timeScale = 1;
        if(GameManager.Player.UpdateVal.shootTime <= 0.05f) GameManager.Player.UpdateVal.shootTime -= 0.05f;
        this.gameObject.SetActive(false);
    }

    public void OnButton3Clecked()
    {
        Time.timeScale = 1;
        GameManager.Player.UpdateVal.col += 0.01f;
        this.gameObject.SetActive(false);
    }

    public void OnButton4Clecked()
    {
        Time.timeScale = 1;
        GameManager.Player.UpdateVal.hp = GameManager.Player.MaxHP;
        this.gameObject.SetActive(false);
    }
}
