using GTA;
using System;
using System.Windows.Forms;


public class Speed : Script

{
    Boolean carReady = false; // prepare car for bomb.
    Boolean bombReady = false; // check if bomb has been activated.
    Ped player = Game.Player.Character; // reference to player's character model.
    Vehicle theCar; // reference to boom car.

    public Speed()
    {
        Tick += OnTick; // happens at every frame.
        //KeyDown += OnKeyDown; // check when key is down.
        KeyUp += OnKeyUp; // check when key is up.



        Interval = 1000; // time interval.

      

    }

    void OnTick(object sender, EventArgs e)
    {
        theCar = player.LastVehicle; // get name of car that the player is in.
        

        if(carReady == true) // check if car is spawned.
        {
            if (player.IsInVehicle(theCar) == true) // check if player is in the car.
            {
                
                if(theCar.Speed > 30 && bombReady == false) // if speed above 30, activate *speed mode* .
                {
                    UI.ShowSubtitle("Bomb Armed.");
                    bombReady = true; //activate bomb.
                    
                }

                if(theCar.Speed < 28 && bombReady == true) // if speed below 28 and bomb is ready then kill player. (Below 28 due to an insta-explode bug).
                {   
                    theCar.Explode();
                    bombReady = false; // deactivate bomb.
                }

            }
        }

    }

    void OnKeyDown(object sender, KeyEventArgs e)
    {

    }

    void OnKeyUp(object sender, KeyEventArgs e)
    {
       
    
        if(e.KeyCode == Keys.K) // if key pressed = k, enable/disable mod.
        {
            

            if(carReady == false)
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
    }

}
