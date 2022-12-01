using UnityEngine;

public class GameStateMono : MonoBehaviour {
    public static GameState instance = null;

    // Start is called before the first frame update
    void Start() {
        if (instance == null) {
            transform.parent = null;
            instance = new GameState();
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
