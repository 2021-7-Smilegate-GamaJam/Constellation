using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EState { E_IDLE, E_JUMP, E_ATTACK, E_SLIDE };


public class PlanetButtonHandle : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 startTouchPos;
    private Vector2 currentTouchPos;
    private Vector2 currentAtkPos;

    private StateMachine stateMachine;
    [SerializeField] private Animator playerAnim;       //나중에 그냥 find로 처리

    public static EState eState;
    public EState eState_
    {
        get
        {
            return eState;
        }
        set
        {
            if (eState.Equals(value)) return;

            ExitState(eState);
            eState = value;

            EnterState(eState);
        }
    }


    void Awake()
    {
        stateMachine = new StateMachine(new IdleState());
    }

    public void OnPointerDown(PointerEventData _eventData)
    {
        startTouchPos = _eventData.position;
        currentAtkPos = startTouchPos;
    }
    
    public void OnDrag(PointerEventData _eventData)
    {
        currentTouchPos = _eventData.position;
        Vector2 dirVec = currentTouchPos - startTouchPos;
        Vector2 normDirVec = dirVec.normalized;
        float distance = (currentTouchPos - startTouchPos).sqrMagnitude;

        if (distance > 50f)
        {
            if (0.9f < normDirVec.y && normDirVec.y <= 1f)
            {
                stateMachine.SetState(new JumpState());
            }
            else if (-1f <= normDirVec.y && normDirVec.y < -0.9f)
            {
                stateMachine.SetState(new SlideState());
            }
        }
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
        currentAtkPos = _eventData.position;

        if ((currentAtkPos - startTouchPos).sqrMagnitude < 0.1f)
        {
            stateMachine.SetState(new AttackState());
        }
        else
        {
            eState_ = EState.E_IDLE;
        }

    }
}


#region 상태 변화.. 그냥 심심해서
public enum EState { E_IDLE, E_JUMP, E_ATTACK, E_SLIDE };
//상세 조건은 아직

public abstract class IState
{
    public abstract void OnEnter();
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }
}

public class StateMachine
{
    public IState state { get; private set; }

    public StateMachine(IState _state)
    {
        this.state = _state;
    }

    public void SetState(IState _state)
    {
        switch (_eState)
        {
            case EState.E_IDLE:

                break;
            case EState.E_JUMP:
                StartCoroutine(Action("jump"));
                break;
            case EState.E_ATTACK:
                StartCoroutine(Action("attack"));
                break;
            case EState.E_SLIDE:
                StartCoroutine(Action("sliding"));
                break;
            default:
                break;
        }
    }

    IEnumerator Action(string _paramName)
    {
        Debug.Log(_paramName);
        playerAnim.SetTrigger(_paramName);

        //yield return new WaitUntil(() => playerAnim.IsInTransition(0));
        yield return new WaitForSeconds(0.1f);

        if(eState == EState.E_ATTACK)
            eState_ = EState.E_IDLE;
    }

    //exit에서 할게 있나
    public void ExitState(EState _eState)
    {
        if (state.Equals(_state))
        {
            return;
        }

        _state.OnExit();
        this.state = _state;
        state.OnEnter();
    }
}

public class IdleState : IState
{
    public override void OnEnter()
    {
        Debug.Log("원상태");
    }
}

public class JumpState : IState
{
    public override void OnEnter()
    {
        Debug.Log("점프");
    }
}

public class AttackState : IState
{
    public override void OnEnter()
    {
        Debug.Log("공격");
    }
}

public class SlideState : IState
{
    public override void OnEnter()
    {
        Debug.Log("슬라이드");
    }
}

#endregion
