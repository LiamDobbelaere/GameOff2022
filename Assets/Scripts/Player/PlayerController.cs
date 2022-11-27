using PixelCrushers.DialogueSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    private float walkSpeed = 1.2f;

    // Start is called before the first frame update
    void Start() {
        this.rb = GetComponent<Rigidbody2D>();

        GameState.inPhotographyMode = false;

        if (GameState.isFirstTime) {
            AddMechanicsExplanationNotification();
            GameState.isFirstTime = false;
        }
        DisableDoingFlagInterviewsWhenDone();
        RestoreLastPositionIfSet();
        RestoreMarkerPositionIfSet();
    }

    private void Update() {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * walkSpeed, Input.GetAxisRaw("Vertical") * walkSpeed);
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
}
