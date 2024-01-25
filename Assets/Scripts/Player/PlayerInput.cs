using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public static PlayerInput Instance;
    public PlayerInputActions actions;

    void Awake() {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        Instance = this;

        actions = new();
        actions.Enable();
    }
}
