using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuStartHanlder : MonoBehaviour
{
    public void PlayGameButton()
    {
        SceneManager.LoadScene("Game");
    }
}
