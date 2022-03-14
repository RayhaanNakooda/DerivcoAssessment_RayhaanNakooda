using System;
using System.Collections.Generic;
using System.Text;

namespace DerivcoAssessment_RayhaanNakooda
{
    class PlayerNames
    {

        private string playerOneName;
        private string playerTwoName;

        //Default constructor
        public PlayerNames()
        { 
        }

        public PlayerNames(string playerOneName, string playerTwoName)
        {
            this.playerOneName = playerOneName;
            this.playerTwoName = playerTwoName;
        }

        public string PlayerOneName { get => playerOneName; set => playerOneName = value; }
        public string PlayerTwoName { get => playerTwoName; set => playerTwoName = value; }
    }
}
