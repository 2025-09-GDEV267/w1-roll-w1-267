using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ComboTracker : MonoBehaviour
{
    [SerializeField]
    TMP_Text VelocityText;
    [SerializeField]
    TMP_Text ComboText;
    [SerializeField]
    PlayerController Player;

    public Vector3 lastGrappled;

    public int potentialScore;
    private int compoundScore;
    public int DsiplayScore;
    public int Score = 0;
    public int grappleInARow = 0;
    public float airTime = 0;

    private bool newGrapple;
    private bool isGrappled = false;
    private bool isGrounded = false;
    private bool died = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        VelocityText.text = "Score: " + DsiplayScore;
        if (died)
        {
            ComboText.text = "";
            potentialScore = 0;
            compoundScore = 0;
            grappleInARow = 0;
            died = false;
            DsiplayScore = Score;
        }
        if (isGrappled)
        {
            potentialScore = Player.getMagnitude() * Convert.ToInt32(airTime) * grappleInARow + compoundScore;
            
        }
        if (grappleInARow > 0)
        {
            ComboText.text = "Combo: " + "Velocity(" + Player.getMagnitude() + ") x AirTime(" + MathF.Round(airTime,2) + ")" + " x Grapple(" + grappleInARow + ") + Chain(" + compoundScore + ")";
        }
        if (isGrounded && !isGrappled)
        {
            airTime = 0;
            ComboText.text = "";
            addScore();
           

            potentialScore = 0;
            compoundScore = 0;
            grappleInARow = 0;
        }
        if (!isGrounded && !isGrappled)
        {
            airTime += Time.deltaTime;
        }
    }
    public void addScore()
    {
        Score += compoundScore;
        StartCoroutine("Ticker");
    }
    public void setIsGrappled(bool isG, Vector3 grappled)
    {
        if (lastGrappled != grappled)
        {
            newGrapple = true;
            compoundScore = potentialScore;
            lastGrappled = grappled;
            grappleInARow++;
        }
        isGrappled = isG;
    }
    public void setIsGrounded(bool isG)
    {
        isGrounded = isG;
    }
    public void getRespawned(bool respawn)
    {
        died = respawn;
    }
    public IEnumerator Ticker()
    {
        while (DsiplayScore != Score)
        {
            VelocityText.color = Color.limeGreen;
            //we don't want to increment CurrentScore to infinity, so we only do it if it's lower than TargetScore
            if (DsiplayScore < Score)
            {
                DsiplayScore += 132;

                //this is a 'safety net' to ensure we never exceed our TargetScore
                if (DsiplayScore > Score)
                {
                    DsiplayScore = Score;
                }
            }

            //wait for some time before incrementing again
            yield return new WaitForSeconds(0.1f);

        }
        VelocityText.color = Color.white;
    }
}
