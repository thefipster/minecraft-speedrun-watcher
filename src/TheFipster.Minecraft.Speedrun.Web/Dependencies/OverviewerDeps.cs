﻿using SimpleInjector;
using TheFipster.Minecraft.Modules.Abstractions;
using TheFipster.Minecraft.Modules.Components;
using TheFipster.Minecraft.Overview.Abstractions;
using TheFipster.Minecraft.Overview.Services;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public static class OverviewerDeps
    {
        public static void RegisterOverviewer(this Container container)
        {
            container.Register<IOverviewerDatabaseHandler, LiteOverviewerDatabaseHandler>(Lifestyle.Singleton);
            container.Register<IRenderQueue, RenderQueue>(Lifestyle.Singleton);

            container.Register<IJobReader, JobReader>(Lifestyle.Scoped);
            container.Register<IJobWriter, JobWriter>(Lifestyle.Scoped);
            container.Register<IJobHandler, JobHandler>(Lifestyle.Scoped);

            container.Register<IMapRenderer, OverviewerRenderer>();
            container.Register<IJobPrioritizer, JobPrioritizer>();

            container.Register<IMapRenderModule, MapRenderModule>();

        }
    }
}
