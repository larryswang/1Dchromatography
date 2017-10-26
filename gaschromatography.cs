using NationalInstruments;
using NationalInstruments.DAQmx;
//using NationalInstruments.UI;
//using NationalInstruments.UI.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace MyFirstDAQApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /*parameters defination*/
        #region **List data**
        public List<float> x1 = new List<float>();
        public List<float> y1 = new List<float>();
        public List<float> x2 = new List<float>();
        public List<float> y2 = new List<float>();
        public List<float> x3 = new List<float>();
        public List<float> y3 = new List<float>();
        #endregion
        /*ports defination*/
        private string P0_0="dev1/port0/line0";//pump
        private string P0_1= "dev1/port0/line1";//valve
        private string P0_2 = "dev1/port0/line2";//preconhigh
        private string P0_3 = "dev1/port0/line3";//preconlow
        /*packed methods*/
        private void digitalOutHigh(int selectPort)
        {
            switch(selectPort)
            {
                case 0:
                    Task digitalOutTask0 = new Task();
                    DOChannel DOChannel0;
                    DOChannel0 = digitalOutTask0.DOChannels.CreateChannel(P0_0, "DOChannel0",
                        ChannelLineGrouping.OneChannelForEachLine);
                    DigitalSingleChannelWriter writer0 = new DigitalSingleChannelWrit-er(digitalOutTask0.Stream);
                    writer0.WriteSingleSamplePort(true, 1);
                    break;
                case 1:
                    Task digitalOutTask1 = new Task();
                    DOChannel DOChannel1;
                    DOChannel1 = digitalOutTask1.DOChannels.CreateChannel(P0_1, "DOChannel1",
                        ChannelLineGrouping.OneChannelForEachLine);
                    DigitalSingleChannelWriter writer1 = new DigitalSingleChannelWrit-er(digitalOutTask1.Stream);
                    writer1.WriteSingleSamplePort(true, -1);
                    break;
                case 2:
                    Task digitalOutTask2 = new Task();
                    DOChannel DOChannel2;
                    DOChannel2 = digitalOutTask2.DOChannels.CreateChannel(P0_2, "DOChannel2",
                        ChannelLineGrouping.OneChannelForEachLine);
                    DigitalSingleChannelWriter writer2 = new DigitalSingleChannelWrit-er(digitalOutTask2.Stream);
                    writer2.WriteSingleSamplePort(true, -1);
                    break;
                case 3:
                    Task digitalOutTask3 = new Task();
                    DOChannel DOChannel3;
                    DOChannel3 = digitalOutTask3.DOChannels.CreateChannel(P0_3, "DOChannel3",
                        ChannelLineGrouping.OneChannelForEachLine);
                    DigitalSingleChannelWriter writer3 = new DigitalSingleChannelWrit-er(digitalOutTask3.Stream);
                    writer3.WriteSingleSamplePort(true, -1);
                    break;
            }
         }
        private void digitalOutLow(int selectPort)
        {
            switch (selectPort)
            {
                case 0:
                    Task digitalOutTask0 = new Task();
                    DOChannel DOChannel0;
                    DOChannel0 = digitalOutTask0.DOChannels.CreateChannel(P0_0, "DOChannel0",
                        ChannelLineGrouping.OneChannelForEachLine);
                    DigitalSingleChannelWriter writer0 = new DigitalSingleChannelWrit-er(digitalOutTask0.Stream);
                    writer0.WriteSingleSamplePort(true, 0);
                    break;
                case 1:
                    Task digitalOutTask1 = new Task();
                    DOChannel DOChannel1;
                    DOChannel1 = digitalOutTask1.DOChannels.CreateChannel(P0_1, "DOChannel1",
                        ChannelLineGrouping.OneChannelForEachLine);
                    DigitalSingleChannelWriter writer1 = new DigitalSingleChannelWrit-er(digitalOutTask1.Stream);
                    writer1.WriteSingleSamplePort(true, 0);
                    break;
                case 2:
                    Task digitalOutTask2 = new Task();
                    DOChannel DOChannel2;
                    DOChannel2 = digitalOutTask2.DOChannels.CreateChannel(P0_2, "DOChannel2",
                        ChannelLineGrouping.OneChannelForEachLine);
                    DigitalSingleChannelWriter writer2 = new DigitalSingleChannelWrit-er(digitalOutTask2.Stream);
                    writer2.WriteSingleSamplePort(true, 0);
                    break;
                case 3:
                    Task digitalOutTask3 = new Task();
                    DOChannel DOChannel3;
                    DOChannel3 = digitalOutTask3.DOChannels.CreateChannel(P0_3, "DOChannel3",
                        ChannelLineGrouping.OneChannelForEachLine);
                    DigitalSingleChannelWriter writer3 = new DigitalSingleChannelWrit-er(digitalOutTask3.Stream);
                    writer3.WriteSingleSamplePort(true, 0);
                    break;
            }
        }
        private double analogIn1()
        {
            Task analogInTask = new Task();
            AIChannel myAIChannel;
            myAIChannel = analogInTask.AIChannels.CreateVoltageChannel("dev1/ai0", "myAIChannel",
                AITerminalConfiguration.Differential, 0, 5, AIVoltageUnits.Volts);
            AnalogSingleChannelReader reader = new AnalogSingleChannelRead-er(analogInTask.Stream);
            double analogDataIn = reader.ReadSingleSample();
            return analogDataIn;
        }
        private double analogIn2()
        {
            Task analogInTask = new Task();
            AIChannel myAIChannel;
            myAIChannel = analogInTask.AIChannels.CreateVoltageChannel("dev1/ai1", "myAIChannel",
                AITerminalConfiguration.Differential, 0, 5, AIVoltageUnits.Volts);
            AnalogSingleChannelReader reader = new AnalogSingleChannelRead-er(analogInTask.Stream);
            double analogDataIn = reader.ReadSingleSample();
            return analogDataIn;
        }

        private void pwmout3(int percentage)
        {
            for (int i = 0; i < 100; i++)
            {
                if (i < percentage)
                {
                    digitalOutHigh(3);
                    System.Threading.Thread.Sleep(2);
                }
                else
                {
                    digitalOutLow(3);
                    System.Threading.Thread.Sleep(2);
                }
            }
        }

        //private double psetPoint = 0;
        //private double pprocessValue = 0;
        //private double poutPut = 0;
        private double error = 0;
        private double integral = 0;
        private double derivative = 0;
        private double preError = 0;
     /*   public double setPoint
        {
            get { return psetPoint; }
            set
            {
                psetPoint = value;
            }
        }
        public double processValue
        {
            get { return pprocessValue; }
            set
            {
                pprocessValue = value;
            }
        }
        public double outPut
        {
            get { return poutPut; }
            set
            {
                poutPut = value;
            }
        }*/
        private double tempFunction(double lowTemp, double holdingTime, double ramp-ingSpeed, double rampingTime)
        {
            if (timerDrawI <= holdingTime)
            {
                return lowTemp;
            }
            else if (timerDrawI <= (holdingTime + rampingTime))
            {
                return lowTemp + rampingSpeed * (timerDrawI - holdingTime);
            }
            else
            {
                return lowTemp + rampingSpeed * rampingTime;
            }
        }

        private double Dt = 100;
        private double Kp = 0.04;
        private double Ki = 0.03;
        private double Kd = 0.06;
        private double PIDControl(double setPoint, double processValue)
        {
            double outPut;
            error = setPoint - processValue;
            integral = integral + (error * Dt);
            derivative = (error - preError) / Dt;
            outPut = (Kp * error) + (Ki * integral) + (Kd * derivative);
            preError = error;
            return outPut;
        }
        
