using UnityEngine;

public class InternetFavorites : MonoBehaviour {
    public GameObject body;
    public GameObject wikiBody;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void OnOpenWikiPage() {
        body.SetActive(false);
        wikiBody.SetActive(true);
    }
}
