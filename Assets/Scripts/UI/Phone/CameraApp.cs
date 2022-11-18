using UnityEngine;
using UnityEngine.UI;

public class CameraApp : MonoBehaviour {
    private RawImage cameraFeed;

    // Start is called before the first frame update
    void Start() {
        cameraFeed = transform.Find("Body/CameraFeed").GetComponent<RawImage>();
        GameObject flagCaptureCamera = GameObject.FindGameObjectWithTag("FlagCaptureCamera");
        if (flagCaptureCamera != null) {
            cameraFeed.texture = flagCaptureCamera.GetComponent<Camera>().targetTexture;
        }
    }

    private void OnEnable() {

    }

    // Update is called once per frame
    void Update() {

    }
}
