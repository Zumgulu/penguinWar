using UnityEngine;
using System.Collections;

public class zoom : MonoBehaviour {


	void  Update (){
		// -------------------Code for Zooming Out------------
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			if (Camera.main.fieldOfView>40)
				Camera.main.fieldOfView -=2;
		}
		// ---------------Code for Zooming In------------------------
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if (Camera.main.fieldOfView<=120)
				Camera.main.fieldOfView +=2;
		}
		

	}
}