/*******************************************************************************************************************/
        private void btnGetAnalogIn_Click(object sender, EventArgs e)//start the timer when click the button
        {
            timerDraw.Start();//start timer
            zGraphTest.f_ClearAllPix();
            zGraph1.f_ClearAllPix();
            zGraphTest.f_reXY();
            zGraph1.f_reXY();
            zGraphTest.f_LoadOnePix(ref x1, ref y1, Color.Red, 2);
            zGraph1.f_LoadOnePix(ref x2, ref y2, Color.Red, 2);
            zGraph1.f_AddPix(ref x3, ref y3, Color.Blue, 2);
        }
 
        private void zGraphTest_Load(object sender, EventArgs e)
        {
        }
        private void zGraph1_Load(object sender, EventArgs e)
        {

        }
        private float timerDrawI = 0;      //declare the parameter of drawing
        private const double baseLine = 1.4;//loop baseline voltage=1.4v
        private void timerDraw_Tick(object sender, EventArgs e)//start timer and draw
        {
            double analogDataIn=analogIn1()+baseLine;
            //double setTemp = tempFunction(25, 10, 10, 10);
            double setTemp = 0;
            double actualTemp = analogIn2()*175000/7+35000;//thermal couple:25C volt-age=0mv, 200C voltage=7mv
            txtAnalogIn.Text = analogDataIn.ToString();//display in textbox
            x1.Add(timerDrawI);
            y1.Add((float)analogDataIn);
            x2.Add(timerDrawI);
            y2.Add((float)setTemp);
            x3.Add(timerDrawI);
            y3.Add((float)actualTemp);
            timerDrawI =(float)(timerDrawI+0.25);//for each count the x add 0.1
            zGraphTest.f_Refresh();//sampling
            zGraph1.f_Refresh();
        }

