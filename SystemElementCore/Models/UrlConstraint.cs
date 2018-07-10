using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemElementCore.Models
{
    public class UrlConstraint : IRouteConstraint
    {
        private IElementRepository elementRepository;

        public UrlConstraint(IElementRepository repository)
        {
            elementRepository = repository;
        }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values[routeKey] != null)
            {
                var permalink = values[routeKey].ToString();
                if (elementRepository.findParentElementByPermalink(permalink) != null)
                {
                    return true;
                }
                else
                {
                    if (permalink.IndexOf('/') != -1)
                    {
                        string[] arrayParams = permalink.Split('/');
                        int counterTrueElements = 0;
                        foreach (string param in arrayParams)
                        {
                            if (elementRepository.findParentElementByPermalink(permalink) != null)
                            {
                                counterTrueElements++;
                            }
                        }
                        return counterTrueElements == arrayParams.Length;
                    }
                }
            }
            return false;
        }
    }
}
