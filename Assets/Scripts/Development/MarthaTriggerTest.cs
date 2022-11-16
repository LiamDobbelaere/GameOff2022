using UnityEngine;

public class MarthaTriggerTest : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("that tickles my dude");
    }
}
