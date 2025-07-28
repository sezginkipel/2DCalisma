using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public void OyunuBaslat()
    {
        SceneManager.LoadScene("Game");
    }
}
