using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Review {
    public string username;
    public float rating;
    public string message;
}

public class Reviews : MonoBehaviour {
    public Review[] reviews;

    // Start is called before the first frame update
    void Start() {
        Transform reviewTemplate = transform.Find("ReviewTemplate");
        reviewTemplate.gameObject.SetActive(false);

        foreach (Review review in reviews) {
            Transform newReview = Instantiate(reviewTemplate, transform);

            newReview.Find("Header/Author").GetComponent<TextMeshProUGUI>().text = review.username;
            newReview.Find("Header/RatingBack/RatingFilled").GetComponent<Image>().fillAmount = review.rating / 5f;
            newReview.Find("Body/Message").GetComponent<TextMeshProUGUI>().text = review.message;

            newReview.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
