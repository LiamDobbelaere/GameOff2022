using UnityEngine;

public class CopyBoundsAndDisable : MonoBehaviour {
    public Bounds bounds;

    void Start() {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        bounds = boxCollider.bounds;
        boxCollider.enabled = false;
    }
}
