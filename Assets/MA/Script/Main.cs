using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] Camera Camera;
    [SerializeField] CameraFollow CameraFollow;
    [SerializeField] Biology Player;
    [SerializeField] Transform Beacon;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputManager();
    }

    private void InputManager()
    {
        //fixme:一直打射線浪費效能待優化            
        RaycastHit _hit;
        Ray _ray = Camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(_ray, out _hit);


        if (Input.GetKey(KeyCode.LeftShift) == false && Input.GetMouseButtonDown(0))
        {
            Vector3 pos = _hit.point;
            Beacon.position = pos;
            Player.FaceTo(pos);
            Player.MoveTo(pos);

        }

        /*if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonDown(0))
		{
			Player.Attack();
		}*/

        if (Input.GetMouseButtonDown(1))
        {
            Player.Attack();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Player.Spell();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            CameraFollow.MouseWheelZoomIn();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            CameraFollow.MouseWheelZoomOut();
        }

    }
}
