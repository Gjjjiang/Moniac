using Godot;
using System;
using System.Collections.Generic; 
using System.Runtime.InteropServices;
unsafe public class MainScene : Node2D{
     [DllImport ("co2modelfunc")] unsafe static extern void co2modelfunc(double A, double F, double L, double O, double *sol_tA, double
                  *sol_tF, double *sol_tL, double *sol_tO);


    public bool started = false, paused = false;
    

    public override void _Ready(){
        
        
        double sol1 =0,sol2 = 0,sol3 = 0, sol4 = 0;

        co2modelfunc(1,2,3,4, &sol1, &sol2, &sol3, &sol4);

        started = false;
        paused = false;

        ((Button) GetNode("Controls/Start_Stop")).Text = "Start";
        ((Button) GetNode("Controls/PauseButton")).Text = "Pause";
        
        Tank tank1 = (Tank)GetNode("Tank");
        Tank tank2 = (Tank)GetNode("Tank2");
        Tank tank3 = (Tank)GetNode("Tank3");

        tank1.currentVol = 5;
        tank2.currentVol = 0;
        tank3.currentVol = 0;
        flowUpdate();

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta){
        flowUpdate();
    }

    public void _on_Start_Stop_pressed(){
        if(!started){
            started = true;
            paused = false;
            ((Button) GetNode("Controls/Start_Stop")).Text = "Reset";
            GetTree().CallGroup("WaterTanks","StartStop");

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
        GetTree().CallGroup("WaterTanks","PauseButton");
    }
    public double slider,slider2,slider3;
    public void flowUpdate(){
        Tank tank1 = (Tank)GetNode("Tank");
        Tank tank2 = (Tank)GetNode("Tank2");
        Tank tank3 = (Tank)GetNode("Tank3");

        Pipe pipe1 = (Pipe)GetNode("Pipe");
        Pipe pipe2 = (Pipe)GetNode("Pipe2");
        Pipe pipe3 = (Pipe)GetNode("Pipe3");

        pipe1.ConnectPipe(tank1,tank3);
        pipe2.ConnectPipe(tank1,tank2);
        pipe3.ConnectPipe(tank2,tank3);

        pipe1.physicalMaxFlow = 5;
        pipe2.physicalMaxFlow = 5;
        pipe3.physicalMaxFlow = 5;


        pipe1.currentMaxFlow = (double)Math.Sqrt(tank1.currentVol/tank1.crossArea);
        pipe2.currentMaxFlow = (double)Math.Sqrt(tank1.currentVol/tank1.crossArea);
        pipe3.currentMaxFlow = (double)Math.Sqrt(tank2.currentVol/tank2.crossArea);
        
        pipe1.currentFlow = pipe1.currentMaxFlow * slider;
        pipe2.currentFlow = pipe2.currentMaxFlow * slider2;
        pipe3.currentFlow = pipe3.currentMaxFlow * slider3;

    }

    void _on_HSlider_value_changed(float in_slider){
        slider =(double) (in_slider/100);
    }

    void _on_HSlider2_value_changed(float in_slider){
        slider2 = (double) (in_slider/100);
    }

    void _on_HSlider3_value_changed(float in_slider){
        slider3 = (double) (in_slider/100);
    }
}
