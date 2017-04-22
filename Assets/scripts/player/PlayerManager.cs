using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlyingController))]
[RequireComponent(typeof(InPlanetController))]
[RequireComponent(typeof(PlatformerController))]

public class PlayerManager : MonoBehaviour {

    public enum PlayerState
    {
        Flying,
        InPlanet,
        Platformer
    }

    [SerializeField]
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
                break;
            case PlayerState.InPlanet:
                break;
            case PlayerState.Platformer:
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

    public void EnterPlanet()
    {
        //TODO position is hardcoded
        transform.position = Vector3.zero;
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

        CurrentState = PlayerState.Flying;
	}
}
