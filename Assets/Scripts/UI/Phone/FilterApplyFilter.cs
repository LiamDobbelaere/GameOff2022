using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
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
                SaneAudio.PlaySFX("phone.click-button");

                Texture filteredTexture = originalImage;

                foreach (Texture2D filteredPicture in filteredPictures) {
                    List<string> namePieces = new List<string>(originalImage.name.Split('_'));
                    namePieces.RemoveAt(namePieces.Count - 1);

                    string cutName = string.Join('_', namePieces);

                    if (filteredPicture.name == cutName + "_" + filterButton.name) {
                        filteredTexture = filteredPicture;
                        break;
                    }
                }

                string suffix = "";
                if (filteredTexture.name.Contains("hat")) {
                    suffix = "-hat";
                }
                DialogueLua.SetVariable("Chosen Photo For Hearthorn", GameStateMono.instance.lastChosenFilterPhoto + suffix);

                SetShownImageTo(filteredTexture);
            });
        }

        transform.Find("Confirm").GetComponent<Button>().onClick.AddListener(() => {
            SaneAudio.PlaySFX("phone.click-button");

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
