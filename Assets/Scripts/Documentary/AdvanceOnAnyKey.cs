using UnityEngine;

public class AdvanceOnAnyKey : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.anyKeyDown) {
            GameObject
                .FindGameObjectWithTag("DocumentarySequencer")
                .GetComponent<DocumentarySequencer>()
                .Advance();
        }
    }
}
