using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace FlightSimulator.Views
{
    public partial class Joystick : UserControl
    {
        Storyboard joyAnimation;
        private Point location = new Point();

        public Joystick()
        {
            InitializeComponent();
            joyAnimation = Knob.FindResource("JoystickAnimation") as Storyboard;
            joyAnimation.Begin();
            joyAnimation.Stop();
        }
        //Event handler for knob animation
        private void CenterKnob_Completed(Object sender, EventArgs e)
        {
            joyAnimation.Stop();
            knobPosition.X = 0;
            knobPosition.Y = 0;
        }
        // Event handler for a mouse press
        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.location = e.GetPosition(this);
                Mouse.Capture(this.KnobBase);
            }
        }
        // Event handler for a mouse moving (while pressed)
        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            double x, y, border;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                x = (e.GetPosition(this).X - location.X);
                y = (e.GetPosition(this).Y - location.Y);
                border = Math.Sqrt(x * x + y * y);
                if ((Base.Width / 2) > border)
                {
                    knobPosition.X = x;
                    knobPosition.Y = y;
                }
            }
        }
        //Event handler for leaving the mouse button
        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            joyAnimation.Begin();
        }
    }
    }
