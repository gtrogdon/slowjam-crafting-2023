using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;
using System.IO;

/* Create Dictionary of Story variables
 * Subscribe and Unsubscribe when story variables changes */
public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables { get;  private set; }

    public DialogueVariables(TextAsset loadGlobalsJSON)
    {
        Story globalVariablesStory = new Story(loadGlobalsJSON.text);
        // initialize dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized global dialogue variable: " + name + ":" + value);
        }

    }

    public void StartListening(Story story)
    {
        VariablesToStory(story); // MUST assign global variables before assigning listener
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

    public void SetVariable(string key, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(key))
        {
            if (variables[key] != value)
            {
                Debug.Log("Set Variable: " + key + " to " + value.ToString());
                variables.Remove(key);
                variables.Add(key, value);
            }
            else
            {
                Debug.Log("no need to set");
            }
        }
    }
}
