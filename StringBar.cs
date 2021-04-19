using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TelemetryViewer
{
    class StringBar : Component
    {
        private SpriteFont spriteFont;
        private string message;
        private bool status;
        private Game game;
        private Texture2D backgroundOK;
        private Texture2D backgroundBad;
        private Vector2 position;
        private Color colorOK = Color.Green;
        private Color colorBad = Color.DarkRed;

        private void UpdateVectors()
        {
            position = new Vector2(game.GraphicsDevice.Viewport.X,game.GraphicsDevice.Viewport.Height - spriteFont.MeasureString(" ").Y);
        }
        public StringBar(Game game)
        {
            message = "";
            this.game = game;
            status = false;
        }
        public StringBar(Game game, string message, bool status)
        {
            this.message = message;
            this.game = game;
            this.status = status;
        }

        public bool Status { get => status; set => status = value; }
        public string Message { get => message; set => message = value; }

        public void Load(string assetName)
        {
            spriteFont = game.Content.Load<SpriteFont>(assetName);
            backgroundOK = new Texture2D(game.GraphicsDevice, 16, 16);
            backgroundBad = new Texture2D(game.GraphicsDevice, 16, 16);

            Color[] DataColorsBad = new Color[16 * 16];
            Color[] DataColorsOK = new Color[16 * 16];
            for (int i = 0; i < DataColorsBad.Length; i++)
            {
                DataColorsBad[i] = colorBad;
                DataColorsOK[i] = colorOK;
            }
            backgroundBad.SetData<Color>(DataColorsBad);
            backgroundOK.SetData<Color>(DataColorsOK);
            UpdateVectors();

        }
        public override void Update(GameTime gameTime)
        {
            UpdateVectors();
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(status == true)
            {
                spriteBatch.Draw(backgroundOK, 
                   new Rectangle(
                       (int)position.X,
                       (int)position.Y,
                       game.GraphicsDevice.Viewport.Width,
                       game.GraphicsDevice.Viewport.Height),
                   Color.White);
            }
            else
            {
                spriteBatch.Draw(backgroundBad,
                   new Rectangle(
                       (int)position.X,
                       (int)position.Y,
                       game.GraphicsDevice.Viewport.Width,
                       game.GraphicsDevice.Viewport.Height),
                   Color.White);
            }

            spriteBatch.DrawString(spriteFont, message, position, Color.Black);
        }
    }
}
