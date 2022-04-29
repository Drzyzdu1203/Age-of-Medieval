using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [Header("Stamina Main")]
    public float playerStamina;
    [SerializeField] private float maxStamina = 100.0f;
    [SerializeField] private float jumpCost = 20;
    [HideInInspector] public bool hasRegenerated = true;
    [HideInInspector] public bool sprint = false;
    [HideInInspector] public bool jump = false;

    [Header("Stamina Regen Parameters")]
    [Range(0,50)][SerializeField] private float staminaDrain = 0.5f;
    [Range(0, 50)] [SerializeField] private float staminaRegen = 0.5f;

    [Header("Stamina Speed Parameters")]
    [SerializeField] private int normalRunSpeed = 4;
    [SerializeField] private int runSpeed = 8;


    // Start is called before the first frame update
    void Start()
    {
        playerStamina = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (jump)
        {
            playerStamina -= jumpCost;

        }
        if (hasRegenerated)
        {
            playerStamina += staminaRegen;
        }
        if (!sprint)
        {
            playerStamina -= staminaDrain;
        }
    }
}
//END FILE
