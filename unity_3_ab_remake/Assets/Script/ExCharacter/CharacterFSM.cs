using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum CharacterState
{
    Idle,
    WalkingToshelf,
    PickingItem,
    WalkingToCounter,
    PlacingItem
}

public class Timer  //커스텀 타이머 class
{
    private float timeRemining;  //타이머 Float
    
    public void Set(float time)  //시간 설정
    {
        timeRemining = time;
    }

    public void Update(float deltaTime)  //업데이트 동기화
    {
        if (timeRemining > 0)
        {
            timeRemining -= deltaTime;
        }
    }

    public bool IsFinished()  //종료 체크
    {
        return timeRemining <= 0;
    }
}
public class CharacterFSM : MonoBehaviour
{
    public CharacterState currentState;  //현재 캐릭어 상태
    private Timer timer;                 //타이머 선언

    public NavMeshAgent agent;           //에이전트 붙이기
    public Transform target;

    public Transform Counter;
    public List<GameObject> targetPos = new List<GameObject>();

    private static int NextPriority = 0;                          //다음 에이전트위 우선 순위
    private static readonly object priorityLock = new object();   //우선 순위; 할당을 위한 동기화 객체

    public bool isMoveDone = false;                               //도착 판별용 Bool

    public List<GameObject> myBox = new List<GameObject>();
    
    public int boxesTopick = 5;
    private int boxesPicked = 0;

    void AssignPriority()
    {
        lock(priorityLock)
        {
            agent.avoidancePriority = NextPriority;
            NextPriority = (NextPriority + 1) % 100;              //NavMeshAgent 우선순위 범위는 0 ~ 99
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        timer = new Timer();                 //타이머 할당
        currentState = CharacterState.Idle;  //캐릭터 상태 설정
        agent = GetComponent<NavMeshAgent>();
        AssignPriority();
    }

    // Update is called once per frame
    void Update()
    {
        timer.Update(Time.deltaTime);        //타이머 업데이트와 동기화

        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if(!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                isMoveDone = true;
            }
        }

        switch (currentState)
        {
            case CharacterState.Idle:
                Idle();
                break;
            case CharacterState.WalkingToshelf:
                WalkingToshelf();
                break;
            case CharacterState.PickingItem:
                PickingItem();
                break;
            case CharacterState.WalkingToCounter:
                WalkingToCounter();
                break;
            case CharacterState.PlacingItem:
                placingItem();
                break;
        }
    }

    void MoveToTarget()
    {
        isMoveDone = false;

        if(target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    void ChangeState(CharacterState nextState, float waitTime = 0.0f)  //FSM Stat 전환시 다음 스테이트의 타이머 시간 설정
    {
        currentState = nextState;
        timer.Set(waitTime);
    }

    void Idle()
    {
        if(timer.IsFinished())
        {
            target = targetPos[Random.Range(0, targetPos.Count)].transform;
            MoveToTarget();
            ChangeState(CharacterState.WalkingToshelf, 2.0f);
        }
    }

    void WalkingToshelf()
    {
        if (timer.IsFinished() && isMoveDone)
        {
            ChangeState(CharacterState.PickingItem, 2.0f);
            boxesPicked = 0;
        }
    }

    void PickingItem()
    {
        if (timer.IsFinished())
        {
            if(boxesPicked < boxesTopick)
            {
                GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                myBox.Add(box);
                box.transform.parent = gameObject.transform;
                box.transform.localEulerAngles = Vector3.zero;
                box.transform.localPosition = new Vector3(0, boxesPicked * 2f, 0);

                boxesPicked++;
                timer.Set(0.5f);
            }
            else
            {
                target = Counter;
                MoveToTarget();
                ChangeState(CharacterState.WalkingToCounter, 2.0f);

            }
        }
    }

    void WalkingToCounter()
    {
        if (timer.IsFinished() && isMoveDone)
        {
            ChangeState(CharacterState.PlacingItem, 2.0f);
        }
    }

    void placingItem()
    {
        if (timer.IsFinished())
        {
            if (myBox.Count != 0)
            {
                myBox[0].transform.position = Counter.transform.position;
                myBox[0].transform.parent = Counter.transform;
                myBox.RemoveAt(0);
                timer.Set(0.1f);
            }
            else
            {
                ChangeState(CharacterState.Idle, 2.0f);
            }
        }
    }
}
