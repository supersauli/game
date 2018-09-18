using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
public class InputManage : MonoBehaviour {
    private Contexts _contexts;
    private Systems _systems;
    private  InputEntity _inputEntity;
	// Use this for initialization
	void Start ()
	{
	    _contexts = Contexts.sharedInstance;
	    _systems = new Systems().Add(new MoveSysteam(_contexts))
	        .Add(new AttackSkillSysteam(_contexts));
	    _inputEntity = _contexts.input.CreateEntity();
        
	}
     // Update is called once per frame
	void Update ()
	{
	   // InputDirKey();
	   InputExtern();

        _systems.Execute();
	    _systems.Cleanup();
	}
    // 方向键
    void InputDirKey()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _inputEntity.ReplaceKeyForwardInput(true);
        }
         if (Input.GetKeyDown(KeyCode.S))
        {
            _inputEntity.ReplaceKeyBackInput(true);
        }
         if (Input.GetKeyDown(KeyCode.A))
        {
            _inputEntity.ReplaceKeyLeftInput(true);
        }
         if (Input.GetKeyDown(KeyCode.D))
        {
            _inputEntity.ReplaceKeyRightInput(true);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            _inputEntity.ReplaceKeyForwardInput(false);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            _inputEntity.ReplaceKeyBackInput(false);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            _inputEntity.ReplaceKeyLeftInput(false);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            _inputEntity.ReplaceKeyRightInput(false);
        }
       
    }



    void InputExtern()
    {
        var dirVertical = Input.GetAxis("Vertical");
        var dirHorizontal = Input.GetAxis("Horizontal");
        if (dirVertical > 0)
        {
            _inputEntity.ReplaceKeyBackInput(false);
            _inputEntity.ReplaceKeyForwardInput(true);
        }
	    else if(dirVertical<0)
	    {
	        _inputEntity.ReplaceKeyForwardInput(false);
            _inputEntity.ReplaceKeyBackInput(true);
        }
	   
	    if (dirHorizontal > 0)
	    {
	        _inputEntity.ReplaceKeyLeftInput(false);
	        _inputEntity.ReplaceKeyRightInput(true);
	    }
	    else if(dirHorizontal<0)
	    {
	        _inputEntity.ReplaceKeyRightInput(false);
	        _inputEntity.ReplaceKeyLeftInput(true);

	    }
	  

	    if (Input.GetKeyDown(KeyCode.J))
	    {
	        _inputEntity.ReplaceKeyAttackSkill1(true);
        }
	    if (Input.GetKeyDown(KeyCode.K))
	    {

	    }

    }

   
}
