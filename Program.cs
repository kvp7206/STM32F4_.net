using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System.Diagnostics;
using Microsoft.SPOT.Hardware.STM32F4;
using System.Threading;
namespace MFConsoleApplication1
{
    public class Program
    {
        public static void TimerCallback(object state)
        {
            Debug.Print(DateTime.Now.Second.ToString());
        }

        public static void Main()
        {
            InterruptPort przycisk = 
                new InterruptPort(Pins.GPIO_PIN_A_0, false, Port.ResistorMode.PullDown, Port.InterruptMode.InterruptEdgeLevelHigh);
            var green = new PWM(Cpu.PWMChannel.PWM_0, 300, 0, false);
            var orange = new PWM(Cpu.PWMChannel.PWM_1, 300, 0, false);
            var red = new PWM(Cpu.PWMChannel.PWM_2, 300, 0, false); 
            var blue= new PWM(Cpu.PWMChannel.PWM_3, 300, 0, false);/
            green.Start();
            orange.Start();
            red.Start();
            blue.Start();
            int x = 0, y=0;
            var timer = new System.Threading.Timer(TimerCallback, null, 0, 1000);
            while (true)
            {
                x = DateTime.Now.Second;
                while (DateTime.Now.Second >= 52) { }
                if (przycisk.Read())
                {
                    green.DutyCycle = 0;
                    blue.DutyCycle = 0;
                    orange.DutyCycle = 0;
                    red.DutyCycle = 0;
                }
                switch(x%13){
                    case 0:
                        red.DutyCycle = 0.3;
                        break;
                    case 1:
                        red.DutyCycle = 0.6;
                        break;
                    case 2:
                        red.DutyCycle = 1;
                        break;
                    case 3:
                        blue.DutyCycle = 0.3;
                        break;
                    case 4:
                        blue.DutyCycle = 0.6;
                        break;
                    case 5:
                        blue.DutyCycle = 1;
                        break;
                    case 6:
                        green.DutyCycle = 0.3;
                        break;
                    case 7:
                        green.DutyCycle = 0.6;
                        break;
                    case 8:
                        green.DutyCycle = 1;
                        break;
                    case 9:
                        orange.DutyCycle = 0.3;
                        break;
                    case 10:
                        orange.DutyCycle = 0.6;
                        break;
                    case 11:
                        orange.DutyCycle = 1;
                        break;
                    case 12:
                        red.DutyCycle = 0;
                        blue.DutyCycle = 0;
                        orange.DutyCycle = 0;
                        green.DutyCycle = 0;
                        break;
                }
            }  
        }
    }
}
