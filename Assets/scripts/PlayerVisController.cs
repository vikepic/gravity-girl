using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisController : MonoBehaviour {

    public SpriteRenderer _renderer;

    void Awake () {
        _renderer = GetComponent<SpriteRenderer>();

    }

    private static PlayerVisController _instance;
    public static PlayerVisController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (PlayerVisController)FindObjectOfType(typeof(PlayerVisController));
            }
            return _instance;
        }
    }
}
