using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchSceneButton : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene(gameObject.name);
        });
    }

    // Update is called once per frame
    void Update() {

    }
}
