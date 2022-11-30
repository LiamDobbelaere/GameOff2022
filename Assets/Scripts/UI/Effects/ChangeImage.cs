using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour {
    public Sprite targetSprite;
    public float changeTime;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0f) {
            GetComponent<Image>().sprite = targetSprite;
        }
    }
}
