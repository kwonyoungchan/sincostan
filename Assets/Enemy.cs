using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject target;
    
    //Enemy의 초기위치를 저장하기 위한 변수 선언
    Vector3 setPosition ;
    Vector3 targetPosition ;
    
    
    //상태머신 선언부
    public enum State
    {
        Idle,
        Move,
        Attack,
        Return,
    }
    public State state;
    //Enemy의 감지거리
    private float findDistance=10;
    //Enemy의 속도
    private float speed=3;
    //Enemy의 추적거리
    private float traceDistance=12;
    //Enemy의 공격거리
    public float AttackDistance=1.5f;
    float currentTime;
    float attackTime=1;
    // 플레이어와 적 사이 각도 
     float angle;

    
    
    void Start()
    {
        state=State.Idle;
        target=GameObject.Find("Player");
        //초기위치를 저장한다.
        setPosition=transform.position;
        

    }

   
    void Update()
    {
        if (state==State.Idle)
        {
            UpdateIdle();
        }
        else if (state==State.Move)
        {
            UpdateMove();
        }
        else if (state==State.Attack)
        {
            UpdateAttack();
        }
        else if (state ==State.Return)
        {
            UpdateReturn();
        }

        //------------------------------------------------------------------//
        //-----------------------감지 각도 정의 부분-------------------------//
        //------------------------------------------------------------------//

        targetPosition = target.transform.position-transform.position;
        angle = Vector3.Angle(targetPosition,transform.forward);
        print("현재 각도: "+angle);
        
        //------------------------------------------------------------------//
        
    }
    
    
    private void UpdateIdle()
    {
        
        float distance=Vector3.Distance(transform.position,target.transform.position);

        //----------------플레이어와 적의 각도가 45 degree에 들어왔을 경우----//
        if(angle<45&&angle>0)
        {   
            print("감지범위에 들어옴");
            if (distance<findDistance)
            {
            //상태를 Move로 전이
            state=State.Move;
            }
        }
    }

        //------------------------------------------------------------------//
    
    private void UpdateMove()
    {
        
        //target 방향으로 이동하다가 target공격거리안에 들어오면 Attack으로 전이
        Vector3 dir= target.transform.position-transform.position;
        dir.Normalize();
        transform.position+=dir*speed*Time.deltaTime;
        float distance=Vector3.Distance(transform.position,target.transform.position);
        if (distance<AttackDistance)
        {
            state=State.Attack;
        }
        // 만약 target이 감지거리 이상으로 멀어질 경우 Return 으로 전이
        else if (distance>traceDistance)
        {
            state=State.Return;
        }
        
    }
    
    private void UpdateAttack()
    {
        currentTime+=Time.deltaTime;
        if(currentTime>attackTime)
        {
            currentTime=0;
            float distance=Vector3.Distance(transform.position,target.transform.position);
            if (distance>AttackDistance)
        {
            state=State.Move;
        }
        
            
            
        }
        
    }
    private void UpdateReturn()
    {
        Vector3 dir= setPosition-transform.position;
        dir.Normalize();
        transform.position+=dir*speed*Time.deltaTime;
        //현재위치와 초기위치 사이의 거리
        float distance = Vector3.Distance(transform.position,setPosition);
        // 만약 현재위치가 초기위치에 근접했을 때
        if(distance>=0&&distance<=0.1)          //<== 복귀할 시 정확한 좌표로 이동할 수 없으므로 일정거리 근접하면 강제 이동되게 구현함
        {
            //현재위치를 초기위치로 강제 이동
            transform.position=setPosition;
            //상태를 Idle로 전이
            state=State.Idle;
        }
    }
    
    
    

    

    
}
