using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    /// <summary>
    /// The number of real-world seconds in one game day.
    /// </summary>
    public float dayCycleLength;

    /// <summary>
    /// The current time within the day cycle. Modify to change the World Time.
    /// </summary>
    public float currentCycleTime;

    //Would be the amount of time the sky takes to transition if UpdateSkybox were used.
    //public float skyTransitionTime;

    /// <summary>
    /// The current 'phase' of the day; Dawn, Day, Dusk, or Night
    /// </summary>
    public DayPhase currentPhase;

    /// <summary>
    /// The number of hours per day used in the WorldHour time calculation.
    /// </summary>
    public float hoursPerDay;

    /// <summary>
    /// Dawn occurs at currentCycleTime = 0.0f, so this offsets the WorldHour time to make
    /// dawn occur at a specified hour. A value of 3 results in a 5am dawn for a 24 hour world clock.
    /// </summary>
    public float dawnTimeOffset;

    /// <summary>
    /// The calculated hour of the day, based on the hoursPerDay setting. Do not set this value.
    /// Change the time of day by calculating and setting the currentCycleTime.
    /// </summary>
    public int worldTimeHour;

    /// <summary>
    /// The scene ambient color used for full daylight.
    /// </summary>
    public Color fullLight;

    /// <summary>
    /// The scene ambient color used for full night.
    /// </summary>
    public Color fullDark;

    /// <summary>
    /// The scene skybox material to use at dawn and dusk.
    /// </summary>
    public Material dawnDuskSkybox;

    /// <summary>
    /// The scene fog color to use at dawn and dusk.
    /// </summary>
    public Color dawnDuskFog;

    /// <summary>
    /// The scene skybox material to use during the day.
    /// </summary>
    public Material daySkybox;

    /// <summary>
    /// The scene fog color to use during the day.
    /// </summary>
    public Color dayFog;

    /// <summary>
    /// The scene skybox material to use at night.
    /// </summary>
    public Material nightSkybox;

    /// <summary>
    /// The scene fog color to use at night.
    /// </summary>
    public Color nightFog;

    /// <summary>
    /// The calculated time at which dawn occurs based on 1/4 of dayCycleLength.
    /// </summary>
    private float dawnTime;

    /// <summary>
    /// The calculated time at which day occurs based on 1/4 of dayCycleLength.
    /// </summary>
    private float dayTime;

    /// <summary>
    /// The calculated time at which dusk occurs based on 1/4 of dayCycleLength.
    /// </summary>
    private float duskTime;

    /// <summary>
    /// The calculated time at which night occurs based on 1/4 of dayCycleLength.
    /// </summary>
    private float nightTime;

    /// <summary>
    /// One quarter the value of dayCycleLength.
    /// </summary>
    private float quarterDay;

    //Would be the amount of time remaining in the skybox transition if UpdateSkybox were used.
    //private float remainingTransition;

    /// <summary>
    /// The specified intensity of the directional light, if one exists. This value will be
    /// faded to 0 during dusk, and faded from 0 back to this value during dawn.
    /// </summary>
    private float lightIntensity;

    /// <summary>
    /// Initializes working variables and performs starting calculations.
    /// </summary>
    void Initialize()
    {
        //remainingTransition = skyTransitionTime; //Would indicate that the game should start with an active transition, if UpdateSkybox were used.
        quarterDay = dayCycleLength * 0.25f;
        dawnTime = 0.0f;
        dayTime = dawnTime + quarterDay;
        duskTime = dayTime + quarterDay;
        nightTime = duskTime + quarterDay;
    }

    /// <summary>
    /// Sets the script control fields to reasonable default values for an acceptable day/night cycle effect.
    /// </summary>
    void Reset()
    {
        dayCycleLength = 120.0f;
        //skyTransitionTime = 3.0f; //would be set if UpdateSkybox were used.
        hoursPerDay = 24.0f;
        dawnTimeOffset = 3.0f;
        fullDark = new Color(32.0f / 255.0f, 28.0f / 255.0f, 46.0f / 255.0f);
        fullLight = new Color(253.0f / 255.0f, 248.0f / 255.0f, 223.0f / 255.0f);
        dawnDuskFog = new Color(133.0f / 255.0f, 124.0f / 255.0f, 102.0f / 255.0f);
        dayFog = new Color(180.0f / 255.0f, 208.0f / 255.0f, 209.0f / 255.0f);
        nightFog = new Color(12.0f / 255.0f, 15.0f / 255.0f, 91.0f / 255.0f);
        Skybox[] skyboxes = AssetBundle.FindObjectsOfTypeIncludingAssets(typeof(Skybox)) as Skybox[];
        foreach (Skybox box in skyboxes)
        {
            if (box.name == "DawnDusk Skybox")
            { dawnDuskSkybox = box.material; }
            else if (box.name == "StarryNight Skybox")
            { nightSkybox = box.material; }
            else if (box.name == "Sunny2 Skybox")
            { daySkybox = box.material; }
        }
    }

    // Use this for initialization
    void Start()
    {
        Initialize();
        BGMManager.PlayMusic("BGM");
    }

    // Update is called once per frame
    void Update()
    {
        // Rudementary phase-check algorithm:
        if (currentCycleTime > nightTime && currentPhase == DayPhase.Dusk)
        {
            SetNight();
        }
        else if (currentCycleTime > duskTime && currentPhase == DayPhase.Day)
        {
            SetDusk();
        }
        else if (currentCycleTime > dayTime && currentPhase == DayPhase.Dawn)
        {
            SetDay();
        }
        else if (currentCycleTime > dawnTime && currentCycleTime < dayTime && currentPhase == DayPhase.Night)
        {
            SetDawn();
        }

        // Perform standard updates:
        UpdateWorldTime();
        UpdateDaylight();
        UpdateFog();
        //UpdateSkybox(); //would be called if UpdateSkybox were used.

        // Update the current cycle time:
        currentCycleTime += Time.deltaTime;
        currentCycleTime = currentCycleTime % dayCycleLength;
    }

    /// <summary>
    /// Sets the currentPhase to Dawn, turning on the directional light, if any.
    /// </summary>
    public void SetDawn()
    {
        RenderSettings.skybox = dawnDuskSkybox; //would be commented out or removed if UpdateSkybox were used.
        //remainingTransition = skyTransitionTime; //would be set if UpdateSkybox were used.
      
        currentPhase = DayPhase.Dawn;
    }

    /// <summary>
    /// Sets the currentPhase to Day, ensuring full day color ambient light, and full
    /// directional light intensity, if any.
    /// </summary>
    public void SetDay()
    {
        RenderSettings.skybox = daySkybox; //would be commented out or removed if UpdateSkybox were used.
        //remainingTransition = skyTransitionTime; //would be set if UpdateSkybox were used.
        RenderSettings.ambientLight = fullLight;
        currentPhase = DayPhase.Day;
    }

    /// <summary>
    /// Sets the currentPhase to Dusk.
    /// </summary>
    public void SetDusk()
    {
        RenderSettings.skybox = dawnDuskSkybox; //would be commented out or removed if UpdateSkybox were used.
        //remainingTransition = skyTransitionTime; //would be set if UpdateSkybox were used.
        currentPhase = DayPhase.Dusk;
    }

    /// <summary>
    /// Sets the currentPhase to Night, ensuring full night color ambient light, and
    /// turning off the directional light, if any.
    /// </summary>
    public void SetNight()
    {
        RenderSettings.skybox = nightSkybox; //would be commented out or removed if UpdateSkybox were used.
        //remainingTransition = skyTransitionTime; //would be set if UpdateSkybox were used.
        RenderSettings.ambientLight = fullDark;
        currentPhase = DayPhase.Night;
    }

    /// <summary>
    /// If the currentPhase is dawn or dusk, this method adjusts the ambient light color and direcitonal
    /// light intensity (if any) to a percentage of full dark or full light as appropriate. Regardless
    /// of currentPhase, the method also rotates the transform of this component, thereby rotating the
    /// directional light, if any.
    /// </summary>
    private void UpdateDaylight()
    {
        if (currentPhase == DayPhase.Dawn)
        {
            float relativeTime = currentCycleTime - dawnTime;
            RenderSettings.ambientLight = Color.Lerp(fullDark, fullLight, relativeTime / quarterDay);
        }
        else if (currentPhase == DayPhase.Dusk)
        {
            float relativeTime = currentCycleTime - duskTime;
            RenderSettings.ambientLight = Color.Lerp(fullLight, fullDark, relativeTime / quarterDay);
        }

        transform.Rotate(Vector3.up * ((Time.deltaTime / dayCycleLength) * 360.0f), Space.Self);
    }

    /// <summary>
    /// Interpolates the fog color between the specified phase colors during each phase's transition.
    /// eg. From DawnDusk to Day, Day to DawnDusk, DawnDusk to Night, and Night to DawnDusk
    /// </summary>
    private void UpdateFog()
    {
        if (currentPhase == DayPhase.Dawn)
        {
            float relativeTime = currentCycleTime - dawnTime;
            RenderSettings.fogColor = Color.Lerp(dawnDuskFog, dayFog, relativeTime / quarterDay);
        }
        else if (currentPhase == DayPhase.Day)
        {
            float relativeTime = currentCycleTime - dayTime;
            RenderSettings.fogColor = Color.Lerp(dayFog, dawnDuskFog, relativeTime / quarterDay);
        }
        else if (currentPhase == DayPhase.Dusk)
        {
            float relativeTime = currentCycleTime - duskTime;
            RenderSettings.fogColor = Color.Lerp(dawnDuskFog, nightFog, relativeTime / quarterDay);
        }
        else if (currentPhase == DayPhase.Night)
        {
            float relativeTime = currentCycleTime - nightTime;
            RenderSettings.fogColor = Color.Lerp(nightFog, dawnDuskFog, relativeTime / quarterDay);
        }
    }

    //Not yet implemented, but would be nice to allow a smoother transition of the Skybox material.
    //private void UpdateSkybox()
    //{
    //    if (remainingTransition > 0.0f)
    //    {
    //        if (currentPhase == DayCycle.Dawn)
    //        {
    //            //RenderSettings.skybox.Lerp(dawnDuskSkybox, nightSkybox, remainingTransition / skyTransitionTime);
    //        }
    //        if (currentPhase == DayCycle.Day)
    //        {

    //        }
    //        if (currentPhase == DayCycle.Dusk)
    //        {

    //        }
    //        if (currentPhase == DayCycle.Night)
    //        {

    //        }
    //        remainingTransition -= Time.deltaTime;
    //    }
    //}

    /// <summary>
    /// Updates the World-time hour based on the current time of day.
    /// </summary>
    private void UpdateWorldTime()
    {
        worldTimeHour = (int)((Mathf.Ceil((currentCycleTime / dayCycleLength) * hoursPerDay) + dawnTimeOffset) % hoursPerDay) + 1;
    }

    public enum DayPhase
    {
        Night = 0,
        Dawn = 1,
        Day = 2,
        Dusk = 3
    }
}
