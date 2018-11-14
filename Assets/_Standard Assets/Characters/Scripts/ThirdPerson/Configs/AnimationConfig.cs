﻿using System;
using StandardAssets.Characters.Helpers;
using UnityEngine;
using UnityEngine.Serialization;

namespace StandardAssets.Characters.ThirdPerson.Configs
{
	/// <summary>
	/// Data model class containing various settings for animation
	/// </summary>
	[CreateAssetMenu(fileName = "Third Person Animation Configuration", 
		menuName = "Standard Assets/Characters/Third Person Animation Configuration", order = 1)]
	public class AnimationConfig : ScriptableObject
	{
		[Serializable]
		protected class AdvancedAnimationConfig
		{
			[Header("Ground Movement")]
			[SerializeField, Tooltip("Configuration for the forward speed animation parameter")]
			FloatRange m_ForwardSpeedInterpolationRange = new FloatRange(0.2f, 0.35f);

			[SerializeField, Tooltip("Configuration for the lateral speed animation parameter")]
			FloatRange m_LateralSpeedInterpolationRange = new FloatRange(0.2f, 0.35f);

			[SerializeField, Tooltip("Configuration for the turning speed animation parameter")]
			FloatRange m_TurningSpeedInterpolationRange = new FloatRange(0.01f, 0.05f);
			
			[SerializeField, Tooltip("Input change angle threshold used to trigger a strafe rapid direction change")]
			float m_StrafeRapidDirectionChangeAngle = 140.0f;
			
			[SerializeField, Tooltip("Should a strafe rapid direction change be detected and smoothed. This should only " +
			                         "be enabled if opposing strafe animations are reverses of each other. eg walk " +
			                         "backwards is walk forward played at a -1 speed")]
			bool m_EnableStrafeRapidDirectionChangeSmoothing = true;
			
			[SerializeField, Tooltip("Curve used to change animator movement speeds during a strafe rapid direction change")]
			AnimationCurve m_StrafeRapidDirectionChangeSpeedCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
		
			[SerializeField, Tooltip("Curve used to remap raw normailized turning speed")]
			AnimationCurve m_TurningSpeedCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
					
			[Header("Jumping")]
			[SerializeField, Tooltip("Curve used to determine the cross fade duration of the transition into the jump " +
			                         "animation state in exploration mode")]
			AnimationCurve m_JumpTransitionAsAFactorOfSpeed = AnimationCurve.Constant(0.0f, 1.0f, 0.15f);
		
			[SerializeField, Tooltip("Curve used to determine the cross fade duration of the transition into the jump " +
			                         "animation state in strafe mode")]
			AnimationCurve m_StrafeJumpTransitionAsAFactorOfSpeed = AnimationCurve.Constant(0.0f, 1.0f, 0.15f);

			[SerializeField, Tooltip("Curve used to determine the cross fade duration of the transition into the " +
			                         "locomotion animation from the jump animation state")]
			AnimationCurve m_JumpEndTransitionAsAFactorOfSpeed = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 0.125f);
			
			[SerializeField, Tooltip("Cross fade cycle offset for transition into locomotion state after a right foot jump")]
			float m_RightFootJumpLandAnimationTimeOffset = 0.6f;
			
			[SerializeField, Tooltip("Cross fade cycle offset for transition into locomotion state after a left foot jump")]
			float m_LeftFootJumpLandAnimationTimeOffset = 0.3f;

			[SerializeField, Tooltip("Time in seconds allowed between jumps to create a skip effect")]
			float m_SkipJumpLandWindow = 0.38f;
			
			[Header("Landing")]
			[SerializeField, Tooltip("Curve used to determine the land animation speed")]
			AnimationCurve m_LandSpeedAsAFactorOfSpeed = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 2.0f);
			
			[SerializeField, Tooltip("Time used for the cross fade into the roll animation state")]
			float m_RollAnimationBlendTime = 0.15f;

			[SerializeField, Tooltip("Time used for the cross fade into the land animation state")]
			float m_LandAnimationBlendTime = 0.11f;
			
			[Header("Grounded Foot")]
			[SerializeField, Tooltip("Should the right foot start as grounded? Default is the left foot")]
			bool m_StartRightFootGrounded;
			
			[Header("Head Turn")]
			[SerializeField, Tooltip("Configuration for the head turning/looking")]
			HeadTurnProperties m_HeadTurnProperties;

