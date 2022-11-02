using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
        this.rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    // Update is called once per frame
    void FixedUpdate() {

    }
}
