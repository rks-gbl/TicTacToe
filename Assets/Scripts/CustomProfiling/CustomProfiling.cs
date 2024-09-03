using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class CustomProfiling : MonoBehaviour
{
    void Awake()
    {
        
    }
    
    void StartLogging(string logFile)
    {
        Profiler.logFile = logFile;

        Profiler.enabled=true;
        Profiler.enableBinaryLog = true;
    }

    public void StopLogging()
    {
        Profiler.enabled = false;
        Profiler.logFile = "";
    }
}
