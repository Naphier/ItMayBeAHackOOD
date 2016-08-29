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
    /// Locking object.
    /// </summary>
    private static object syncRoot = new object();

    /// <summary>
    /// Do not directly modify!
    /// </summary>
    private static volatile uGUIConsole _instance = null;

    /// <summary>
    /// Singleton instance accessor
    /// </summary>
    public static uGUIConsole Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    _instance = FindObjectOfType<uGUIConsole>();

                    if (_instance == null)
                    {
                        GameObject go = new GameObject("uGUIConsole");
                        _instance = go.AddComponent<uGUIConsole>();
                    }
                }
            }
            return _instance;
        }
    }


    /// <summary>
    /// Ensures only one singleton exists in the scene.
    /// </summary>
    void Awake()
    {
        if (Instance != this)
        {
            Destroy(this);
        }
    }

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

    public void Clear()
    {
        if (text)
        {
            text.text = "";
        }
    }

    /// <summary>
    /// Writes a line to the console.
    /// </summary>
    /// <param name="value"></param>
    public void WriteLine(object value)
    {
        Write(value.ToString() + "\n");
    }
}
