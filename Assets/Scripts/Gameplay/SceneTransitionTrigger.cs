using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour {
    public string targetSceneName;
    public string targetMarkerName;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (targetMarkerName.Length > 0) {
            GameStateMono.instance.targetMarker = targetMarkerName;
        }

        SceneManager.LoadScene(targetSceneName);
    }
}
