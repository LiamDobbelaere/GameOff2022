using UnityEngine;

public class GlowingFlagBase : MonoBehaviour {
    public JollyRogerCheck jollyRogerCheck;

    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start() {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        Color col = rend.color;

        if (jollyRogerCheck.isJollyRoger) {
            col.a = Mathf.Abs(Mathf.Sin(Time.time)) * 0.5f;
        } else {
            col.a = 0f;
        }

        rend.color = col;
    }
}
