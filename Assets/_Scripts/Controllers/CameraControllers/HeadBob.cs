using UnityEngine;

public class Headbob : MonoBehaviour
{
    private float timer = 0.0f;
    
    [SerializeField] private float bobbingSpeed;
    [SerializeField] private float bobbingAmount = 0.2f;

    [SerializeField] float localYMidpoint = 0.5f;

    private const float footstepCooldown = 0.2f;
    private float footstepTimer;

    private void Update()
    {
        if (DataMessenger.GetBool(BoolKey.IsGameActive))
        {
            HeadBob();
        }
    }
    private void HeadBob()
    {
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 newPosition = transform.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);

            timer += bobbingSpeed * Time.deltaTime;

            if (timer > Mathf.PI * 2)
            {
                timer -= Mathf.PI * 2;
            }
        }

        if (waveslice != 0)
        {
            float translateChange = waveslice * bobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            newPosition.y = localYMidpoint + translateChange;

            if (footstepTimer < 0)
            {
                if (Mathf.Abs(newPosition.y - (localYMidpoint - bobbingAmount)) < 0.01f)
                {
                    PlayFootstep();
                }
            }
            else
            {
                footstepTimer -= Time.deltaTime;
            }
        }
        else
        {
            newPosition.y = localYMidpoint;
        }

        transform.localPosition = newPosition;
    }
    private void PlayFootstep()
    {
        AudioPlayer.PlaySound("Player Footstep", minPitch: 0.85f, maxPitch: 1.15f, 
            volume: Random.Range(0.85f, 1f));

        footstepTimer = footstepCooldown;
    }
}