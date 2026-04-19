using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CandleSystem : MonoBehaviour
{
    [SerializeField] private PlayerMovementController playerMovementController; // disable movement while changing candle
    [SerializeField] private Light candleLight;
    [SerializeField] private ParticleSystemRenderer candleFlameRenderer;
    
    [SerializeField] private float dimDuration = 0.1f;
    [SerializeField] private float glowDuration = 1f;
    private float elapsedTime;
    private bool isCandleOn = true;
    private bool dimmingCandle = false;
    private bool glowingCandle = false;
    private float originalLightIntensity;
    
    void Start()
    {
        InputHandler.Instance.CandleButtonHeld.AddListener(OnCandleHoldInput);

        originalLightIntensity = candleLight.intensity;
        isCandleOn = true;

        var originalMat = candleFlameRenderer.sharedMaterial;
        candleFlameRenderer.sharedMaterial = new Material(originalMat); // prevents color changes to save to the asset
    }

    void Update()
    {
        if (dimmingCandle)
        {
            LerpCandleInternal(false);
        }
        else if (glowingCandle)
        {
            LerpCandleInternal(true);
        }
    }

    public void OnCandleHoldInput(bool inputSuccess)
    {
        if (inputSuccess)
        {
            ToggleCandle();
        }
    }
    
    public void ShowCandle()
    {
        // if not already on and not already in the process of dimming
        if (!isCandleOn && !dimmingCandle)
        {
            glowingCandle = true;
        }
    }
    
    public void HideCandle()
    {
        // if the candle is on and not already in the process of glowing
        if (isCandleOn && !glowingCandle)
        {
            dimmingCandle = true;
        }
    }

    public void ToggleCandle()
    {
        if (!isCandleOn)
        {
            ShowCandle();
        }
        else
        {
            HideCandle();
        }
    }
    
    private void LerpCandleInternal(bool toggleOn)
    {
        float duration = toggleOn ? glowDuration : dimDuration;
        
        if (elapsedTime < duration)
        {
            playerMovementController.movementEnabled = false;
            
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            float lerpValueFlameMat = toggleOn ?  Mathf.Lerp(0, 1, t) : Mathf.Lerp(1, 0, t);
            float lerpValueLightIntensity = toggleOn ? Mathf.Lerp(0, originalLightIntensity, t): Mathf.Lerp(originalLightIntensity, 0, t);

            // update mat
            Color oldColor = candleFlameRenderer.sharedMaterial.color;
            candleFlameRenderer.sharedMaterial.color = new Color(oldColor.r, oldColor.g, oldColor.b, lerpValueFlameMat);
            // update light
            candleLight.intensity = lerpValueLightIntensity;
        }
        else if (elapsedTime >= duration)
        {
            // update mat
            Color oldColor = candleFlameRenderer.sharedMaterial.color;
            candleFlameRenderer.sharedMaterial.color = new Color(oldColor.r, oldColor.g, oldColor.b, toggleOn ? 1 : 0);
            // update light
            candleLight.intensity = toggleOn ? originalLightIntensity : 0;
            
            elapsedTime = 0;

            glowingCandle = false;
            dimmingCandle = false;
            isCandleOn = toggleOn;
            playerMovementController.movementEnabled = true;
        }
    }
}
