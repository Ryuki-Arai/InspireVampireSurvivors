using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string _sceneName;
    public void OnClick()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
