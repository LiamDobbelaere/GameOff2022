using PixelCrushers.DialogueSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    private float walkSpeed = 1.2f;
    private Vector3 originalScale;

    private GameObject playerScaleTargetMin = null;
    private GameObject playerScaleTargetMax = null;

    // Start is called before the first frame update
    void Start() {
        this.rb = GetComponent<Rigidbody2D>();

        originalScale = transform.localScale;

        GetPlayerScaleTargets();

        GameState.inPhotographyMode = false;

        if (GameState.isFirstTime) {
            AddMechanicsExplanationNotification();
            GameState.isFirstTime = false;
        }
        DisableDoingFlagInterviewsWhenDone();
        RestoreLastPositionIfSet();
        RestoreMarkerPositionIfSet();
        AddInterviewsNotificationIfNeeded();
    }

    private void Update() {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * walkSpeed, Input.GetAxisRaw("Vertical") * walkSpeed);

        UpdateScale();
    }

    // Update is called once per frame
    void FixedUpdate() {

    }

    private void RestoreLastPositionIfSet() {
        if (GameState.lastPosition == null) {
            return;
        }

        transform.position = (Vector2)GameState.lastPosition;
        GameState.lastPosition = null;
    }

    private void RestoreMarkerPositionIfSet() {
        if (GameState.targetMarker == null) {
            return;
        }

        GameObject marker = GameObject.Find(GameState.targetMarker);
        if (marker == null) {
            Debug.LogError("Marker not found for name " + GameState.targetMarker);
        }

        transform.position = (Vector2)marker.transform.position;

        GameState.targetMarker = null;
    }

    private void DisableDoingFlagInterviewsWhenDone() {
        if (DialogueLua.GetVariable("Flag - Interviews done").asInt >= 2) {
            DialogueLua.SetVariable("Doing Interviews", false);
        }
    }

    private void AddMechanicsExplanationNotification() {
        GameState.AddNotification("The goal is simple. Make the most cliche pirate documentary ever. Or go completely wild.");
        GameState.AddNotification("Speak to the crew, use your apps to assist you, and once you've plotted and schemed, talk to Council Chair Coldwell.");
        GameState.AddNotification("After each event, you can then interview several pirates to create a unique narrative.");
        GameState.AddNotification("There are many branching paths, and everything will stick together at the end to make your hopefully epic documentary.");
        GameState.AddNotification("You can look at your checklist. The main objectives are in order, whilst the side objectives can be completed at any time. Everything will stick together at the end, and you'll have a full documentary based on your decisions.");
    }

    private void AddInterviewsNotificationIfNeeded() {
        if (GameState.justSawTheFlag) {
            GameState.AddNotification("Interview some people about the flag! Not everyone will have something to say.");
            GameState.justSawTheFlag = false;
        }
    }

    private void GetPlayerScaleTargets() {
        GameObject[] scaleTargets = GameObject.FindGameObjectsWithTag("PlayerScaleTarget");

        if (scaleTargets.Length > 2) {
            Debug.LogError("Too many scale targets!");
            return;
        } else if (scaleTargets.Length == 2) {
            playerScaleTargetMin = scaleTargets[0];
            playerScaleTargetMax = scaleTargets[1];

            if (playerScaleTargetMin.transform.position.y < playerScaleTargetMax.transform.position.y) {
                playerScaleTargetMin = scaleTargets[1];
                playerScaleTargetMax = scaleTargets[0];
            }

            playerScaleTargetMin.GetComponent<SpriteRenderer>().enabled = false;
            playerScaleTargetMax.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void UpdateScale() {
        if (playerScaleTargetMin == null || playerScaleTargetMax == null) {
            transform.localScale = originalScale;
            return;
        } else {
            float yDistance = Vector2.Distance(playerScaleTargetMin.transform.position, playerScaleTargetMax.transform.position);
            float playerDistanceFromMin = Vector2.Distance(transform.position, playerScaleTargetMin.transform.position);
            float lerpFactor = Mathf.Clamp01(playerDistanceFromMin / yDistance);

            transform.localScale = Vector3.Lerp(playerScaleTargetMin.transform.localScale, playerScaleTargetMax.transform.localScale, lerpFactor);
        }
    }
}
