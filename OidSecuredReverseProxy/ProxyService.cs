// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Net.Http;
using Microsoft.Extensions.Options;

namespace OidSecuredReverseProxy
{
    public class ProxyService
    {
        public ProxyService(IOptions<ProxyOptions> options, IOptions<SharedProxyOptions> sharedOptions)
        {
            Options = options.Value;
            SharedOptions = sharedOptions.Value;
            Client = new HttpClient(SharedOptions.MessageHandler ?? new HttpClientHandler { AllowAutoRedirect = false, UseCookies = false });
        }

        public ProxyOptions Options { get; private set; }
        public SharedProxyOptions SharedOptions { get; private set; }
        internal HttpClient Client { get; private set; }
    }
}
