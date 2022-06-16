using UnityEngine;

public class Exp : MonoBehaviour,IObjectPool
{
    [SerializeField] int _exp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().EXP = _exp;
            Destroy();
        }
    }

    bool _isActrive = false;
    public bool IsActive => _isActrive;
    public void DisactiveForInstantiate()
    {
        gameObject.SetActive(false);
        _isActrive = false;
    }
    public void Create()
    {
        gameObject.SetActive(true);
        _isActrive = true;
    }
    public void Destroy()
    {
        gameObject.SetActive(false);
        _isActrive = false;
    }


}
