using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlyingController))]
[RequireComponent(typeof(InPlanetController))]
[RequireComponent(typeof(PlatformerController))]

public class PlayerManager : MonoBehaviour {
    public static int objectives = 0;
    private GameObject textGo;
    public enum PlayerState
    {
        Flying,
        InPlanet,
        Platformer
    }

    [SerializeField]
    private PlayerState startingState;

    private PlayerState currentState;

    // Every state controller should be assigned to its
    // equivalent state
    // i.e.
    // stateControllers[PlayerState.Flying] = FlyingController
    private StateController[] stateControllers;

    private void ChangeState(PlayerState newState)
    {
        // What we're doing here is basically disable all
        // state controllers
        foreach(StateController sc in stateControllers)
        {
            sc.enabled = false;
        }
        // And then activate the one we want to transition to
        stateControllers[(int)newState].enabled = true;

        switch (newState)
        {
            case PlayerState.Flying:
                AudioManager.Instance.SetState(0);
                break;
            case PlayerState.InPlanet:
                AudioManager.Instance.SetState(0);
                break;
            case PlayerState.Platformer:
                AudioManager.Instance.SetState(0);
                break;
            default:
                break;
        }

        currentState = newState;
    }

    public PlayerState CurrentState
    {
        get
        {
            return currentState;
        }

        set
        {
            ChangeState(value);
        }
    }

    public void OnPlanetCollision(GameObject planetPivot)
    {
        ChangeState(PlayerState.InPlanet);
        stateControllers[(int)PlayerState.InPlanet].SendMessage(
            "ResetPivot", planetPivot);
    }

    public void ExitPlanet()
    {
        ChangeState(PlayerState.Flying);
    }

    public void ExitLevel(Vector3 exitPos)
    {
        transform.position = exitPos;
        transform.rotation = Quaternion.identity;
        ChangeState(PlayerState.Flying);
    }

    public void EnterPlanet(Vector3 entryPosition)
    {
        transform.position = entryPosition;
        transform.rotation = Quaternion.identity;
        ChangeState(PlayerState.Platformer);
    }

    void Start () {
        stateControllers = new StateController[3];
        stateControllers[(int)PlayerState.Flying] = 
            gameObject.GetComponent<FlyingController>();
        stateControllers[(int)PlayerState.InPlanet] =
            gameObject.GetComponent<InPlanetController>();
        stateControllers[(int)PlayerState.Platformer] =
            gameObject.GetComponent<PlatformerController>();

        CurrentState = startingState;
        textGo = GameObject.Find("winGameObj");
        textGo.SetActive(false);
	}

    private static PlayerManager _instance;
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (PlayerManager)FindObjectOfType(typeof(PlayerManager));
            }
            return _instance;
        }
    }

    public void objectiveCompleted()
    {
        objectives++;
        // Ending music
        AudioManager.Instance.SetState(2);
        if (objectives == 4)
        {
            AudioManager.Instance.PlayVictoryMusic();
             textGo.SetActive(true);
        }
    }
}
