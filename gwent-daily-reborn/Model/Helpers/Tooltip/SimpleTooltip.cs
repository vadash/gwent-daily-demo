using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace gwent_daily_reborn.Model.Helpers.Tooltip
{
    public sealed class SimpleTooltip : Form, ITooltip
    {
        private readonly Label _label;
        private readonly Rectangle Dimensions;

        public SimpleTooltip()
        {
            SuspendLayout();
            Dimensions = Screen.FromControl(this).Bounds;

            AutoScaleDimensions = new SizeF(6F, 50F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "";

            _label = new Label
            {
                UseMnemonic = false,
                Top = 1,
                Left = 1,
                Name = "label"
            };
            Text = "";

            FormBorderStyle = FormBorderStyle.None;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Padding = new Padding(3);
            StartPosition = FormStartPosition.Manual;
            ShowInTaskbar = false;
            Location = new Point(0, 0);
            Load += ToolTip_Load;
            ResumeLayout(false);
        }

        private static int Position { get; set; }

        // http://stackoverflow.com/questions/6328962/form-without-focus-activation
        protected override bool ShowWithoutActivation => true;

        public override string Text
        {
            // ReSharper disable once ArrangeAccessorOwnerBody
            get => base.Text;
            set
            {
                base.Text = value;
                _label.Text = value;
                Invalidate();
            }
        }

        private int LocationX
        {
            set
            {
                var location = Location;
                location.X = value;
                Location = location;
            }
        }

        private int LocationY
        {
            set
            {
                var location = Location;
                location.Y = value;
                Location = location;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var createParams = base.CreateParams;
                createParams.ExStyle =
                    0x00000008 | 0x08000000 | 0x00000080; // WS_EX_TOPMOST | WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW 
                return createParams;
            }
        }

        public void Show(string text, int time = 5000)
        {
            var t = new Thread(() => RealStart(text, time));
            t.Start();
        }

        private void RealStart(string text, int time = 5000)
        {
            if (Position >= 180)
                Position = 0;
            var tooltip = new SimpleTooltip
            {
                Text = text,
                LocationX = (int) (Dimensions.Width * 0.9f - 50f),
                LocationY = Position
            };
            Position += 20;
            tooltip.Show();
            tooltip.Refresh();
            Thread.Sleep(time);
            tooltip.Close();
        }

        private void ToolTip_Load(object sender, EventArgs e)
        {
            _label.Text = Text;
            _label.AutoSize = true;
            Controls.Add(_label);
        }
    }
}
