using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UPTK.TPController;

namespace Assets.Player.Scripts.TPController.Default
{
    public class KeyboardJumpInput : MonoBehaviour, IJumpInput
    {
        public bool JumpPressed()
        {
            return Input.GetKey(KeyCode.Space);
        }
    }
}
