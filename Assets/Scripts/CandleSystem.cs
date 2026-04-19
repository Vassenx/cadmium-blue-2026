using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class CandleSystem : MonoBehaviour
{
    [SerializeField] private PlayerMovementController playerMovementController; // disable movement while changing candle
    [SerializeField] private Light candleLight;
    [SerializeField] private ParticleSystemRenderer candleFlameRenderer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip snuffOutCandleAudioClip;
    [SerializeField] private AudioClip lightCandleAudioClip;
    [SerializeField] private GameObject firstpersonCam;
    [SerializeField] private GameObject flame1;
    [SerializeField] private GameObject flame2;

    [SerializeField] private TextMeshProUGUI candleTutorialText;

    
    [SerializeField] private float dimDuration = 0.1f;
    [SerializeField] private float glowDuration = 1f;

    [SerializeField] private float waitTilMoveCandleBack = 0.5f;

    private Vector3 goalPosition;
    private bool readyToRelightCandle = false;
    private float elapsedTime;
    public bool isCandleOn = true;
    private bool dimmingCandle = false;
    private bool glowingCandle = false;
    private float originalLightIntensity;

    private Vector3 originalLocation;
    
    void Start()
    {
        candleTutorialText.gameObject.SetActive(true);
        
        originalLocation = transform.localPosition;
        InputHandler.Instance.CandleButtonHeld.AddListener(OnCandleHoldInput);
        InputHandler.Instance.CandleButtonPressed.AddListener(OnCandlePressInput);

        originalLightIntensity = candleLight.intensity;
        isCandleOn = true;

        var originalMat = candleFlameRenderer.sharedMaterial;
        candleFlameRenderer.sharedMaterial = new Material(originalMat); // prevents color changes to save to the asset

        TurnCandleOff();
    }
    
    private void TurnCandleOff() // no lerp, just for start
    {
        // update mat
        Color oldColor = candleFlameRenderer.sharedMaterial.color;
        candleFlameRenderer.sharedMaterial.color = new Color(oldColor.r, oldColor.g, oldColor.b, 0);
        // update light
        candleLight.intensity =0;
        
        flame1.SetActive(false);
        flame2.SetActive(false);
        
        glowingCandle = false;
        dimmingCandle = false;
        isCandleOn = false;
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

            LerpCandleMovement();
        }
    }

    public void OnCandleHoldInput(bool inputSuccess)
    {
        if (inputSuccess)
        {
            if (candleTutorialText.gameObject.activeSelf)
            {
                candleTutorialText.gameObject.SetActive(false);
            }

            ToggleCandle();
        }
    }
    
    public void OnCandlePressInput()
    {
        HideCandle();
    }
    
    public void ShowCandle()
    {
        // if not already on and not already in the process of dimming
        if (!isCandleOn && !dimmingCandle)
        {
            flame1.SetActive(true);
            flame2.SetActive(true);
            audioSource.PlayOneShot(lightCandleAudioClip);
            StartMovingCandle();
            glowingCandle = true;
        }
    }
    
    public void HideCandle()
    {
        // if the candle is on and not already in the process of glowing
        if (isCandleOn && !glowingCandle)
        {
            flame1.SetActive(false);
            flame2.SetActive(false);
            
            audioSource.PlayOneShot(snuffOutCandleAudioClip);
            
            dimmingCandle = true;
            readyToRelightCandle = false;
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
        if (toggleOn && !readyToRelightCandle)
        {
            return;
        }
        
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
    
    private void StartMovingCandle()
    {
        StartCoroutine(MoveCandle());
    }

    IEnumerator MoveCandle()
    {
        Vector3 camForward = firstpersonCam.transform.forward;
        Vector3 localCamForward = firstpersonCam.transform.InverseTransformDirection(camForward);
        goalPosition = transform.localPosition + (localCamForward * -1f) * 50f * Time.deltaTime; // backwards

        yield return new WaitForSeconds(waitTilMoveCandleBack);
        readyToRelightCandle = true;

        goalPosition = originalLocation; // forwards

    }
    
    private void LerpCandleMovement()
    {
        if (!goalPosition.AlmostZero())
        {
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, goalPosition, 0.1f);
        }

        if (Vector3.Distance(transform.localPosition, goalPosition) < 0.001f)
        {
            goalPosition = Vector3.zero;
        }
    }
}
