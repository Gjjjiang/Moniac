using Godot;
using System;

public class EditValueScene : Node2D
{

    Tank water_tank;

   

    public override void _Ready(){
        
    }

    public void _on_Tank_on_tank_EditInfo(Tank input){
        water_tank=input;
        this.Visible=true;
        updateTextfields();
        //set all text field to current values

    }
    [Signal] public delegate void _on_EditValue_send_and_set_new_values();
    //public void _on_EditValue_send_and_set_new_values()
    public void _on_Set_pressed(){
        //Get string from respective LineEdit Text Fields
        String CrossSectionText = ((LineEdit) GetNode("Control/VBoxContainer/LineEdit_CrossSectionArea")).Text;
        String CurrentVolText = ((LineEdit) GetNode("Control/VBoxContainer/LineEdit_CurrentVol")).Text;
        String MaxVolText = ((LineEdit) GetNode("Control/VBoxContainer/LineEdit_MaxVol")).Text;
        //Assign Respective Values to Water Tank

        water_tank.crossArea = double.Parse(CrossSectionText);
        water_tank.currentVol = double.Parse(CurrentVolText);
        water_tank.maxVol = double.Parse(MaxVolText);
        water_tank.startVol = water_tank.currentVol;
        EmitSignal(nameof(_on_EditValue_send_and_set_new_values));
        //Pass New water tank to water tank instance
        GetTree().CallGroup("WaterTanks","PauseButton");

        this.Visible=false;
    }
    public void _on_Cancel_Button_pressed(){
        GetTree().CallGroup("WaterTanks","PauseButton");
        this.Visible=false;
    }

    public override void _Process(float delta){

    }

    void updateTextfields(){
        LineEdit CrossSection = (LineEdit) GetNode("Control/VBoxContainer/LineEdit_CrossSectionArea");
        LineEdit CurrentVol = (LineEdit) GetNode("Control/VBoxContainer/LineEdit_CurrentVol");
        LineEdit MaxVol = (LineEdit) GetNode("Control/VBoxContainer/LineEdit_MaxVol");

        CrossSection.Text = water_tank.crossArea.ToString();
        CurrentVol.Text = water_tank.currentVol.ToString();
        MaxVol.Text = water_tank.maxVol.ToString();

    }



}
