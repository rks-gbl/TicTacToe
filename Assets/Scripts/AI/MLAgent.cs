using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Policies;
using Unity.Barracuda;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MLAgent : Agent
{
    public bool Initialized;
    public BehaviorParameters behaviorParameters;

    public void InitializeModel(NNModel model)
    {
        Initialized = true;
        behaviorParameters.Model = model;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        base.CollectObservations(sensor);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        base.Heuristic(actionsOut);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        base.OnActionReceived(actions);
    }
}