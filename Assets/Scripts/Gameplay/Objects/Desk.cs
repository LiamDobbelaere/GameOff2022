using UnityEngine;
using UnityEngine.SceneManagement;

public class Desk : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void OnUse() {
        GameStateMono.instance.StoreLastLocation();
        SceneManager.LoadScene("FlagEditor");
    }
}
