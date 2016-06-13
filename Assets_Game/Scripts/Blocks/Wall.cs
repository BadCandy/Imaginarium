using UnityEngine;
using System.Collections;

namespace Blocks
{
    public class Wall : MonoBehaviour
    {
        /// <summary>
        /// If wall slides are allowed. A wall slide is when a motor slides down a wall. This will only take in effect
        /// once the stick is over.
        /// </summary>
        public bool overrideWallInteractions = false;

        /// <summary>
        /// If wall jumps are allowed.
        /// </summary>
        public bool enableWallJumps = true;

        /// <summary>
        /// The jump speed multiplier when wall jumping. This is useful to force bigger jumps off of the wall.
        /// </summary>
        public float wallJumpMultiplier = 1f;

        /// <summary>
        /// The angle (degrees) in which the motor will jump away from the wall. 0 is horizontal and 90 is straight up.
        /// </summary>
        [Range(0f, 90f)]
        public float wallJumpAngle = 70;

        /// <summary>
        /// If wall sticking is allowed. A wall sticking is when a motor will 'grab' a wall.
        /// </summary>
        public bool enableWallSticks = true;

        /// <summary>
        /// The duration of the wall sticks in seconds. Set to a very large number to effectively allow permanent sticks.
        /// </summary>
        public float wallSticksDuration = 1f;

        /// <summary>
        /// If wall slides are allowed. A wall slide is when a motor slides down a wall. This will only take in effect
        /// once the stick is over.
        /// </summary>
        public bool enableWallSlides = true;

        /// <summary>
        /// The speed that the motor will slide down the wall.
        /// </summary>
        public float wallSlideSpeed = 5;

        /// <summary>
        /// The time, in seconds, to get to wall slide speed.
        /// </summary>
        public float timeToWallSlideSpeed = 3;

        /// <summary>
        /// Are corner grabs allowed? A corner grab is when the motor sticks to a corner.
        /// </summary>
        public bool enableCornerGrabs = true;

        /// <summary>
        /// The duration, in seconds, that the motor will stick to a corner.
        /// </summary>
        public float cornerGrabDuration = 1f;

        /// <summary>
        /// The jump speed multiplier when jumping from a corner grab. Useful to forcing bigger jumps.
        /// </summary>
        public float cornerJumpMultiplier = 1f;

        /// <summary>
        /// This is the size of the corner check. This can be tweaked with if corner grabs are not working correctly.
        /// </summary>
        public float cornerDistanceCheck = 0.2f;

        /// <summary>
        /// This is the size of a valid check (normalized to collider height) that will consider wall interactions valid.
        /// Starts from the top of the collider and moves down.
        /// </summary>
        [Range(0.1f, 1f)]
        public float normalizedValidWallInteraction = 0.2f;

        /// <summary>
        /// After a corner or wall jump, this is how longer horizontal input is ignored.
        /// </summary>
        public float ignoreMovementAfterJump = 0.2f;

        /// <summary>
        /// Cooldown for allowing slides, sticks, and corner grabs. This may be necessary if the motor can slide down a vertical
        /// moving platform. If they don't exist then this can be 0.
        /// </summary>
        public float wallInteractionCooldown = 0.1f;

        /// <summary>
        /// The threshold that normalizedXMovement will have to be higher than to consider wall sticks, wall slides, wall jumps,
        /// and corner grabs.
        /// </summary>
        [Range(0f, 1f)]
        public float wallInteractionThreshold = 0.5f;
    }
}