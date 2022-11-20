using UnityEngine;

public class AdvanceAfterTime : MonoBehaviour {
    public float time = 10000f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        time -= Time.deltaTime;

        if (time <= 0f) {
            GameObject
                .FindGameObjectWithTag("DocumentarySequencer")
                .GetComponent<DocumentarySequencer>()
                .Advance();
        }
    }
}
