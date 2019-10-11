/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
///
/// Changes made to this file could be overwritten when upgrading the Vuforia version.
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    #region PROTECTED_MEMBER_VARIABLES

    public static TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    public GameObject modocadastro;
    public GameObject tabela;
    public GameObject aviso;
    public GameObject voltar;

    public Text txtaviso;

    public static bool cadastro = false;
    public static bool modocadastrar = false;

    public string namee;
    public static string namee2;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        cadastro = false;
        //namee = "";
        namee2 = "";
        modocadastro = GameObject.Find("btnCadastrar");

        tabela = GameObject.Find("ScrollTabela");

        aviso = GameObject.Find("aviso");
        txtaviso = aviso.GetComponent<Text>();

        voltar = GameObject.Find("btnVoltar");

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }




    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>




    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + namee + " found");
            namee2 = namee;
            OnTrackingFound();

            if (modocadastrar == true)
            {
                //tabela.SetActive(true);
                tabela.transform.localPosition = new Vector2(45, 0);
                //aviso.SetActive(false);
                aviso.transform.localPosition = new Vector2(0, 10000);
                //voltar.SetActive(false);
                voltar.transform.localPosition = new Vector2(0, 10000);
                cadastro = true;

            }
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            txtaviso.text = "Posicione o QR no local indicado";

            Debug.Log("Trackable " + namee + " lost");
            OnTrackingLost();

            if(cadastro == true)
            {
                tabela.transform.localPosition = new Vector2(0, 10000);

                aviso.transform.localPosition = new Vector2(0, 0);

                voltar.transform.localPosition = new Vector2(0, 550);
            }
            else
            {
                modocadastro.transform.localPosition = new Vector2(-30, 550);

                tabela.transform.localPosition = new Vector2(0, 10000);

                aviso.transform.localPosition = new Vector2(0, 10000);

                voltar.transform.localPosition = new Vector2(0, 10000);
                cadastro = false;
            }

        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();

        }
    }



    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;
    }


    protected virtual void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
    }

    #endregion // PROTECTED_METHODS
}
