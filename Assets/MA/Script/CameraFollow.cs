using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] GameObject Target;
    [SerializeField] float CamZoomSpeed;
    [SerializeField] float ScrollWheelSpeed;
    [SerializeField] Camera Camera;
    [SerializeField] float MaxSize;
    [SerializeField] float MiniSize;
    [SerializeField] float FollowSpeed;
    private Vector3 offset;
    private float OrthographicSizeTarget;
    private float currentVelocity;
    private Vector3 Velocity;



    // Start is called before the first frame update
    void Start()
    {
        OrthographicSizeTarget = Camera.orthographicSize;
        offset = transform.position - Target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
        SmoothZoom();

    }

    private void SmoothZoom() //SmoothDamp是拉遠拉近速度
    {
        if (Mathf.Abs(Camera.orthographicSize - OrthographicSizeTarget) < 0.1f) return;
        Camera.orthographicSize = Mathf.SmoothDamp(Camera.orthographicSize, OrthographicSizeTarget, ref currentVelocity, CamZoomSpeed);
    }

    internal void MouseWheelZoomIn() //Camera.orthographicSize API 基本上就是使遊戲畫面維持原來比例進行縮放 影片https://www.youtube.com/watch?v=3xXlnSetHPM
    {
        float _OrthographicSizeTarget = Camera.orthographicSize + ScrollWheelSpeed;
        if (_OrthographicSizeTarget < MaxSize) OrthographicSizeTarget = _OrthographicSizeTarget;

    }
    internal void MouseWheelZoomOut()
    {
        float _OrthographicSizeTarget = Camera.orthographicSize - ScrollWheelSpeed;
        if (_OrthographicSizeTarget > MiniSize) OrthographicSizeTarget = _OrthographicSizeTarget;
    }


    private void FollowTarget() //鏡頭追隨  offset是指攝影鏡頭與玩家距離  SmoothDamp是控制跟隨速度
    {
        Vector3 targetPos = Target.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref Velocity, FollowSpeed);
    }
}
