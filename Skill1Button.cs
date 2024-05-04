using UnityEngine;
using UnityEngine.UI;

public class Skill1Button : MonoBehaviour
{
    public Button skill1Button; 
    public Text cooldownText; 

    public float cooldownDuration = 3f; 

    private SkillButton skillButtonScript; 

    void Start()
    {
        
        skillButtonScript = skill1Button.GetComponent<SkillButton>();

        
        skill1Button.onClick.AddListener(UseSkill1);
    }

    void UseSkill1()
    {
        
        skillButtonScript.StartCooldown(cooldownDuration);
    }
}
