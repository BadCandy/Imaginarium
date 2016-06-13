using System.Text;
using System.Collections;
using UnityEngine;
using UnityEditor;
using Blocks;

[CustomEditor(typeof(Wall))]
[CanEditMultipleObjects]
public class ObstacleEditor : ExtendedEditor
{
    private static bool _showWallInteractions;
    private static bool _showInformation;

    #region Properties
    private readonly Property OVERRIDE_WALL_INTERACTIONS = new Property("overrideWallInteractions", "Override Wall Interactions");

    private readonly Property ENABLE_WALL_JUMPS = new Property("enableWallJumps", "Enable Wall Jumps");
    private readonly Property WALL_JUMP_MULTIPLIER = new Property("wallJumpMultiplier", "Wall Jump Multiplier");
    private readonly Property WALL_JUMP_DEGREE = new Property("wallJumpAngle", "Wall Jump Angle (Degrees)");

    private readonly Property ENABLE_WALL_STICKS = new Property("enableWallSticks", "Enable Wall Sticks");
    private readonly Property WALL_STICK_DURATION = new Property("wallSticksDuration", "Wall Stick Duration");

    private readonly Property ENABLE_WALL_SLIDES = new Property("enableWallSlides", "Enable Wall Slides");
    private readonly Property WALL_SLIDE_SPEED = new Property("wallSlideSpeed", "Wall Slide Speed");
    private readonly Property TIME_TO_WALL_SLIDE_SPEED = new Property("timeToWallSlideSpeed", "Time to Wall Slide Speed");

    private readonly Property ENABLE_CORNER_GRABS = new Property("enableCornerGrabs", "Enable Corner Grabs");
    private readonly Property CORNER_JUMP_MULTIPLIER = new Property("cornerJumpMultiplier", "Corner Jump Multiplier");
    private readonly Property CORNER_GRAB_DURATION = new Property("cornerGrabDuration", "Corner Grab Duration");
    private readonly Property CORNER_DISTANCE_CHECK = new Property("cornerDistanceCheck", "Distance Check for Corner Grab");
    private readonly Property NORMALIZED_VALID_WALL_INTERACTION = new Property("normalizedValidWallInteraction", "Valid Normalized Interaction Area");

    private readonly Property WALL_INTERACTION_IGNORE_MOVEMENT_DURATION = new Property(
        "ignoreMovementAfterJump",
        "Ignore Movement After Jump Duration");

    private readonly Property WALL_INTERACTION_COOLDOWN = new Property("wallInteractionCooldown", "Wall Interaction Cooldown");
    private readonly Property WALL_INTERACTION_THRESHOLD = new Property("wallInteractionThreshold", "Wall Interaction Threshold");
    #endregion

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        _timingProperties.Clear();

        GUIStyle boldStyle = new GUIStyle();
        boldStyle.fontStyle = FontStyle.Bold;

        EditorGUILayout.Separator();

        _showWallInteractions = EditorGUILayout.Foldout(_showWallInteractions, "Wall Interactions");

        if (_showWallInteractions)
        {
            DisplayRegularField(OVERRIDE_WALL_INTERACTIONS);
            if (_properties[OVERRIDE_WALL_INTERACTIONS.name].hasMultipleDifferentValues || _properties[OVERRIDE_WALL_INTERACTIONS.name].boolValue)
            {
                DisplayRegularField(ENABLE_WALL_JUMPS);

                if (_properties[ENABLE_WALL_JUMPS.name].hasMultipleDifferentValues || _properties[ENABLE_WALL_JUMPS.name].boolValue)
                {
                    DisplayRegularField(WALL_JUMP_MULTIPLIER);
                    DisplayRegularField(WALL_JUMP_DEGREE);
                }

                EditorGUILayout.Separator();

                DisplayRegularField(ENABLE_WALL_STICKS);

                if (_properties[ENABLE_WALL_STICKS.name].hasMultipleDifferentValues || _properties[ENABLE_WALL_STICKS.name].boolValue)
                {
                    DisplayTimingField(WALL_STICK_DURATION);
                }

                EditorGUILayout.Separator();

                DisplayRegularField(ENABLE_WALL_SLIDES);

                if (_properties[ENABLE_WALL_SLIDES.name].hasMultipleDifferentValues || _properties[ENABLE_WALL_SLIDES.name].boolValue)
                {
                    DisplayRateField(WALL_SLIDE_SPEED);
                    DisplayAccelerationField(TIME_TO_WALL_SLIDE_SPEED);
                }

                EditorGUILayout.Separator();

                DisplayRegularField(ENABLE_CORNER_GRABS);

                if (_properties[ENABLE_CORNER_GRABS.name].hasMultipleDifferentValues || _properties[ENABLE_CORNER_GRABS.name].boolValue)
                {
                    DisplayTimingField(CORNER_GRAB_DURATION);
                    DisplayRegularField(CORNER_JUMP_MULTIPLIER);
                    DisplayRegularField(CORNER_DISTANCE_CHECK);
                }

                EditorGUILayout.Separator();

                if ((_properties[ENABLE_WALL_JUMPS.name].hasMultipleDifferentValues ||
                        _properties[ENABLE_WALL_JUMPS.name].boolValue) ||
                    (_properties[ENABLE_CORNER_GRABS.name].hasMultipleDifferentValues ||
                        _properties[ENABLE_CORNER_GRABS.name].boolValue))
                {
                    DisplayTimingField(WALL_INTERACTION_IGNORE_MOVEMENT_DURATION);
                }

                EditorGUILayout.Separator();

                if ((_properties[ENABLE_WALL_STICKS.name].hasMultipleDifferentValues ||
                        _properties[ENABLE_WALL_STICKS.name].boolValue) ||
                    (_properties[ENABLE_CORNER_GRABS.name].hasMultipleDifferentValues ||
                        _properties[ENABLE_CORNER_GRABS.name].boolValue) ||
                    (_properties[ENABLE_WALL_SLIDES.name].hasMultipleDifferentValues ||
                        _properties[ENABLE_WALL_SLIDES.name].boolValue))
                {
                    DisplayRegularField(NORMALIZED_VALID_WALL_INTERACTION);
                    DisplayTimingField(WALL_INTERACTION_COOLDOWN);
                    DisplayRegularField(WALL_INTERACTION_THRESHOLD);
                }

                EditorGUILayout.Separator();
            }
        }

        if (!serializedObject.isEditingMultipleObjects)
        {
            _showInformation = EditorGUILayout.Foldout(_showInformation, "Information");

            if (_showInformation)
            {
                EditorGUILayout.HelpBox(
                    GetInformation(),
                    MessageType.Info,
                    true);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private string GetInformation()
    {
        StringBuilder sb = new StringBuilder();

        if (_properties[TIME_TO_WALL_SLIDE_SPEED.name].floatValue != 0 && _properties[ENABLE_WALL_SLIDES.name].boolValue)
        {
            sb.AppendFormat(
                "\nWall slide acceleration: {0}",
                _properties[WALL_SLIDE_SPEED.name].floatValue / _properties[TIME_TO_WALL_SLIDE_SPEED.name].floatValue);
        }


        return sb.ToString();
    }
}
