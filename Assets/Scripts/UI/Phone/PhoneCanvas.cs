using PixelCrushers.DialogueSystem;
using UnityEngine;

public class PhoneCanvas : MonoBehaviour {
    private GameObject phoneOverlay;

    // Start is called before the first frame update
    void Start() {
        phoneOverlay = transform.Find("PhoneOverlay").gameObject;
        phoneOverlay.SetActive(false);
    }

    private void OnEnable() {
        Lua.RegisterFunction(
            "ShowPhone", this, SymbolExtensions.GetMethodInfo(() => ShowPhone())
        );
    }

    private void OnDisable() {
        Lua.UnregisterFunction("ShowPhone");
    }

    public void ShowPhone() {
        phoneOverlay.SetActive(true);
    }

    public void HidePhone() {
        phoneOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Phone")) {
            phoneOverlay.SetActive(!phoneOverlay.activeSelf);
        }
    }
}
