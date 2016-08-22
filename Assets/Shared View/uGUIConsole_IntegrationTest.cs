using UnityEngine;
using System.Collections;

public class uGUIConsole_IntegrationTest : MonoBehaviour
{
    private IOutputService console;

    void Start()
    {
        console = FindObjectOfType<uGUIConsole>();

        if (console == null)
        {
            Debug.LogError("Console is null");
            return;
        }

        for (int i = 0; i < 100; i++)
        {
            if (i % 2 == 0)
                console.WriteFormat((i < 10 ? "0" : "") + "{0} Write format\n", i);
            else
                console.Write((i < 10 ? "0" : "") + i + " Write\n");
        }
    }

}
