using UnityEngine;

public class Phone : MonoBehaviour {
    private Transform screenMask;
    private RectTransform rect;
    private const float PHONE_APPEAR_SPEED = 8f;

    private void Awake() {
        rect = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start() {
        screenMask = transform.GetChild(0);
        StartApp("HomeScreen");
    }

    // Update is called once per frame
    void Update() {
        Vector2 anchoredPos = rect.anchoredPosition;

        anchoredPos.y = Mathf.Lerp(anchoredPos.y, 0f, PHONE_APPEAR_SPEED * Time.deltaTime);

        rect.anchoredPosition = anchoredPos;
    }

    private void OnEnable() {
        Vector2 anchoredPos = rect.anchoredPosition;

        anchoredPos.y = -rect.rect.height;

        rect.anchoredPosition = anchoredPos;
    }

    public void StartApp(string appName) {
        UnloadAllApps();
        ActivateApp(appName);
    }

    private void ActivateApp(string appName) {
        screenMask.Find(appName).gameObject.SetActive(true);
    }

    private void UnloadAllApps() {
        for (int i = 0; i < screenMask.childCount; i++) {
            GameObject child = screenMask.GetChild(i).gameObject;

            if (child.activeSelf) {
                child.SetActive(false);
            }
        }
    }
}
