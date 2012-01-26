﻿using System.Linq;
using DeskMetrics;
using Ninject;
using NzbDrone.Common;

namespace NzbDrone.Core.Providers
{
    public class AnalyticsProvider
    {
        private readonly IDeskMetricsClient _deskMetricsClient;
        public const string DESKMETRICS_TEST_ID = "4ea8d347a14ad71442000002";
        public const string DESKMETRICS_PRODUCTION_ID = "4f20b01ea14ad729b2000000";

        [Inject]
        public AnalyticsProvider(IDeskMetricsClient deskMetricsClient)
        {
            _deskMetricsClient = deskMetricsClient;
        }

        public AnalyticsProvider()
        {

        }

        public virtual void Checkpoint()
        {
            if (EnviromentProvider.IsNewInstall)
            {
                _deskMetricsClient.RegisterInstall();
            }

            if (_deskMetricsClient.Started)
            {
                _deskMetricsClient.Stop();
            }
            
            _deskMetricsClient.Start();
        }
    }
}
