using GTA;
using System;
using System.Windows.Forms;


public class Speed : Script

{
    Boolean carReady = false; // prepare car for bomb.
    Boolean bombReady = false; // check if bomb has been activated.
    Ped player = Game.Player.Character; // reference to player's character model.
    Vehicle theCar; // reference to boom car.
    float activationSpeed = 30; // speed at which the bomb triggers, 30 = roughly 70mph ingame.
    float detonationSpeed = 28; // speed at which the bomb detonates. (slightly lower to bypass the *insta-explode on activation* bug.

    public Speed()
    {
        Tick += OnTick; // happens at every frame.
        //KeyDown += OnKeyDown; // check when key is down.
        KeyUp += OnKeyUp; // check when key is up.



        Interval = 100; //// time interval (how quickly the script runs - the lower the value, the faster the bomb reacts).



    }

    void OnTick(object sender, EventArgs e)
    {
        theCar = player.LastVehicle; // // get name of car that the player is in.
        

        if(carReady == true) // check if car is spawned.
        {
            if (player.IsInVehicle(theCar) == true) // check if player is in the car.
            {
                
                if(theCar.Speed > activationSpeed && bombReady == false) // if speed above 30, activate *speed mode* .
                {
                    UI.ShowSubtitle("Bomb Armed.");
                    bombReady = true; //activate bomb.
                   

                }

                else if(theCar.Speed < detonationSpeed && bombReady == true) // if speed below 28 and bomb is ready then kill player. (Below 28 due to an insta-explode bug).
                {   
                    theCar.Explode();
                    bombReady = false; // deactivate bomb.
                }

                else if(player.IsJumpingOutOfVehicle == true && bombReady == true) // check to see if player tries to leave the car after bomb has been armed. If the player does try to leave, boom.
                {
                    theCar.Explode();
                    bombReady = false;
                }

                

            }
        }

    }

    void OnKeyDown(object sender, KeyEventArgs e)
    {

    }

    void OnKeyUp(object sender, KeyEventArgs e)
    {


        if (e.KeyCode == Keys.K) // if key pressed = k, enable/disable mod.
        {


            if (carReady == false)
            {
                UI.ShowSubtitle("Speed Mode Activated");
                carReady = true;
            }

            else if (carReady == true)
            {
                UI.ShowSubtitle("Speed Mode Deactivated");
                carReady = false;
            }

        }


            if (e.KeyCode == Keys.J && carReady == true) // if key pressed = j, decrease activation and detonation speed by 5.
            {

                activationSpeed = activationSpeed -= 5;
                detonationSpeed = detonationSpeed -= 5;
                UI.ShowSubtitle("Activation Speed Decreased To: " + (activationSpeed));



            }


        if (e.KeyCode == Keys.L && carReady == true) // if key pressed = l, increase activation and detonation speed by 5.
        {

            activationSpeed = activationSpeed += 5;
            detonationSpeed = detonationSpeed += 5;
            UI.ShowSubtitle("Activation Speed Increased To: " + (activationSpeed));



        }
    }

}
