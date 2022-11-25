using PixelCrushers.DialogueSystem;
using UnityEngine;

public class PhoneCanvas : MonoBehaviour {
    public AudioClip showPhone;
    public AudioClip forceShowPhone;
    private GameObject phoneOverlay;

    // Start is called before the first frame update
    void Start() {
        phoneOverlay = transform.Find("PhoneOverlay").gameObject;
        phoneOverlay.SetActive(false);
    }

    private void OnEnable() {
        Lua.RegisterFunction(
            "ShowPhone", this, SymbolExtensions.GetMethodInfo(() => ShowPhone(false))
        );
    }

    private void OnDisable() {
        Lua.UnregisterFunction("ShowPhone");
    }

    public void ShowPhone(bool isManual = false) {
        phoneOverlay.SetActive(true);

        if (isManual) {
            GetComponent<AudioSource>().PlayOneShot(showPhone);
        } else {
            GetComponent<AudioSource>().PlayOneShot(forceShowPhone);
        }
    }

    public void HidePhone() {
        phoneOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Phone")) {
            if (phoneOverlay.activeSelf) {
                HidePhone();
            } else {
                ShowPhone(true);
            }
        }
    }
}