/*******************************************************************************************************************/
        
        private void overallStart_Click(object sender, EventArgs e)
        {
            timerMenu.Start();
            startTime = System.DateTime.Now;
        }

        private DateTime startTime;
        private void timerMenu_Tick(object sender, EventArgs e)//flow control loop
        {
            double pumpingT = float.Parse(pumpingTime.Text);
            double waitingT = float.Parse(waitingTime.Text);
            double preconHighT = float.Parse(preconHighTime.Text);
            double preconLowT = float.Parse(preconLowTime.Text);
            timeUsed.Text = Sys-tem.DateTime.Now.Subtract(startTime).TotalSeconds.ToString();// time has been used
            if (System.DateTime.Now.Subtract(startTime).TotalMilliseconds < (pumpingT * 1000))
            {
                digitalOutHigh(0); pumpIndicator.BackColor = Color.Green;
                digitalOutHigh(1); valveIndicator.BackColor = Color.Green;
                digitalOutLow(2); preconOnIndicator.BackColor = Color.Gray;
                digitalOutLow(3); preconHighIndicator.BackColor = Color.Gray;
                //indicator bars, pumping progress indicator range from 0-100 while analyze bar not change
                pumpProgress.Value = (int)((System.DateTime.Now.Subtract(startTime).TotalMilliseconds) / (pumpingT * 10));
                analyzeProgress.Value = 0;
            }
            else if (System.DateTime.Now.Subtract(startTime).TotalMilliseconds < ((pumpingT + waitingT) * 1000))
            {
                digitalOutLow(0); pumpIndicator.BackColor = Color.Gray;
                digitalOutLow(1); valveIndicator.BackColor = Color.Gray;
                digitalOutLow(2); preconOnIndicator.BackColor = Color.Gray;
                digitalOutLow(3); preconHighIndicator.BackColor = Color.Gray;
                pumpProgress.Value = 100;
                analyzeProgress.Value = 0;
            }
            else if (System.DateTime.Now.Subtract(startTime).TotalMilliseconds < ((pumpingT + waitingT + preconHighT) * 1000))
            {
                digitalOutLow(0); pumpIndicator.BackColor = Color.Gray;
                digitalOutLow(1); valveIndicator.BackColor = Color.Gray;
                digitalOutHigh(2); preconOnIndicator.BackColor = Color.Green;
                digitalOutHigh(3); preconHighIndicator.BackColor = Color.Green;
            }
            else if (System.DateTime.Now.Subtract(startTime).TotalMilliseconds < ((pumpingT + waitingT + preconHighT + preconLowT) * 1000))
            {
                digitalOutLow(0); pumpIndicator.BackColor = Color.Gray;
                digitalOutLow(1); valveIndicator.BackColor = Color.Gray;
                digitalOutHigh(2); preconOnIndicator.BackColor = Color.Green;
                pwmout3(50); preconHighIndicator.BackColor = Color.Gray;
            }
            else
            {
                digitalOutLow(0); pumpIndicator.BackColor = Color.Gray;//clear all ex-pected
                digitalOutLow(1); valveIndicator.BackColor = Color.Gray;
                digitalOutLow(2); preconOnIndicator.BackColor = Color.Gray;
                digitalOutLow(3); preconHighIndicator.BackColor = Color.Gray;
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            Task analogInTask = new Task();
            AIChannel myAIChannel;
            myAIChannel = analogInTask.AIChannels.CreateVoltageChannel("dev1/ai0", "myAIChannel",
            AITerminalConfiguration.Differential, 0, 5, AIVoltageUnits.Volts);
            AnalogSingleChannelReader reader = new AnalogSingleChannelRead-er(analogInTask.Stream);
            double[] test1 = reader.ReadMultiSample(-1);
            double procedureTime = Sys-tem.DateTime.Now.Subtract(start).TotalMilliseconds;
            textBox1.Text = procedureTime.ToString();
        }
    }
}

