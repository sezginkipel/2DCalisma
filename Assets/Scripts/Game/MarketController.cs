using UnityEngine;

public class MarketController : MonoBehaviour
{
    public GameObject marketPanel;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Market Alanına Girdiniz! Alışveriş Yapabilirsiniz.");
            marketPanel.SetActive(true);
            Time.timeScale = 0f; // Oyunu duraklat
        }
    }
}
