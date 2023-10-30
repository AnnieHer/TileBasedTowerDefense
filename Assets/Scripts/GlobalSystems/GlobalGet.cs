using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGet : MonoBehaviour
{
    private Canvas canvas;
    [SerializeField] private UIContainer debugControls;
    [SerializeField] private UIContainer playerControls;
    public static GlobalGet instance;
    void Awake()
    {
        if (instance != null) {
            Destroy(this);
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        canvas = this.GetComponent<Canvas>();
    }
    public Canvas GetCanvas() {
        return canvas;
    }
    public UIContainer getPlayerControls() {
        return playerControls;
    }
    public UIContainer getDebugControls() {
        return debugControls;
    }
}
