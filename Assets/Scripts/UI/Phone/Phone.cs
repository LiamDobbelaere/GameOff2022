using UnityEngine;

public class Phone : MonoBehaviour {
    private Transform screenMask;

    // Start is called before the first frame update
    void Start() {
        screenMask = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update() {

    }

    public void StartApp(string appName) {
        UnloadCurrentApp();
        ActivateApp(appName);
    }

    private void ActivateApp(string appName) {
        screenMask.Find(appName).gameObject.SetActive(true);
    }

    private void UnloadCurrentApp() {
        for (int i = 0; i < screenMask.childCount; i++) {
            GameObject child = screenMask.GetChild(i).gameObject;

            if (child.activeSelf) {
                child.SetActive(false);
            }
        }
    }
}
