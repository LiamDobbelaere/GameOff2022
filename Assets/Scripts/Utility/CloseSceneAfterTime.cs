using UnityEngine;

public class CloseSceneAfterTime : MonoBehaviour {
    public float time;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        time -= Time.deltaTime;
        if (time <= 0f) {
            GameState.LoadLastLocation();
        }
    }
}
