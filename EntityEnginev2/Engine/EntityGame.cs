using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEnginev2.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev2.Engine
{
    public class EntityGame
    {
        public bool Paused { get; protected set; }

        public delegate void EntityGameEventHandler(EntityState es);

        public event EntityGameEventHandler StateChange;

        public Rectangle Viewport { get; protected set; }

        public Game Game { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public GameTime GameTime { get; private set; }
        private EntityState _currentstate;
        public Color BGColor = Color.White;

        public EntityState CurrentState
        {
            get { return _currentstate; }
            set
            {
                _currentstate = value;
                if (StateChange != null)
                    StateChange(_currentstate);
            }
        }

        public EntityGame(Game game, GraphicsDeviceManager g, Rectangle viewport, SpriteBatch spriteBatch)
        {
            Game = game;
            SpriteBatch = spriteBatch;

            game.Components.Add(new InputHandler(game));

            Paused = false;

            Viewport = viewport;
            MakeWindow(g);
        }

        public void MakeWindow(GraphicsDeviceManager g)
        {
            if ((Viewport.Width > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width) ||
                (Viewport.Height > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)) return;
            g.PreferredBackBufferWidth = Viewport.Width;
            g.PreferredBackBufferHeight = Viewport.Height;
            g.IsFullScreen = false;
            g.ApplyChanges();
        }

        public virtual void Update(GameTime gt)
        {
            GameTime = gt;
            CurrentState.Update();
        }

        public virtual void Draw()
        {
            Game.GraphicsDevice.Clear(BGColor);
            SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp,
                              DepthStencilState.None, RasterizerState.CullCounterClockwise);
            CurrentState.Draw(SpriteBatch);
            SpriteBatch.End();
        }

        public virtual void Pause()
        {
            Paused = true;
        }

        public virtual void Unpause()
        {
            Paused = false;
        }
    }
}
