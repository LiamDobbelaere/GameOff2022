using UnityEngine;

public class PlayerAnimations : MonoBehaviour {
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer rend;
    private string currentAnimationState = "WalkDown";

    // Start is called before the first frame update
    void Start() {
        anim = GetComponentInChildren<Animator>();
        rend = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        bool isMoving = rb.velocity.magnitude > 0f;
        if (isMoving) {
            currentAnimationState = GetCurrentAnimationStateName();
            rend.flipX = GetCurrentSpriteFlipState();
            anim.speed = 0.12f;
            anim.Play(currentAnimationState);
        } else {
            anim.speed = 0f;
            anim.Play(currentAnimationState, -1, 0f);
        }

    }

    private string GetCurrentAnimationStateName() {
        if (rb.velocity.y > 0f) {
            return "WalkUp";
        } else if (rb.velocity.y < 0f) {
            return "WalkDown";
        } else {
            return "WalkLeft";
        }
    }

    private bool GetCurrentSpriteFlipState() {
        if (rb.velocity.x > 0f) {
            return true;
        } else {
            return false;
        }
    }
}
