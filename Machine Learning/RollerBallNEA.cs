using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

// Class definition for the Agent
public class RollerBallNEA : Agent
{
    // Member variable for storing the Rigidbody component
    Rigidbody rBody;

    // Start method is called when the Agent starts
    void Start () {
        // Get the Rigidbody component attached to the Agent
        rBody = GetComponent<Rigidbody>();
    }

    // Public variable to store the Target Transform component
    public Transform Target;

    // Method called at the beginning of each episode
    public override void OnEpisodeBegin()
    {
        // If the Agent's y position is less than 0, zero its momentum and move it back to a safe position
        if (this.transform.localPosition.y < 0)
        {
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3( 0, 0.5f, 0);
        }

        // Move the Target to a new random location
        Target.localPosition = new Vector3(Random.value * 12 - 4,
                                           0.5f,
                                           Random.value * 12 - 4);
    }

    // Method for collecting observations
    public override void CollectObservations(VectorSensor sensor)
    {
        // Observe the Target and Agent's positions
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Observe the Agent's velocity
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    }

    // Member variable to store the force multiplier
    public float forceMultiplier = 10;

    // OnActionReceived function is called whenever an action is received for the agent.
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Get the action and convert it to control signal for the Agent's movement
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0]; // the first continuous action, representing the horizontal movement
        controlSignal.z = actionBuffers.ContinuousActions[1]; // the second continuous action, representing the vertical movement
        rBody.AddForce(controlSignal * forceMultiplier); // add the force to the rigid body component of the agent, multiplied by a force multiplier

        // Calculate the distance between the Agent and Target
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

        // If the Agent reaches the Target, provide a positive reward and end the episode
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f); // set a reward of 1.0
            EndEpisode(); // end the current episode
        }

        // If the Agent falls off the platform, end the episode
        else if (this.transform.localPosition.y < 0)
        {
            EndEpisode(); // end the current episode
        }
    }

    // Heuristic function is used to provide human control over the agent
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal"); // get the horizontal input from the user
        continuousActionsOut[1] = Input.GetAxis("Vertical"); // get the vertical input from the user
    }
}
