using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour

{
    private Rigidbody rb;

    public float jumpForce = 1.5f;
    public KeyCode rotateKey;
    private Vector3 movementDirection;
    bool canMove = true;
    bool gameStarted = false;
    public static Movement instance;
    public AudioClip bounceClip;
    public AudioSource audioSource;
    public Animator animator;

    [SerializeField] float halfJumptime = 0.3f;
    Coroutine jumpAnim;
    public float initialmovementFreeze; 

    private void Start()
    {
        movementDirection = transform.forward;
        rb = GetComponent<Rigidbody>();
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(rotateKey) && gameStarted == true)
        {
            transform.rotation *= Quaternion.Euler(0, 90, 0);
            animator.SetTrigger("Trigger");
            canMove = false;
        }
        if (Input.GetKeyUp(rotateKey))
        {
            canMove = true;
        }
    }

    IEnumerator JumpAnim()
    {
        while (true)
        {
            JumpUp();
            Jump();
            yield return new WaitForSeconds(0.5f + 2 * halfJumptime);
        }
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(initialmovementFreeze);
        gameStarted = true;
    }

    void JumpUp()
    {
        transform.DOBlendableMoveBy(new Vector3(0, 1, 0), halfJumptime).SetEase(Ease.OutCubic).OnComplete(() => 
        {
            transform.DOBlendableMoveBy(new Vector3(0, -1, 0), halfJumptime).SetEase(Ease.InCubic);
        });
    }


    void Jump()
    {
        audioSource.PlayOneShot(bounceClip);

        if(canMove == true)
        {
            transform.DOBlendableMoveBy(transform.forward * jumpForce, 0.6f);
        }
    }    

    public void StartMoving()
    {
        StartCoroutine(StartCountdown());
        jumpAnim = StartCoroutine(JumpAnim());
    }
}
