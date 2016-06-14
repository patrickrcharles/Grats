#pragma strict

var anim: Animator;
var Cloth1: boolean = true;
var Cloth2: boolean = true;

function Start () {
	anim = this.GetComponent("Animator");
}

function Update () {

}

function OnGUI(){

	if(GUI.Button(Rect(30, 0, 100, 30), "IDLE"))
		anim.SetInteger("Cond", 0);

	if(GUI.Button(Rect(30, 30, 100, 30), "ATTACK 1"))
		anim.SetInteger("Cond", 1);
		
	if(GUI.Button(Rect(30, 60, 100, 30), "ATTACK 2"))
		anim.SetInteger("Cond", 2);
		
	if(GUI.Button(Rect(30, 90, 100, 30), "RUN"))
		anim.SetInteger("Cond", 3);
		
	if(GUI.Button(Rect(30, 120, 100, 30), "WALK RIGHT"))
		anim.SetInteger("Cond", 4);
		
	if(GUI.Button(Rect(30, 150, 100, 30), "WALK LEFT"))
		anim.SetInteger("Cond", 5);
		
	if(GUI.Button(Rect(30, 180, 100, 30), "WALK"))
		anim.SetInteger("Cond", 6);
		
	if(GUI.Button(Rect(30, 210, 100, 30), "JUMP"))
		anim.SetInteger("Cond", 7);
		
	if(GUI.Button(Rect(30, 240, 100, 30), "DEATH"))
		anim.SetInteger("Cond", 8);
		
	if(GUI.Button(Rect(Screen.width - 100, 0, 100, 30), "Outerwear"))
		if (Cloth1 == true){
			GameObject.Find("Robber@T-pose/Old_man_low_1/Outerwear").GetComponent.<Renderer>().enabled = false;
			Cloth1 = false;
		}else{
			GameObject.Find("Robber@T-pose/Old_man_low_1/Outerwear").GetComponent.<Renderer>().enabled = true;
			Cloth1 = true;
		};
		
	if(GUI.Button(Rect(Screen.width - 100, 30, 100, 30), "Pants"))
		if (Cloth2 == true){
			GameObject.Find("Robber@T-pose/Old_man_low_1/PantsLow").GetComponent.<Renderer>().enabled = false;
			Cloth2 = false;
		}else{
			GameObject.Find("Robber@T-pose/Old_man_low_1/PantsLow").GetComponent.<Renderer>().enabled = true;
			Cloth2 = true;
		};
		

}