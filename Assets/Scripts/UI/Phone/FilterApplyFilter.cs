using UnityEngine;
using UnityEngine.UI;

public class FilterApplyFilter : MonoBehaviour {
    public Texture originalImage;
    public Texture2D[] filteredPictures;

    // Start is called before the first frame update
    void Start() {
        Transform filterOptions = transform.Find("FilterOptions");
        for (int i = 0; i < filterOptions.childCount; i++) {
            Transform filterButton = filterOptions.GetChild(i);

            filterButton.GetComponent<Button>().onClick.AddListener(() => {
                Texture filteredTexture = originalImage;

                if (filterButton.name != "none") {
                    foreach (Texture2D filteredPicture in filteredPictures) {
                        if (filteredPicture.name == originalImage.name + "_" + filterButton.name) {
                            filteredTexture = filteredPicture;
                            break;
                        }
                    }
                }

                SetShownImageTo(filteredTexture);
            });
        }

        transform.Find("Confirm").GetComponent<Button>().onClick.AddListener(() => {
            transform.parent.Find("Body - Pick photo").gameObject.SetActive(true);
            transform.parent.Find("Body - Pick photo").GetComponent<FilterPickPhoto>().ApplyFilter(
                transform.Find("RawImage").GetComponent<RawImage>().texture
            );
            gameObject.SetActive(false);
        });
    }

    private void OnEnable() {
        SetShownImageTo(originalImage);
    }

    private void SetShownImageTo(Texture texture) {
        transform.Find("RawImage").GetComponent<RawImage>().texture = texture;
    }

    // Update is called once per frame
    void Update() {

    }
}
