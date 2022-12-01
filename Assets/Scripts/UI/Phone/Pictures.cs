using UnityEngine;

public class Pictures : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    private void OnEnable() {
        foreach (string key in GameStateMono.instance.hasTakenPictureOf.Keys) {
            transform.Find(key).gameObject.SetActive(GameStateMono.instance.hasTakenPictureOf[key]);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
