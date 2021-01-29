using Godot;
using System;

public class PauseButton : Button
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    bool paused= false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
/*
    public void _on_PauseButton(){
        if(!paused){
            ((Button) GetNode("PauseButton")).Text = "Continue";
            paused=true;
        }
        else{
            ((Button) GetNode("PauseButton")).Text = "Pause";
            paused=false;
        }
    }*/
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
