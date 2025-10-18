using UnityEngine;

public class MarketController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                Debug.Log("Market Alanına Girdiniz! Alışveriş Yapabilirsiniz.");
                GameManager.Instance.ChangeState(GameManager.GameState.Paused);
            }
        }
    }
}
