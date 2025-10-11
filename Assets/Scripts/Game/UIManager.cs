using UnityEngine;

public class UIManager : MonoBehaviour
{


    public void CloseMarketPanel(GameObject marketPanel)
    {
        Debug.Log("Market Alanından Çıktınız!");
        marketPanel.SetActive(false);
        Time.timeScale = 1f; // Oyunu devam ettir
    }
}
