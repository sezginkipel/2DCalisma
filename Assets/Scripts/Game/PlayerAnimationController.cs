using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator _playerAnimator;



    void Start()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-0.3f, 0.3f, 1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 1);
        }



        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            _playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            _playerAnimator.SetBool("isWalking", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _playerAnimator.SetBool("isRunning", true);
        }
        else
        {
            _playerAnimator.SetBool("isRunning", false);
        }
    }
}
