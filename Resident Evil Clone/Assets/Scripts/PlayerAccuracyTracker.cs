using System;
using UnityEngine;
using TMPro;

public class PlayerAccuracyTracker : MonoBehaviour
{
    private int roundsFired;
    private int roundsHit;

    void OnEnable()
    {
        Weapon.OnRoundFired += HandleRoundFired;
        Weapon.OnRoundHit += HandleRoundHit;
    }

    void OnDisable()
    {
        Weapon.OnRoundFired -= HandleRoundFired;
        Weapon.OnRoundHit -= HandleRoundHit;
    }

    private void HandleRoundFired()
    {
        roundsFired++;
        UpdateAccuracy();
    }

    private void HandleRoundHit()
    {
        roundsHit++;
        UpdateAccuracy();
    }

    private void UpdateAccuracy()
    {
        float accuracy = roundsFired > 0 ? (float)roundsHit / roundsFired * 100 : 0;
        Debug.Log($"Player Accuracy: {accuracy:F2}%");
    }
}