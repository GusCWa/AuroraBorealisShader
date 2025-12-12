using UnityEngine;
using UnityEngine.Rendering; // Required for ReflectionProbe functionalities
using System.Collections; // Required for Coroutines

[RequireComponent(typeof(ReflectionProbe))]
public class ScriptedRPU : MonoBehaviour
{
    private ReflectionProbe reflectionProbe;
    public float refreshInterval = 2.0f; // Time in seconds between updates

    void Awake()
    {
        reflectionProbe = GetComponent<ReflectionProbe>();
        if (reflectionProbe == null)
        {
            Debug.LogError("ReflectionProbe component not found!");
            return;
        }

        // Configure the probe to be controlled by scripting
        reflectionProbe.mode = ReflectionProbeMode.Realtime;
        reflectionProbe.refreshMode = ReflectionProbeRefreshMode.ViaScripting;
        // Optional: use time slicing to spread the performance cost over multiple frames
        reflectionProbe.timeSlicingMode = ReflectionProbeTimeSlicingMode.AllFacesAtOnce;

        StartCoroutine(RefreshProbePeriodically());
    }

    private IEnumerator RefreshProbePeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(refreshInterval);
            UpdateReflectionProbe();
        }
    }

    public void UpdateReflectionProbe()
    {
        // This is the command to manually render the probe's cubemap
        reflectionProbe.RenderProbe();
        Debug.Log("Reflection probe updated at: " + Time.time);
    }
}

