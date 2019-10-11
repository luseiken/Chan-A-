using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biology : MonoBehaviour
{
    [SerializeField] Pathfinding Pathfinding;

    Vector3 GoalPos;
    [SerializeField] float MoveSpeed;
    [SerializeField] Animator Animator;
    [SerializeField] float Step;
    [SerializeField] float Angle;
    public Transform firePoint;
    public Bullet bullet;
    public FireSpell fireSpell;
    bool IsMove;
    List<Node> Path;
    int PathIndex;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveMovement();
        AnimationManager();
        IsBackDected();

        float inputX = Angle;

        if (IsMove == true)
        {
            Animator.SetFloat("PosX", inputX);
        }

    }

    private void IsBackDected()
    {
        Vector3 moveVector = GoalPos - transform.position;
        Vector3 faceVector = transform.forward;
        Angle = Vector3.SignedAngle(moveVector, faceVector, Vector3.up);
    }

    /*void OnValidate()
    {
         Animator.SetFloat("Step", Step * MoveSpeed);

        // Animator.SetFloat("BackStep", BackStep * MoveSpeed);
    }*/

    private void MoveMovement()
    {
        if (Path == null || Path.Count == 0) return;
        IsMove = false;
        if (PathIndex >= Path.Count) return;
        Vector3 nodePos = Path[PathIndex].worldPosition;
        nodePos = new Vector3(nodePos.x, transform.position.y, nodePos.z);
        if (Vector3.Distance(transform.position, nodePos) < 0.25f) PathIndex++;
        GoalPos = nodePos;
        if (Vector3.Distance(transform.position, GoalPos) < 0.01f) return;
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) return;

        IsMove = true;
        transform.position = Vector3.MoveTowards(transform.position, GoalPos, MoveSpeed);

        Animator.SetFloat("Step", Step * MoveSpeed);
    }

    internal void Attack()
    {
        StopMove();
        //fixme:動畫相關的東西應該都集中到AnimationManager下控制
        Animator.SetTrigger("TriggerAttack");
    }
    internal void Fire()
    {
        Bullet newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as Bullet;
    }

    internal void Spell()
    {
        FireSpell newfireSpell = Instantiate(fireSpell, firePoint.position, firePoint.rotation) as FireSpell;
    }
    private void StopMove()
    {
        GoalPos = transform.position;

    }

    internal void MoveTo(Vector3 pos)
    {
        PathIndex = 0;
        Path = Pathfinding.GetPath(transform.position, pos);
    }

    internal void FaceTo(Vector3 pos)
    {
        pos = new Vector3(pos.x, transform.position.y, pos.z);
        transform.LookAt(pos);
    }

    private void AnimationManager()
    {
        /*
        if (IsMove) Animator.SetBool("IsWalk", true);
        if (!IsMove) Animator.SetBool("IsWalk", false);
        
        上面這段話可以簡化成下面這段：
        Animator.SetBool("IsWalk", IsMove);
        理解後請把我刪掉
         */
        Animator.SetBool("Grounded", IsMove);
    }
}
