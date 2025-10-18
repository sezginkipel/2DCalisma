using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator _playerAnimator;
    private PlayerMovement _playerMovement;

    void Start()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();

        if (_playerMovement == null)
        {
            Debug.LogError("PlayerMovement script not found on this GameObject! Animations will not work.");
            enabled = false; // Disable this script if the movement script is missing
        }
    }

    void Update()
    {
        // Read the state from PlayerMovement and update the animator
        _playerAnimator.SetBool("isWalking", _playerMovement.IsWalking);
        _playerAnimator.SetBool("isRunning", _playerMovement.IsRunning);
    }
}
