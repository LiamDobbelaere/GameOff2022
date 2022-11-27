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
        Lua.RegisterFunction(
            "ShowUIHint", this, SymbolExtensions.GetMethodInfo(() => GameState.ShowUIHint(string.Empty))
        );
        Lua.RegisterFunction(
            "Advance", this, SymbolExtensions.GetMethodInfo(() => Advance())
        );
        Lua.RegisterFunction(
            "AddDocumentaryScene", this, SymbolExtensions.GetMethodInfo(() => GameState.AddDocumentaryScene(string.Empty))
        );
    }

    private void OnDisable() {
        Lua.UnregisterFunction("SwitchScene");
        Lua.UnregisterFunction("ShowUIHint");
        Lua.UnregisterFunction("Advance");
        Lua.UnregisterFunction("AddDocumentaryScene");
    }

    private void Advance() {
        GameObject.FindGameObjectWithTag("DocumentarySequencer")
            .GetComponent<DocumentarySequencer>().Advance();
    }

    private void SwitchScene(string sceneName) {
        GameState.StoreLastLocation();
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update() {

    }
}
