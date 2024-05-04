using UnityEngine;
using UnityEngine.UI;

public class Skill2Button : MonoBehaviour
{
    public Button skill2Button; 
    public Text cooldownText; 

    public float cooldownDuration = 5f; 

    private SkillButton skillButtonScript; 

    void Start()
    {
        
        skillButtonScript = skill2Button.GetComponent<SkillButton>();

        
        skill2Button.onClick.AddListener(UseSkill2);
    }

    void UseSkill2()
    {
        
        skillButtonScript.StartCooldown(cooldownDuration);
    }
}
