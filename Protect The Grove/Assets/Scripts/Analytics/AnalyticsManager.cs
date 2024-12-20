using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using System;

public class AnalyticsManager : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            GiveConsent();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void GiveConsent()
    {
        AnalyticsService.Instance.StartDataCollection();
        Debug.Log($"Consent given! We can get the data!!!");
    }

    // Método para enviar el evento de impacto del boss
    public void SendHeavyUnitImpactEvent(float timeToKill)
    {
        var eventData = new Dictionary<string, object>
        {
            { "time_to_kill", timeToKill }
        };

        // Enviar evento
        AnalyticsService.Instance.RecordEvent("heavyUnitImpact");
        Debug.Log($"Analytics Event Sent: heavyUnitImpact with time_to_kill: {timeToKill}");
    }

    // Método para enviar el evento de oro gastado por partida
    public void SendGoldSpentEvent(float goldSpent)
    {
        var eventData = new Dictionary<string, object>
        {
            { "gold_spent", goldSpent }
        };

        // Enviar evento con datos
        AnalyticsService.Instance.RecordEvent("totalGoldSpent");
        Debug.Log($"Analytics Event Sent: totalGoldSpent with gold_spent: {goldSpent}");
    }
}
