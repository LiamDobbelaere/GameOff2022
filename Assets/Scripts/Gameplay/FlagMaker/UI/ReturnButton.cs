using UnityEngine;
using UnityEngine.UI;

public class ReturnButton : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        GetComponent<Button>().onClick.AddListener(() => {
            GameStateMono.instance.LoadLastLocation();
        });
    }

    // Update is called once per frame
    void Update() {

    }
}
