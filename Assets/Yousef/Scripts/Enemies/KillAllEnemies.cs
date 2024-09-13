using System.Collections.Generic;  // Required for List
using UnityEngine;

public class KillAllEnemies : MonoBehaviour {
    [SerializeField] List<GameObject> Enemies;  // Change from array to List
    [SerializeField] GameObject Trigger;
    private int counter;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 PlayerPos;
    [SerializeField] private Vector3 PlayerNewPos;
    private bool Opened;

    private void Start() {
        
    }

    private void Update() {
        for (int i = Enemies.Count - 1; i >= 0; i--) {  // Loop from end to avoid index issues when removing
            GameObject tempEnemy = Enemies[i];
            if (tempEnemy.GetComponent<Enemy>() != null) {
                Enemy enemy = tempEnemy.GetComponent<Enemy>();
                if (enemy.Health <= 0) {
                    counter++;
                    Enemies.RemoveAt(i);  // Remove the enemy from the list
                }
            }
        }

        // Check if the list is empty and activate the trigger
        if (Enemies.Count == 0 && !Opened) {
            Trigger.SetActive(true);
            Opened = true;
        }
    }

    public void ResetPlayerPostion() {
        player.transform.position = PlayerPos;
    }

    public void UpdatePlayerPostion() {
        player.transform.position = PlayerNewPos;
    }
}