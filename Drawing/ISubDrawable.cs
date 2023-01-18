using Microsoft.Xna.Framework;

namespace Fantasy.Engine.Drawing
{
	internal interface ISubDrawable
	{
		bool IsVisible { get; set; }
		
		void Draw(GameTime gameTime);
	}
}
