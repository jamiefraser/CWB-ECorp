﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ECorp.UI
{
    public class CWBApiAuthorizationHandler : AuthorizationMessageHandler
    {
        public CWBApiAuthorizationHandler(IAccessTokenProvider provider,
        NavigationManager navigationManager)
        : base(provider, navigationManager)
        {
            ConfigureHandler(
                authorizedUrls: new[] { "https://localhost:44352/api/" },
                scopes: new[] { "https://nsdevelopment.onmicrosoft.com/DualBlazor/user_impersonation" });
        }
    }
}
