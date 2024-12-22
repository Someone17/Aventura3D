using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

namespace Ebac.StateMachine
{

/*public class Test 
{
    public enum Test2
    {
        NONE
    }
    public void Aa(){
        StateMachine<Test2> StateMachine = new StateMachine<Test2>();

        StateMachine.RegisterStates(Test.Test2.NONE, new StateBase());
    }
}*/

public class StateMachine<T> where T : System.Enum 
{
    
    public Dictionary<T, StateBase> dictionaryState;
    
    private StateBase _currentState;
    public float timeToStartGame = 1f;
    
    public StateBase CurrentState
    {
        get{return _currentState;}
    }

    /*public StateMachine(T state){
        SwitchState(state);
    }*/
    
    public void Init(){
        
        dictionaryState = new Dictionary<T, StateBase>();

    }

    public void RegisterStates(T typeEnum, StateBase state)
    {
        dictionaryState.Add(typeEnum, state);

        /*SwitchState(States.NONE);

        Invoke(nameof(StartGame), timeToStartGame);*/
    }

   /*[Button]
    private void StartGame()
    {
       SwitchState(States.NONE);
       RegisterStates();
    }

    [Button]
    private void ChangeStateToStateX(){
        SwitchState(States.NONE);
    //}

    //[Button]
    //private void ChangeStateToStateY(){
     //  SwitchState(States.NONE);
    }*/
    
    public void SwitchState(T state){
        
        if(_currentState != null) _currentState.OnStateExit();

        _currentState = dictionaryState[state];
        
        _currentState.OnStateExit();
    }

    public void Update(){
        if(_currentState != null) _currentState.OnStateStay();

        /*if(Input.GetKeyDown(KeyCode.O)){
            SwitchState(States.DEAD);
        }*/
    }
}
}
