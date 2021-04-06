using Godot;
using System;

public class MainScene : Node2D{
    
    public bool started = false, paused = false;
    

    public override void _Ready(){
        started = false;
        paused = false;

        ((Button) GetNode("Controls/Start_Stop")).Text = "Start";
        ((Button) GetNode("Controls/PauseButton")).Text = "Pause";
        
        Tank IncomeSplitter = (Tank)GetNode("Tank");
        Tank Surplus = (Tank)GetNode("Surplus");
        Tank Transaction = (Tank)GetNode("Transaction");

        Surplus.currentVol = 0.9;
        Transaction.currentVol = 0.1;
        
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
        Tank IncomeSplitter = (Tank)GetNode("IncomeSplitter");
        Tank Surplus = (Tank)GetNode("Surplus");
        Tank Transaction = (Tank)GetNode("Transaction");

        Pipe Consumption = (Pipe)GetNode("Consumption");
        Pipe Saving = (Pipe)GetNode("Saving");
        Pipe Expenditure = (Pipe)GetNode("Expenditure");
        Pipe Income = (Pipe)GetNode("Income");

        Consumption.ConnectPipe(IncomeSplitter,Transaction);
        Saving.ConnectPipe(IncomeSplitter,Surplus);
        Expenditure.ConnectPipe(Surplus,Transaction);
        Income.ConnectPipe(Transaction,IncomeSplitter);

        Income.SetPointPosition(3,Income.GetPointPosition(1));
        Income.SetPointPosition(1,Income.GetPointPosition(0)- new Vector2(Transaction.wall_width+50,0));
        Income.SetPointPosition(2,Income.GetPointPosition(3)- new Vector2(Transaction.wall_width+50,0));
        ((RichTextLabel)GetNode("Income/PipeName")).SetPosition((Income.GetPointPosition(1)+Income.GetPointPosition(2))/2);
        Consumption.physicalMaxFlow = 5;
        Saving.physicalMaxFlow = 5;
        Expenditure.physicalMaxFlow = 5;


        Consumption.currentMaxFlow = (double)Math.Sqrt(IncomeSplitter.currentVol/IncomeSplitter.crossArea);
        Saving.currentMaxFlow = (double)Math.Sqrt(IncomeSplitter.currentVol/IncomeSplitter.crossArea);
        Expenditure.currentMaxFlow = (double)Math.Sqrt(Surplus.currentVol/Surplus.crossArea);
        
        Consumption.currentFlow = Consumption.currentMaxFlow * 0.2;
        Saving.currentFlow = Saving.currentMaxFlow * slider2;
        Expenditure.currentFlow = Expenditure.currentMaxFlow * slider3;

    }


}
