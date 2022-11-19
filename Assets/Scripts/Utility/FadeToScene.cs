using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeToScene : MonoBehaviour {
    public string sceneName;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void BeginFade() {
        SceneManager.LoadScene(sceneName);
    }
}
