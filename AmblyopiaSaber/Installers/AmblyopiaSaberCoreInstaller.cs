﻿using AmblyopiaSaber.Config;
using SiraUtil.Interfaces;
using Zenject;
using SiraUtil.Objects.Beatmap;
using SiraUtil.Extras;
using AmblyopiaSaber.Data;

namespace AmblyopiaSaber.Installers
{
    internal class AmblyopiaSaberCoreInstaller : Installer<PluginConfig, AmblyopiaSaberCoreInstaller>
    {
        private readonly PluginConfig _pluginConfig;

        public AmblyopiaSaberCoreInstaller(PluginConfig pluginConfig)
        {
            _pluginConfig = pluginConfig;
            if (!_pluginConfig.LoadedYet)
            {
                _pluginConfig.LoadedYet = true;
                _pluginConfig.LeftNoteData.RightEyeVisibility = 0.5f;
                _pluginConfig.RightNoteData.LeftEyeVisibility = 0.5f;
            }
        }
        public override void InstallBindings()
        {
            Container.BindInstance(_pluginConfig).AsSingle();
            Container.RegisterRedecorator(new BasicNoteRegistration(Modify));
            //Container.Bind(typeof(IModelProvider), typeof(GameNoteProvider)).To<GameNoteProvider>().AsSingle();
            //Container.Bind(typeof(IModelProvider), typeof(CustomBombNoteProvider)).To<CustomBombNoteProvider>().AsSingle();
        }

        public GameNoteController Modify(GameNoteController original)
        {
            //if (!CanSetup) return original;
            original.gameObject.AddComponent<AmblyopiaController>();
            return original;
        }
    }
}
