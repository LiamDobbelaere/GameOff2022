using UnityEngine;
using UnityEngine.UI;

public class TakePictureButton : MonoBehaviour {
    private int oldCullingMask = -1;

    // Start is called before the first frame update
    void Start() {
        GetComponent<Button>().onClick.AddListener(() => {
            GameObject flagCaptureCamera = GameObject.FindGameObjectWithTag("FlagCaptureCamera");
            if (flagCaptureCamera != null) {
                Camera flagCam = flagCaptureCamera.GetComponent<Camera>();
                RenderTexture renderTexture = flagCam.targetTexture;

                flagCam.Render();

                RenderTexture.active = renderTexture;
                GameState.customFlagTexture.ReadPixels(
                        new Rect(0, 0, renderTexture.width, renderTexture.height),
                    0, 0);
                GameState.customFlagTexture.Apply();


                int currentCullingMask = flagCam.cullingMask;
                flagCam.cullingMask = oldCullingMask;
                flagCam.Render();

                GameState.customFlagTextureDrawn.ReadPixels(
                        new Rect(0, 0, renderTexture.width, renderTexture.height),
                    0, 0);
                GameState.customFlagTextureDrawn.Apply();

                flagCam.cullingMask = currentCullingMask;

                GameState.customFlagIsJollyRoger =
                    GameObject.FindGameObjectWithTag("DragAndDropItems")
                        .GetComponent<JollyRogerCheck>().isJollyRoger;

                GameState.hasTakenPictureOf["flag"] = true;
            }
        });

    }

    private void OnEnable() {
        GameObject flagCaptureCamera = GameObject.FindGameObjectWithTag("FlagCaptureCamera");
        if (flagCaptureCamera != null) {
            Camera flagCam = flagCaptureCamera.GetComponent<Camera>();

            if (oldCullingMask == -1) {
                oldCullingMask = flagCam.cullingMask;
            }
            int newCullingMask = oldCullingMask;

            newCullingMask = newCullingMask & ~(1 << 3);
            newCullingMask = newCullingMask | (1 << 0);

            flagCam.cullingMask = newCullingMask;
        }
    }

    private void OnDisable() {
        GameObject flagCaptureCamera = GameObject.FindGameObjectWithTag("FlagCaptureCamera");
        if (flagCaptureCamera != null) {
            Camera flagCam = flagCaptureCamera.GetComponent<Camera>();
            flagCam.cullingMask = oldCullingMask;
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
