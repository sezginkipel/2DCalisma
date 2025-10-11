using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    public Health playerHealth;
    public int playerCoin = 0;
    public TMP_Text coinText;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            playerHealth.TakeDamage(10f); 
        }
        else if (collision.CompareTag("Coin"))
        {
            playerCoin += 10;
            coinText.text = "🪙" + playerCoin.ToString();
            collision.gameObject.transform.DOMove(transform.position, 0.25f).SetEase(Ease.OutQuad);
            Destroy(collision.gameObject, 0.25f);
           

        }
        
    }
}
