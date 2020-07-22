using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DefendButtonController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
    public PlayerInputController player;
    
	// Use this for initialization
	void Start () {
		
	}

    public virtual void OnPointerDown(PointerEventData ped)
    {
        player.setDefendiendo(true);
    }

    // this event happens when the touch (or mouse pointer) comes up and off the screen
    public virtual void OnPointerUp(PointerEventData ped)
    {
        player.setDefendiendo(false);
    }

}
