using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class touchController : MonoBehaviour
{
    public squidScript squid;

    // Start is called before the first frame update
    void Start()
    {
        EnhancedTouchSupport.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if(squid.isAlive){
            // gets each active touch on the screen
            foreach(Finger eachFinger in Touch.activeFingers)
            {
                Touch touch = eachFinger.currentTouch;

                if(touch.phase == TouchPhase.Moved){
                    // detects the direction of the player's finger
                    UnityEngine.Vector2 dragDirection = (touch.screenPosition - touch.startScreenPosition) /Screen.dpi;

                    // moves the squid in the drag direction
                    if(squid != null){
                        squid.moveWithVector(dragDirection);
                    }
                    //Debug.Log(dragDirection);
                }

                if(touch.phase == TouchPhase.Ended){
                    if(squid != null){
                        squid.moveWithVector(UnityEngine.Vector2.zero);
                    }
                }
            }
        }
        else{
            // if the squid is dead, its movement is stopped
            squid.moveWithVector(UnityEngine.Vector2.zero);
        }
    }
}
