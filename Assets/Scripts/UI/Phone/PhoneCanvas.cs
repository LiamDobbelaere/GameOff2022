using UnityEngine;

public class PhoneCanvas : MonoBehaviour {
    private GameObject phoneOverlay;

    // Start is called before the first frame update
    void Start() {
        phoneOverlay = transform.Find("PhoneOverlay").gameObject;
        phoneOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Phone")) {
            phoneOverlay.SetActive(!phoneOverlay.activeSelf);
        }
    }
}
