using UnityEngine;
using UnityEngine.UI;

public class PirateFlagPicture : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
    }

    private void OnEnable() {
        GetComponent<RawImage>().texture = GameState.customFlagTexture;
    }

    // Update is called once per frame
    void Update() {

    }
}