			public bool enableStrafeRapidDirectionChangeSmoothing
			{
				get { return m_EnableStrafeRapidDirectionChangeSmoothing; }
			}
			
			public HeadTurnProperties headTurnProperties
			{
				get { return m_HeadTurnProperties; }
			}

			public bool startRightFootGrounded
			{
				get { return m_StartRightFootGrounded; }
			}

			public float landAnimationBlendTime
			{
				get { return m_LandAnimationBlendTime; }
			}

			public float rollAnimationBlendTime
			{
				get { return m_RollAnimationBlendTime; }
			}

			public AnimationCurve landSpeedAsAFactorOfSpeed
			{
				get { return m_LandSpeedAsAFactorOfSpeed; }
			}

			public float skipJumpLandWindow
			{
				get { return m_SkipJumpLandWindow; }
			}

			public float leftFootJumpLandAnimationTimeOffset
			{
				get { return m_LeftFootJumpLandAnimationTimeOffset; }
			}

			public float rightFootJumpLandAnimationTimeOffset
			{
				get { return m_RightFootJumpLandAnimationTimeOffset; }
			}

			public AnimationCurve jumpEndTransitionAsAFactorOfSpeed
			{
				get { return m_JumpEndTransitionAsAFactorOfSpeed; }
			}

			public AnimationCurve strafeJumpTransitionAsAFactorOfSpeed
			{
				get { return m_StrafeJumpTransitionAsAFactorOfSpeed; }
			}

			public AnimationCurve jumpTransitionAsAFactorOfSpeed
			{
				get { return m_JumpTransitionAsAFactorOfSpeed; }
			}

			public AnimationCurve turningSpeedCurve
			{
				get { return m_TurningSpeedCurve; }
			}

			public AnimationCurve strafeRapidDirectionChangeSpeedCurve
			{
				get { return m_StrafeRapidDirectionChangeSpeedCurve; }
			}

			public float strafeRapidDirectionChangeAngle
			{
				get { return m_StrafeRapidDirectionChangeAngle; }
			}

			public FloatRange turningSpeedInterpolationRange
			{
				get { return m_TurningSpeedInterpolationRange; }
			}

			public FloatRange lateralSpeedInterpolationRange
			{
				get { return m_LateralSpeedInterpolationRange; }
			}

