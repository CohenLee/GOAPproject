using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class GOAPMyAgent : MonoBehaviour {

	// Use this for initialization

    private MyFSM stateMachine;

    private MyFSM.MyFSMState idleState; // 闲置状态，找一些事情做
    private MyFSM.MyFSMState moveToState; // 移动到一个目标
    private MyFSM.MyFSMState performActionState; // 执行一个动作
    
    //代理所有的行为
    private HashSet<GOAPBaseAction> availableActions;
    //规划器当前的行为
    private Queue<GOAPBaseAction> currentActions;

    // 提供世界数据，监听反馈
    private IGoap dataProvider;
    //规划器
    private GoapPlanner planner;

	void Start () {
        stateMachine = new MyFSM();
        availableActions = new HashSet<GOAPBaseAction>();
        currentActions = new Queue<GOAPBaseAction>();
        //planner = new GoapPlanner();
        //findDataProvider();
        createIdleState();
        createMoveToState();
        createPerformActionState();
        stateMachine.pushState(idleState);
        //loadActions();
	}

    void createPerformActionState()
    {
        throw new System.NotImplementedException();
    }

    void createMoveToState()
    {
        throw new System.NotImplementedException();
    }

    void createIdleState()
    {
        idleState = (fsm, gameObj) =>
        {
            // GOAP planning

            // get the world state and the goal we want to plan for
            HashSet<KeyValuePair<string, object>> worldState = dataProvider.getWorldState();
            HashSet<KeyValuePair<string, object>> goal = dataProvider.createGoalState();

            // Plan
            Queue<GoapAction> plan = planner.plan(gameObject, availableActions, worldState, goal);
            if (plan != null)
            {
                // we have a plan, hooray!
                currentActions = plan;
                dataProvider.planFound(goal, plan);

                fsm.popState(); // move to PerformAction state
                fsm.pushState(performActionState);

            }
            else
            {
                // ugh, we couldn't get a plan
                Debug.Log("<color=orange>Failed Plan:</color>" + prettyPrint(goal));
                dataProvider.planFailed(goal);
                fsm.popState(); // move back to IdleAction state
                fsm.pushState(idleState);
            }

        };
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
