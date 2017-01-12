using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class EnergyCharge : MonoBehaviour {

	//public float rechargeRate = 25;
	//public float maxEnergy = 100;
	//public Slider energy;
	//public AudioClip fullChime;

	//float energyLevel = 0;
	//bool energyFullTrigger = false;
	//AudioSource audioSrc;

 //   bool firstCharge;

	//void OnEnable(){
	//	EventManager.StartListening(Events.Damage, ResetEnergy);
	//	EventManager.StartListening(Events.PlayerAttack, ResetEnergy);
	//}

	//void OnDisable(){
	//	EventManager.StopListening(Events.Damage, ResetEnergy);
	//	EventManager.StopListening(Events.PlayerAttack, ResetEnergy);
	//}

	//void ResetEnergy(){
	//	energyLevel = 0;
	//	energyFullTrigger = false;
	//}

	//void Start(){
	//	Assert.IsNotNull(energy);
	//	audioSrc = GetComponent<AudioSource>();

 //       //If in tutorial, keep track of first charge
 //       firstCharge = (TutorialManager.Instance.inTutorial) ? true : false;
	//}

	//void OnTriggerEnter2D(Collider2D other){
	//	if(other.CompareTag(Tags.Player)){
	//		audioSrc.Play();
	//	}
	//}

	//void OnTriggerStay2D(Collider2D other){
	//	if(other.CompareTag(Tags.Player) && !energyFullTrigger){
	//		//Mathf.Min returns smallest of 2 values, so energy level won't ever be above 100
	//		energyLevel = Mathf.Min(energyLevel + rechargeRate * Time.deltaTime, maxEnergy);
	//		if(energyLevel == maxEnergy){
	//			energyFullTrigger = true;
	//			audioSrc.Stop();
	//	        EventManager.TriggerEvent(Events.EnergyFull);
 //               if (firstCharge) {
 //                   EventManager.TriggerEvent(TutorialEvents.EnergyFull);
 //                   firstCharge = false;
 //               }
	//		}
	//	}
	//}

	//void Update(){
	//	if(!energyFullTrigger){
	//		energy.value = energyLevel;
	//	}
	//}

	//void OnTriggerExit2D(Collider2D other){
	//	if(other.CompareTag(Tags.Player)){
	//		audioSrc.Stop();
	//	}
	//}
}
