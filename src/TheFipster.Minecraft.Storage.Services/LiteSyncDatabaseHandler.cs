﻿using LiteDB;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Storage.Services
{
    public class LiteSyncDatabaseHandler : ISyncDatabaseHandler
    {
        private const string DatabaseFilename = "sync.litedb";

        private readonly LiteDatabaseHandler _handler;

        public LiteSyncDatabaseHandler(IConfigService config)
            => _handler = new LiteDatabaseHandler(config, DatabaseFilename);

        public ILiteCollection<T> GetCollection<T>()
            => _handler.Database.GetCollection<T>();

        public ILiteCollection<T> GetCollection<T>(string name)
            => _handler.Database.GetCollection<T>(name);
    }
}