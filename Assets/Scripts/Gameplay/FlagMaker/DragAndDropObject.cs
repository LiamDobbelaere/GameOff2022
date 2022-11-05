using UnityEngine;

public class DragAndDropObject : MonoBehaviour {
    private Vector2 distanceFromOrigin;
    private CopyBoundsAndDisable boundsCopy;
    private DragAndDropItems dndItems;
    private JollyRogerCheck jrCheck;

    // Start is called before the first frame update
    void Start() {
        boundsCopy = transform.parent.GetComponent<CopyBoundsAndDisable>();
        dndItems = transform.parent.GetComponent<DragAndDropItems>();
        jrCheck = transform.parent.GetComponent<JollyRogerCheck>();
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, boundsCopy.bounds.min.x, boundsCopy.bounds.max.x),
            Mathf.Clamp(transform.position.y, boundsCopy.bounds.min.y, boundsCopy.bounds.max.y)
        );
    }

    public void Rotate() {
        transform.Rotate(Vector3.forward, 30f);
        jrCheck.DoCheck();
    }

    private void OnMouseDown() {
        distanceFromOrigin = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dndItems.lastTouched = this;
    }

    private void OnMouseDrag() {
        transform.position = distanceFromOrigin + (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp() {
        jrCheck.DoCheck();
    }
}
