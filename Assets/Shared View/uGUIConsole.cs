using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

/// <summary>
/// Handles output to a Unity Input Field via IOutputService implementation.
/// Must be attached to a UI GameObject with an InputField component.
/// </summary>
[RequireComponent(typeof(InputField))]
public class uGUIConsole : MonoBehaviour, IOutputService
{
    /// <summary>
    /// Flag to write "missing component" error once.
    /// </summary>
    private bool warnOnce = false;

    /// <summary>
    /// Do not touch. Used only in "text" getter.
    /// </summary>
    private InputField _text;

    /// <summary>
    /// Getter for InputField that will be sent output.
    /// </summary>
    private InputField text
    {
        get
        {
            if (_text == null && !warnOnce)
            {
                _text = GetComponent<InputField>();

                if (_text == null)
                {
                    warnOnce = true;
                    Debug.LogError(ToString() + " missing UI InputField object!");
                }
            }

            return _text;
        }
    }

    /// <summary>
    /// Write object.ToString() to console.
    /// </summary>
    /// <param name="value"></param>
    public void Write(object value)
    {
        AppendTotext(value.ToString());
    }


    /// <summary>
    /// Write string format to console.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="values"></param>
    public void WriteFormat(string format, params object[] values)
    {
        format = string.Format(format, values);
        AppendTotext(format);
    }

    
    /// <summary>
    /// Appends text to the InputField console.
    /// </summary>
    /// <param name="value"></param>
    void AppendTotext(string value)
    {
        if (text)
        {
            text.text += value;
        }
        else
        {
            Debug.Log(value);
        }
    }
}
