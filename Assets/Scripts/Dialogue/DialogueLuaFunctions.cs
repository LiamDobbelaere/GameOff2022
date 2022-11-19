using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueLuaFunctions : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    private void OnEnable() {
        Lua.RegisterFunction(
            "SwitchScene", this, SymbolExtensions.GetMethodInfo(() => SwitchScene(string.Empty))
        );
    }

    private void OnDisable() {
        Lua.UnregisterFunction("SwitchScene");
    }

    private void SwitchScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update() {

    }
}
