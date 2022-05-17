using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Core;
using  Lightbug.CharacterControllerPro.Implementation;
namespace GWLPXL.ActionCharacter.Integration.CCPro
{
    public class PlayerCCProLoco : LocomotionBase
    {

        protected PlayerCCPro player;
        public PlayerCCProLoco(PlayerCCPro instance, Movement movement) : base(instance, movement)
        {
            this.player = instance;
            this.movement = movement;
        }

        public override void Tick()
        {
            base.Tick();


        }
        protected override void AssignInputs()
        {

            inputx = player.InputWrapper.GetXAxis();
            inputz = player.InputWrapper.GetZAxis();
            base.AssignInputs();

        }



      


    }
    public class PlayerCCPro : PlayerCharacter
    {

        protected PlayerCCProLoco loco;
        protected CharacterActor ccpro;
        protected CharacterController cc;
        protected CharacterBody body;
        protected Rigidbody rb;
        protected CharacterBrain brain;
        protected override void Awake()
        {
            brain = GetComponentInChildren<CharacterBrain>();
            cc = GetComponent<CharacterController>();

            cc.detectCollisions = false;
            ccpro = GetComponent<CharacterActor>();
            body = GetComponent<CharacterBody>();


            base.Awake();
        }



        public override bool TryStartActionSequence(string actionName)
        {
            bool success = base.TryStartActionSequence(actionName);
            if (success)
            {
             //   rb = GetComponent<Rigidbody>();
                brain.enabled = false;

             //   rb.isKinematic = true;
             //   rb.detectCollisions = false;


                ccpro.Velocity = new Vector3(0, 0, 0);
                ccpro.enabled = false;
                body.enabled = false;
                cc.enabled = true;
                cc.Move(new Vector3(0, 0, 0));
            }
            return success;
        }

        public override Vector3 GetCenter()
        {
            return body.ColliderComponent.Center;
        }

        public override float GetHeight()
        {
            return body.ColliderComponent.BoundsSize.y;
        }

        public override float GetRadius()
        {
            return body.ColliderComponent.ContactOffset;
        }

        public override void ActionSequenceEnded(string actionName)
        {
           // rb = GetComponent<Rigidbody>();
            brain.enabled = true;
          //  rb.isKinematic = false;
          //  rb.detectCollisions = true;


            body.enabled = true;
            ccpro.enabled = true;
            cc.enabled = false;

            base.ActionSequenceEnded(actionName);

        }
        public override bool GetGrounded()
        {
            return ccpro.IsGrounded;
        }
        public override void InitializeLocomotion()
        {
            if (loco != null) loco.RemoveTicker();
            loco = new PlayerCCProLoco(this, MovementRuntime.Movement);
            loco.AddTicker();
        }
    

        public override void RemoveLocotion()
        {
            if (loco != null) loco.RemoveTicker();

        }

        public override void Move()
        {
            if (hasOutsideVel && !actionCheck)
            {
                cc.Move(outsideVel);
                hasOutsideVel = false;
                outsideVel = new Vector3(0, 0, 0);
            }
            else if (hasOutsideVel && actionCheck)
            {
               // ccpro.Move(ccpro.Velocity + outsideVel);
                hasOutsideVel = false;
                outsideVel = new Vector3(0, 0, 0);
            }
         
        }
    }
}
