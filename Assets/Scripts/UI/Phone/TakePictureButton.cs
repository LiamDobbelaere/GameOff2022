using UnityEngine;
using UnityEngine.UI;

public class TakePictureButton : MonoBehaviour {
    private int oldCullingMask = -1;

    // Start is called before the first frame update
    void Start() {
        GetComponent<Button>().onClick.AddListener(() => {
            SaneAudio.PlaySFX("phone.click-button");

            GameObject flagCaptureCamera = GameObject.FindGameObjectWithTag("FlagCaptureCamera");
            if (flagCaptureCamera != null) {
                Camera flagCam = flagCaptureCamera.GetComponent<Camera>();
                RenderTexture renderTexture = flagCam.targetTexture;

                flagCam.Render();

                RenderTexture.active = renderTexture;
                GameStateMono.instance.customFlagTexture.ReadPixels(
                        new Rect(0, 0, renderTexture.width, renderTexture.height),
                    0, 0);
                GameStateMono.instance.customFlagTexture.Apply();


                int currentCullingMask = flagCam.cullingMask;
                flagCam.cullingMask = oldCullingMask;
                flagCam.Render();

                GameStateMono.instance.customFlagTextureDrawn.ReadPixels(
                        new Rect(0, 0, renderTexture.width, renderTexture.height),
                    0, 0);
                GameStateMono.instance.customFlagTextureDrawn.Apply();

                flagCam.cullingMask = currentCullingMask;

                GameStateMono.instance.customFlagIsJollyRoger =
                    GameObject.FindGameObjectWithTag("DragAndDropItems")
                        .GetComponent<JollyRogerCheck>().isJollyRoger;

                GameStateMono.instance.hasTakenPictureOf["flag"] = true;

                GetComponentInParent<Phone>().StartApp("Pictures");
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
