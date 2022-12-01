using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneOnUse : MonoBehaviour {
    public string bailCondition;
    public string whenVariable;
    public bool isWhenVariableBoolean;
    public List<string> hasValue;
    public string sceneToLoad;
    public string overrideUseMessage;
    private Usable usable;

    private float updateTime;
    private const float MAX_UPDATE_TIME = 0.3f;

    [Header("Read-only")]
    public bool isConditionTrue = false;

    private bool addedListener = false;

    // Start is called before the first frame update
    void Start() {
        usable = GetComponent<Usable>();
    }

    // Update is called once per frame
    void Update() {
        bool mustBail = DialogueLua.GetVariable(bailCondition).asBool;

        if (
            (
            !DialogueLua.GetVariable("Doing Interviews").asBool
            && !DialogueLua.GetVariable("Doing Plundering Interviews").asBool
            && !DialogueLua.GetVariable("Doing Mutiny Interviews").asBool
            )
            || mustBail
        ) {
            return;
        }

        updateTime += Time.deltaTime;
        if (updateTime > MAX_UPDATE_TIME) {
            if (isWhenVariableBoolean) {
                isConditionTrue = DialogueLua.GetVariable(whenVariable).asBool;
            } else {
                isConditionTrue = hasValue.Contains(DialogueLua.GetVariable(whenVariable).asString);
            }

            if (isConditionTrue) {
                usable.overrideUseMessage = overrideUseMessage;
                if (!addedListener) {
                    usable.events.onUse.AddListener(ExecuteUse);
                    addedListener = true;
                }
            } else {
                if (addedListener) {
                    usable.overrideUseMessage = "";
                    usable.events.onUse.RemoveListener(ExecuteUse);
                    addedListener = false;
                }
            }

            updateTime = 0f;
        }
    }

    private void ExecuteUse() {
        GameStateMono.instance.StoreLastLocation();
        SceneManager.LoadScene(sceneToLoad);
    }
}
