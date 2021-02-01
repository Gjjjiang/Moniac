using Godot;
using System;

public class ValveControl : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    [Signal]
    public delegate void _sliderValue(float slider);
    void _on_HSlider_value_changed(float input){
       EmitSignal(nameof(_sliderValue),input);
    }
}
