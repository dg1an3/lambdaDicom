using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OidSecuredReverseProxy;

namespace OidSecuredReverseProxy.Controllers
{
    public class ProxyController : ControllerBase
    {
        ProxyService _proxyService;

        public ProxyController(ProxyService proxyService)
        {
            _proxyService = proxyService;
        }

        [Authorize]
        [Route("{*url}")]
        public async Task ProxyRequest(string url)
        {
#if HELLO_STRING
            await this.HttpContext.Response.WriteAsync($"Hello {url}");
#else
            var uri = new Uri(UriHelper.BuildAbsolute(_proxyService.Options.Scheme, 
                _proxyService.Options.Host, 
                _proxyService.Options.PathBase, 
                this.HttpContext.Request.Path, 
                this.HttpContext.Request.QueryString.Add(_proxyService.Options.AppendQuery)));
            await this.HttpContext.ProxyRequest(uri);
#endif
        }
    }
}
