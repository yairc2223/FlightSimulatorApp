using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Controls.xaml
    /// </summary>
    public partial class Controls : UserControl
    {
            #region Sliders (Aileron & Throttle)
            // Suitable properties for sliders
            private double aileron;
            public double Aileron
            {
                get { return this.aileron; }
                set
                {
                    if (this.aileron != value)
                    {
                        this.aileron = value;
                    }
                }
            }

            private double throttle;
            public double Throttle
            {
                get { return this.throttle; }
                set
                {
                    if (this.throttle != value)
                    {
                        this.throttle = value;
                    }
                }
            }
            #endregion
            #region Joystick Relative X & Y
            // Suitable properties for joystick
            private double joyRelativeX;
            public double JoyRelativeX
            {
                get { return this.joyRelativeX; }
                set
                {
                    if (this.joyRelativeX != value)
                    {
                        this.joyRelativeX = value;
                    }
                }
            }

            private double joyRelativeY;
            public double JoyRelativeY
            {
                get { return this.joyRelativeY; }
                set
                {
                    if (this.joyRelativeY != value)
                    {
                        this.joyRelativeY = value;
                    }
                }
            }
            #endregion
            public Controls()
            {
                InitializeComponent();
            }

            // If aileron slider value changed - set new value of aileron at VM
            private void AileronSld_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
            {
                if (Aileron != e.NewValue)
                {
                    double aileronToShow;
                    Aileron = e.NewValue;
                    (Application.Current as App).Controls.setAileron(Aileron);
                    aileronToShow = Math.Round(Aileron, 4);
                    this.Aileron_val.Text = aileronToShow.ToString();
                }
            }

            // If throttle slider value changed - set new value of throttle at VM
            private void ThrottleSld_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
            {
                if (Throttle != e.NewValue)
                {
                    double throttleToShow;
                    Throttle = e.NewValue;
                    (Application.Current as App).Controls.setThrottle(Throttle);
                    throttleToShow = Math.Round(Throttle, 4);
                    this.Throttle_val.Text = throttleToShow.ToString();
                }
            }

            private void joy1_MouseMove(object sender, MouseEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    double xToShow, yToShow;
                    joyRelativeX = (joy1.knobPosition.X / 100);
                    joyRelativeY = (joy1.knobPosition.Y / 100);
                    (Application.Current as App).Controls.setJoyValues(joyRelativeX, joyRelativeY);
                    xToShow = Math.Round(joyRelativeX, 4);
                    yToShow = Math.Round(joyRelativeY, 4);
                    Rudder_val.Text = xToShow.ToString();
                    Elevator_val.Text = yToShow.ToString();
                }
            }

            private void joy1_MouseUp(object sender, MouseButtonEventArgs e)
            {
                joyRelativeX = 0;
                joyRelativeY = 0;
                Rudder_val.Text = joyRelativeX.ToString();
                Elevator_val.Text = joyRelativeY.ToString();
                (Application.Current as App).Controls.setJoyValues(joyRelativeX, joyRelativeY);
            }
        // Restart sliders' value
        public void restartControls()
        {
            ThrottleSld.Value = 0;
            AileronSld.Value = 0;
        }
    }

    }

