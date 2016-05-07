using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scroll1
{
    class Marker : asd.TextureObject2D
    {
        public Marker()
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Coil.png");
            Position = asd.Engine.Mouse.Position + new asd.Vector2DF(Program.AxisX - asd.Engine.WindowSize.X / 2, Program.AxisY - asd.Engine.WindowSize.Y / 2);
            CenterPosition = Texture.Size.To2DF()/2;  
        }

        protected override void OnUpdate()
        {
            if (asd.Engine.Mouse.Position.Length == 0)
            {
                IsDrawn = false;
                return;
            }
            IsDrawn = true;
            Position = asd.Engine.Mouse.Position + new asd.Vector2DF(Program.AxisX - asd.Engine.WindowSize.X / 3, Program.AxisY - asd.Engine.WindowSize.Y / 2);
            asd.Vector2DF position = Position;

            position.X = asd.MathHelper.Clamp(position.X, Program.AxisX + asd.Engine.WindowSize.X * (1/ 2.0f + 1/6.0f) + 1 - Texture.Size.X / 2, Program.AxisX - asd.Engine.WindowSize.X * (1/ 2.0f - 1/6.0f ) + Texture.Size.X / 2);
            position.Y = asd.MathHelper.Clamp(position.Y, Program.AxisY + asd.Engine.WindowSize.Y / 2 - Texture.Size.Y / 2, Program.AxisY - asd.Engine.WindowSize.Y / 2 + Texture.Size.Y / 2);
            Position = position;

            if (asd.Engine.Mouse.LeftButton.ButtonState == asd.MouseButtonState.Push && this.Children.Count() < 2)
            {
                this.AddChild(new Skewer(Parent.Position, this.Position - Parent.Position), asd.ChildManagementMode.RegistrationToLayer, asd.ChildTransformingMode.Nothing);
            }
        }
    }
}
