using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens.Transitions;

namespace MonoGame.Extended.Screens
{
    public class ScreenManager : SimpleDrawableGameComponent
    {
        public ScreenManager()
        {
        }

        private Screen _activeScreen;

        public Screen ActiveScreen => _activeScreen;

        //private bool _isInitialized;
        //private bool _isLoaded;
        private Transition _activeTransition;

        public void LoadScreen(Screen screen, Transition transition)
        {
            _activeTransition = transition;
            _activeTransition.StateChanged += (sender, args) => LoadScreen(screen);
            _activeTransition.Completed += (sender, args) =>
            {
                _activeTransition.Dispose();
                _activeTransition = null;
            };
        }

        public void LoadScreen(Screen screen)
        {
            _activeScreen?.UnloadContent();
            _activeScreen?.Dispose();

            _activeScreen = screen;

            screen.ScreenManager = this;

            screen.Initialize();
            screen.LoadContent();

        }

        public override void Initialize()
        {
            base.Initialize();
            _activeScreen?.Initialize();
            //_isInitialized = true;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _activeScreen?.LoadContent();
            //_isLoaded = true;
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            _activeScreen?.UnloadContent();
            //_isLoaded = false;
        }

        public override void Update(GameTime gameTime)
        {
            _activeScreen?.Update(gameTime);
            _activeTransition?.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _activeScreen?.Draw(gameTime);
            _activeTransition?.Draw(gameTime);
        }
    }
}