			public FloatRange forwardSpeedInterpolationRange
			{
				get { return m_ForwardSpeedInterpolationRange; }
			}
		}
		
		// values used to determine the grounded foot based on animation normalized time. These should only be changed
		// if locomotion animations are irregular.
		const float k_GroundedFootThreshold = 0.25f;
		const float k_GroundedFootThresholdOffset = 0.25f;

		[FormerlySerializedAs("landSpeedAsAFactorOfSpeed")]
		[Header("Landing")]
		[FormerlySerializedAs("normalizedForwardSpeedToRoll")]
		[SerializeField, Tooltip("A forward speed higher than this will trigger a roll on land")]
		float m_NormalizedForwardSpeedRequiredToRoll = 0.3f;

		[FormerlySerializedAs("fallTimeRequiredToTriggerRoll")]
		[SerializeField, Tooltip("A fall time greater than this will trigger a roll. Less than this will transition to" +
								 "locomotion")]
		float m_FallTimeRequiredToTriggerRoll = 1.0f;


		[FormerlySerializedAs("enableHeadTurn")]
		[Header("Head Movement"), Tooltip("Should the head look be turned off?")]
		[SerializeField]
		bool m_EnableHeadTurn = true;

		[SerializeField, Space]
		private AdvancedAnimationConfig advancedSettings;

		/// <summary>
		/// Gets whether strafe rapid direction smoothing logic should be performed.
		/// </summary>
		public bool enableStrafeRapidDirectionChangeSmoothingLogic
		{
			get { return advancedSettings.enableStrafeRapidDirectionChangeSmoothing; }
		}
		
		/// <summary>
		/// Gets the angle threshold used to trigger a strafe rapid direction change.
		/// </summary>
		public float strafeRapidChangeAngleThreshold
		{
			get { return advancedSettings.strafeRapidDirectionChangeAngle; }
		}
		
		/// <summary>
		/// Gets the curve used to interpolate animator movement speeds during a strafe rapid direction change.
		/// </summary>
		public AnimationCurve strafeRapidChangeSpeedCurve
		{
			get { return advancedSettings.strafeRapidDirectionChangeSpeedCurve; }
		}
		
		/// <summary>
		/// Gets the animation curved used for remapping turning speed.
		/// </summary>
		public AnimationCurve animationTurningSpeedCurve
		{
			get { return advancedSettings.turningSpeedCurve; }
		}

		/// <summary>
		/// Gets the forward speed parameter configuration
		/// </summary>
		public FloatRange forwardSpeedInterpolation
		{
			get { return advancedSettings.forwardSpeedInterpolationRange; }
		}

		/// <summary>
		/// Gets the lateral speed parameter configuration
		/// </summary>
		public FloatRange lateralSpeedInterpolation
		{
			get { return advancedSettings.lateralSpeedInterpolationRange; }
		}

		/// <summary>
		/// Gets the turning speed parameter configuration
		/// </summary>
		public FloatRange turningSpeedInterpolation
		{
			get { return advancedSettings.turningSpeedInterpolationRange; }
		}

		/// <summary>
		/// Gets whether the right foot should start as grounded
		/// </summary>
		/// <value>True if the right foot should start grounded; false if the left foot should.</value>
		public bool invertFoot
		{
			get { return advancedSettings.startRightFootGrounded; }
		}

		/// <summary>
		/// Gets the threshold value used in determining the current grounded foot.
		/// </summary>
		public float groundedFootThresholdValue
		{
			get { return k_GroundedFootThreshold; }
		}

		/// <summary>
		/// Gets the threshold offset value used in determining the current grounded foot.
		/// </summary>
		public float groundedFootThresholdOffsetValue
		{
			get { return k_GroundedFootThresholdOffset; }
		}

		/// <summary>
		/// Gets whether the head turning/look at should be enabled
		/// </summary>
		/// <value>True if the head turning should enabled; false if it is to be disabled. </value>
		public bool enableHeadLookAt
		{
			get { return m_EnableHeadTurn; }
		}

		/// <summary>
		/// Gets the head turn look at weight.
		/// </summary>
		public float lookAtWeight
		{
			get { return advancedSettings.headTurnProperties.lookAtWeight; }
		}

		/// <summary>
		/// Gets the head turn look at max rotation.
		/// </summary>
		public float lookAtMaxRotation
		{
			get { return advancedSettings.headTurnProperties.lookAtMaxRotation; }
		}

		/// <summary>
		/// Gets the head turn look at rotation speed.
		/// </summary>
		public float lookAtRotationSpeed
		{
			get { return advancedSettings.headTurnProperties.lookAtRotationSpeed; }
		}
		
		/// <summary>
		/// Gets whether head turning should be disabled in an aerial state.
		/// </summary>
		public bool lookAtWhileAerial
		{
			get { return advancedSettings.headTurnProperties.lookAtWhileAerial; }
		}

		/// <summary>
		/// Gets whether head turning should be disabled in a turnaround state.
		/// </summary>
		public bool lookAtWhileTurnaround
		{
			get { return advancedSettings.headTurnProperties.lookAtWhileTurnaround; }
		}

		/// <summary>
		/// Gets the curve to be used to evaluate the transition duration out of the jump state.
		/// </summary>
		public AnimationCurve jumpEndTransitionAsAFactorOfSpeed
		{
			get { return advancedSettings.jumpEndTransitionAsAFactorOfSpeed; }
		}

		/// <summary>
		/// Gets the offset used during the cross fade out of right foot physics jump.
		/// </summary>
		public float rightFootJumpLandAnimationOffset
		{
			get { return advancedSettings.rightFootJumpLandAnimationTimeOffset; }
		}

		/// <summary>
		/// Gets the offset used during the cross fade out of left foot physics jump.
		/// </summary>
		public float leftFootJumpLandAnimationOffset
		{
			get { return advancedSettings.leftFootJumpLandAnimationTimeOffset; }
		}
		
		/// <summary>
		/// Gets the time allowed between physics jumps to alternate the grounded foot.
		/// </summary>
		public float skipJumpWindow
		{
			get { return advancedSettings.skipJumpLandWindow; }
		}

		/// <summary>
		/// Gets the curve to be used to evaluate the animation speed of a land animation.
		/// </summary>
		public AnimationCurve landSpeedAsAFactorSpeed
		{
			get { return advancedSettings.landSpeedAsAFactorOfSpeed; }
		}

		/// <summary>
		/// Gets the normalized forward speed required to initiate a roll during a land.
		/// </summary>
		public float forwardSpeedRequiredToRoll
		{
			get { return m_NormalizedForwardSpeedRequiredToRoll; }
		}

		/// <summary>
		/// Gets the fall time in seconds required to trigger a roll on land.
		/// </summary>
		public float fallTimeRequiredToRoll
		{
			get { return m_FallTimeRequiredToTriggerRoll; }
		}

		/// <summary>
		/// Gets the duration of the transition into the land animator state.
		/// </summary>
		public float landAnimationBlendDuration
		{
			get { return advancedSettings.landAnimationBlendTime; }
		}

		/// <summary>
		/// Gets the duration of the transition into the roll land animator state.
		/// </summary>
		public float rollAnimationBlendDuration
		{
			get { return advancedSettings.rollAnimationBlendTime; }
		}

		/// <summary>
		/// Gets the curve to be used to evaluate the transition duration into the jump state in exploration mode.
		/// </summary>
		public AnimationCurve jumpTransitionAsAFactorOfSpeed
		{
			get { return advancedSettings.jumpTransitionAsAFactorOfSpeed; }
		}
		
		/// <summary>
		/// Gets the curve to be used to evaluate the transition duration into the jump state in strafe mode.
		/// </summary>
		public AnimationCurve strafeJumpTransitionAsAFactorOfSpeed
		{
			get { return advancedSettings.strafeJumpTransitionAsAFactorOfSpeed; }
		}

		/// <summary>
		/// Gets the scale applied to head look at speed when there is no look input.
		/// </summary>
		public float noLookInputHeadLookAtScale
		{
			get { return advancedSettings.headTurnProperties.noLookInputHeadLookAtScale; }
		}
		
		/// <summary>
		/// A serializable class used to store configuration settings for the head turing/look at.
		/// </summary>
		[Serializable]
		protected class HeadTurnProperties
		{
			[FormerlySerializedAs("headLookAtWeight")]
			[SerializeField, Tooltip("The animator head look at weight.")]
			protected float m_HeadLookAtWeight = 1f;

			[FormerlySerializedAs("headLookAtMaxRotation")]
			[SerializeField, Tooltip("The max angle the head can rotate.")]
			protected float m_HeadLookAtMaxRotation = 75f;

			[FormerlySerializedAs("headLookAtRotationSpeed")]
			[SerializeField, Tooltip("The speed at which head can rotate.")]
			protected float m_HeadLookAtRotationSpeed = 15f;
			
			[FormerlySerializedAs("noInputHeadLookAtScale")]
			[SerializeField, Tooltip("A scale applied to look at speed when there is no look input.")]
			protected float m_NoInputHeadLookAtScale = 0.5f;

			[FormerlySerializedAs("adjustHeadLookAtWhileAerial")]
			[SerializeField, Tooltip("Should head rotation take place while aerial?")]
			protected bool m_AdjustHeadLookAtWhileAerial = true;
		
			[FormerlySerializedAs("adjustHeadLookAtDuringTurnaround")]
			[SerializeField, Tooltip("Should head rotation take place during rapid turnarounds?")]
			protected bool m_AdjustHeadLookAtDuringTurnaround = true;
		
			/// <summary>
			/// Gets the look at weight used by the animator.
			/// </summary>
			public float lookAtWeight
			{
				get { return m_HeadLookAtWeight; }
			}

			/// <summary>
			/// Gets the max look at rotation.
			/// </summary>
			public float lookAtMaxRotation
			{
				get { return m_HeadLookAtMaxRotation; }
			}

			/// <summary>
			/// Gets the rotation look at speed.
			/// </summary>
			public float lookAtRotationSpeed
			{
				get { return m_HeadLookAtRotationSpeed; }
			}

			/// <summary>
			/// Gets whether the head look at should be applied while aerial.
			/// </summary>
			public bool lookAtWhileAerial
			{
				get { return m_AdjustHeadLookAtWhileAerial; }
			}

			/// <summary>
			/// Gets whether the head look at should be applied during a turnaround.
			/// </summary>
			public bool lookAtWhileTurnaround
			{
				get { return m_AdjustHeadLookAtDuringTurnaround; }
			}

			/// <summary>
			/// Gets the scale applied to look at speed when there is no look input.
			/// </summary>
			public float noLookInputHeadLookAtScale
			{
				get { return m_NoInputHeadLookAtScale; }
			}
		}
	}
}