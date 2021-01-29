using Godot;
using System;

public class MainScene : Node2D{
    
    public bool started = false, paused = false;
    

    public override void _Ready(){
        started = false;
        paused = false;

        ((Button) GetNode("Controls/Start_Stop")).Text = "Start";
        ((Button) GetNode("Controls/PauseButton")).Text = "Pause";
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta){
        if(started && !paused){
            ((Tank) GetNode("Tank2")).fill_percent = 0.5f;
        }
    }

    public void _on_Start_Stop_pressed(){
        if(!started){
            started = true;
            paused = false;
            ((Button) GetNode("Controls/Start_Stop")).Text = "Reset";

        }
        else{
            GetTree().ReloadCurrentScene();
        }
    }

    public void _on_PauseButton_pressed(){
        if(!paused){
            ((Button) GetNode("Controls/PauseButton")).Text = "Continue";
            paused=true;
        }
        else{
            ((Button) GetNode("Controls/PauseButton")).Text = "Pause";
            paused=false;
        }
    }

    /*
    public override void _Input(InputEvent @event){
        // Mouse in viewport coordinates.
        if (@event is InputEventMouseButton eventMouseButton)
            GD.Print("Mouse Click/Unclick at: ", eventMouseButton.Position);
        else if (@event is InputEventMouseMotion eventMouseMotion)
            GD.Print("Mouse Motion at: ", eventMouseMotion.Position);

        // Print the size of the viewport.
        GD.Print("Viewport Resolution is: ", GetViewportRect().Size);
    }
    */
}
