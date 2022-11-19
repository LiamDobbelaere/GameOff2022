using UnityEngine;
using UnityEngine.UI;

public class FilterPickPhoto : MonoBehaviour {
    private RawImage lastTouchedRawImage = null;

    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);

            child.GetComponent<Button>().onClick.AddListener(() => {
                lastTouchedRawImage = child.GetComponentInChildren<RawImage>();
                Texture image = lastTouchedRawImage.texture;

                Transform applyFilterScreen = transform.parent.Find("Body - Apply filter");
                applyFilterScreen.GetComponent<FilterApplyFilter>().originalImage = image;
                applyFilterScreen.gameObject.SetActive(true);

                gameObject.SetActive(false);
            });
        }
    }

    public void ApplyFilter(Texture newImage) {
        lastTouchedRawImage.texture = newImage;
    }

    // Update is called once per frame
    void Update() {

    }
}
