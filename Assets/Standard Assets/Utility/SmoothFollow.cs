using UnityEngine;

namespace UnityStandardAssets.Utility
{
	public class SmoothFollow : MonoBehaviour
	{

		// The target we are following
		private Transform target;
		// The distance in the x-z plane to the target
		[SerializeField]
		private float distance = 10.0f;
		// the height we want the camera to be above the target
		[SerializeField]
		private float height = 5.0f;

		[SerializeField]
		private float rotationDamping;
		[SerializeField]
		private float heightDamping;


        //for clamping frustum

        private float rightBound;
        private float leftBound;
        private float topBound;
        private float bottomBound;
        private Vector3 pos;
        private TerrainCollider planeBound;

        // Use this for initialization
        void Start() {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            float vertExtent = GetComponent<Camera>().orthographicSize;
            float horzExtent = vertExtent * Screen.width / Screen.height;
            planeBound = GameObject.FindGameObjectWithTag("Ground").GetComponent<TerrainCollider>();

            leftBound = (float)(horzExtent - planeBound.bounds.size.x / 2.0f);
            rightBound = (float)(planeBound.bounds.size.x / 2.0f - horzExtent);
            bottomBound = (float)(vertExtent - planeBound.bounds.size.y / 2.0f);
            topBound = (float)(planeBound.bounds.size.y / 2.0f - vertExtent);

        }

		// Update is called once per frame
		void LateUpdate()
		{
			// Early out if we don't have a target
			if (!target)
				return;

			// Calculate the current rotation angles
			var wantedRotationAngle = target.eulerAngles.y;
			var wantedHeight = target.position.y + height;

			var currentRotationAngle = transform.eulerAngles.y;
			var currentHeight = transform.position.y;

			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

			// Damp the height
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			// Convert the angle into a rotation
			var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            var pos = new Vector3(target.position.x, target.position.y, target.position.z);
          //  pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
            //pos.z /= 50;
            pos.y = Mathf.Clamp(pos.y, bottomBound, topBound);
            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            transform.position = pos;


			transform.position -= currentRotation * Vector3.forward * distance;

			// Set the height of the camera
			transform.position = new Vector3(transform.position.x , currentHeight, transform.position.z);

			// Always look at the target
			transform.LookAt(target);
		}
	}
}