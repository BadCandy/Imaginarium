using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEditor;
using PC2D;

public class ExtendedEditor : Editor
{
    protected Dictionary<string, SerializedProperty> _properties = new Dictionary<string, SerializedProperty>();
    protected List<Property> _timingProperties = new List<Property>();

    protected class Property
    {
        public string name;
        public string text;

        public Property(string n, string t)
        {
            name = n;
            text = t;
        }
    }

    protected virtual void OnEnable()
    {
        _properties.Clear();
        SerializedProperty property = serializedObject.GetIterator();

        while (property.NextVisible(true))
        {
            _properties[property.name] = property.Copy();
        }

    }

    protected void CheckAndDisplayTimingWarnings(Property property)
    {
        if (!_properties[property.name].hasMultipleDifferentValues &&
            !Mathf.Approximately(_properties[property.name].floatValue / Time.fixedDeltaTime,
                Mathf.Round(_properties[property.name].floatValue / Time.fixedDeltaTime)))
        {
            string msg = string.Format(
                "'{0}' is not a multiple of the fixed time step ({1}). This results in an extra frame effectively making '{0}' {2} instead of {3}",
                property.text,
                Time.fixedDeltaTime,
                Globals.GetFrameCount(_properties[property.name].floatValue) * Time.fixedDeltaTime,
                _properties[property.name].floatValue);

            EditorGUILayout.HelpBox(
                msg,
                MessageType.Warning,
                true);
        }
    }

    protected void DisplayRegularField(Property property)
    {
        EditorGUILayout.PropertyField(_properties[property.name], new GUIContent(property.text));
    }

    protected void DisplayRateField(Property property)
    {
        string frameRate = "-";

        if (!_properties[property.name].hasMultipleDifferentValues)
        {
            frameRate = (_properties[property.name].floatValue * Time.fixedDeltaTime).ToString();
        }

        EditorGUILayout.PropertyField(_properties[property.name],
            new GUIContent(string.Format("{0} ({1} Distance/Frame)", property.text, frameRate)));
    }

    protected void DisplayTimingField(Property property)
    {
        _timingProperties.Add(property);

        string frameCount = "-";

        if (!_properties[property.name].hasMultipleDifferentValues)
        {
            frameCount = Globals.GetFrameCount(_properties[property.name].floatValue).ToString();
        }

        EditorGUILayout.PropertyField(_properties[property.name],
            new GUIContent(string.Format("{0} ({1} Frames)", property.text, frameCount)));
    }

    protected void DisplayAccelerationField(Property property)
    {
        string frameCount = "-";

        if (!_properties[property.name].hasMultipleDifferentValues)
        {
            frameCount = (_properties[property.name].floatValue / Time.fixedDeltaTime).ToString();
        }

        EditorGUILayout.PropertyField(_properties[property.name],
            new GUIContent(string.Format("{0} ({1} Frames)", property.text, frameCount)));
    }
}
