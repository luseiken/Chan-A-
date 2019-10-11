using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    public Transform Player;

    private void FixedUpdate()
    {

        Vector3 newPos = Player.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
