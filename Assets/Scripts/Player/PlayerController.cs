using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    private float walkSpeed = 1.2f;

    // Start is called before the first frame update
    void Start() {
        this.rb = GetComponent<Rigidbody2D>();

        RestoreLastPositionIfSet();
    }

    private void Update() {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * walkSpeed, Input.GetAxisRaw("Vertical") * walkSpeed);
    }

    // Update is called once per frame
    void FixedUpdate() {

    }

    private void RestoreLastPositionIfSet() {
        if (GameState.lastPosition == null) {
            return;
        }

        transform.position = (Vector2)GameState.lastPosition;
        GameState.lastPosition = null;
    }
}
