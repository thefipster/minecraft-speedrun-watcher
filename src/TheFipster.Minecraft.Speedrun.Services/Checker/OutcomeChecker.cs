﻿using System;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class OutcomeChecker : IOutcomeChecker
    {
        public OutcomeResult Check(RunInfo run)
        {
            if (run.Splits.Any())
                return outcomeFromSplits(run);

            return outcomeFromEvents(run);
        }

        private OutcomeResult outcomeFromEvents(RunInfo run)
        {
            if (run.Events.Any(x => x.Data == "Free the End"))
            {
                var dragonKillTime = run.Events.Where(x => x.Data == "Free the End").Select(x => x.Timestamp).Min(x => x);
                var worldCreationTime = run.World.CreatedOn.ToLocalTime();
                var delta = dragonKillTime - worldCreationTime - TimeSpan.FromSeconds(15);
                var outcome = new OutcomeResult(Outcomes.Finished);
                outcome.Time = delta;
                return outcome;
            }

            if (run.Events.Any(x => x.Data == "The End?"))
                return new OutcomeResult(Outcomes.ResetEnd);

            if (run.Events.Any(x => x.Data == "Eye Spy"))
                return new OutcomeResult(Outcomes.ResetStronghold);

            if (run.Events.Any(x => x.Data == "We Need to Go Deeper"))
                return new OutcomeResult(Outcomes.ResetNether);

            return new OutcomeResult(Outcomes.ResetSpawn);
        }

        private OutcomeResult outcomeFromSplits(RunInfo run)
        {
            if (run.Splits.Any(x => x.Type == SplitTypes.ApproxFinish))
            {
                var outcome = new OutcomeResult(Outcomes.Finished);
                outcome.Time = run.Splits.First(x => x.Type == SplitTypes.ApproxFinish).Timestamp;
                return outcome;
            }

            if (run.Splits.Any(x => x.Type == SplitTypes.TheEnd))
                return new OutcomeResult(Outcomes.ResetEnd);

            if (run.Splits.Any(x => x.Type == SplitTypes.Stronghold))
                return new OutcomeResult(Outcomes.ResetStronghold);

            if (run.Splits.Any(x => x.Type == SplitTypes.Nether))
                return new OutcomeResult(Outcomes.ResetNether);

            return new OutcomeResult(Outcomes.ResetSpawn);
        }
    }
}
