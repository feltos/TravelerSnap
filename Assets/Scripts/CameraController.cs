using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform player;
    Vector3 offset;
    Vector3 currentOffset;
    float horizontalMouse;
    float horizontalJoystick;
    float offsetMag;
    float posY;
    Vector3 playerPosition;

    int xboxOneController = 0;
    int ps4Controller = 0;

	void Start ()
    {
        offset = player.position - transform.position;
        posY = transform.position.y;
    }
	
	
	void Update ()
    {
        playerPosition = player.position;
        playerPosition.y = posY;
        horizontalMouse = Input.GetAxis("Mouse X");
        horizontalJoystick = Input.GetAxisRaw("Vertical2");

        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            print(names[x].Length);

            if (names[x].Length == 19)
            {
                print("PS4 CONTROLLER IS CONNECTED");
                ps4Controller = 1;
                xboxOneController = 0;
            }

            if (names[x].Length == 33)
            {
                print("XBOX ONE CONTROLLER IS CONNECTED");
                //set a controller bool to true
                ps4Controller = 0;
                xboxOneController = 1;
            }
        }

        if (xboxOneController == 1)
        {
            transform.RotateAround(player.position, Vector3.down, horizontalJoystick * 60 * Time.deltaTime);
        }
        else if (ps4Controller == 1)
        {
            //do something
        }
        else
        {
            transform.RotateAround(player.position, Vector3.down, -horizontalMouse * 60 * Time.deltaTime);
        }

        currentOffset = playerPosition - transform.position;
        offsetMag = offset.magnitude / currentOffset.magnitude;
        currentOffset = currentOffset * offsetMag;
        transform.position = playerPosition - currentOffset;
        transform.LookAt(playerPosition);
        
    }
}
