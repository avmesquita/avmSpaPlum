using SpaPlum.Web.Contexto;
using SpaPlum.Web.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;
using System.Xml.Linq;

namespace SpaPlum.Web.Service
{
    //private AgendamentoContexto db = new AgendamentoContexto();

    public enum SitemapFrequency
    {
        Never,
        Yearly,
        Monthly,
        Weekly,
        Daily,
        Hourly,
        Always
    }

    public class SitemapNode
    {
        public SitemapFrequency? Frequency { get; set; }
        public DateTime? LastModified { get; set; }
        public double? Priority { get; set; }
        public string Url { get; set; }

        /*
        public IReadOnlyCollection<SitemapNode> GetSitemapNodes(UrlHelper urlHelper)
        {
            List<SitemapNode> nodes = new List<SitemapNode>();

            nodes.Add(
                new SitemapNode()
                {
                    Url = urlHelper.AbsoluteRouteUrl("HomeGetIndex"),
                    Priority = 1
                });
            nodes.Add(
               new SitemapNode()
               {
                   Url = urlHelper.AbsoluteRouteUrl("HomeGetAbout"),
                   Priority = 0.9
               });
            nodes.Add(
               new SitemapNode()
               {
                   Url = urlHelper.AbsoluteRouteUrl("HomeGetContact"),
                   Priority = 0.9
               });

            foreach (int productId in productRepository.GetProductIds())
            {
                nodes.Add(
                   new SitemapNode()
                   {
                       Url = urlHelper.AbsoluteRouteUrl("ProductGetProduct", new { id = productId }),
                       Frequency = SitemapFrequency.Weekly,
                       Priority = 0.8
                   });
            }

            return nodes;
        }

        public class UrlHelperExtensions
        {
            public static string AbsoluteRouteUrl(
                this UrlHelper urlHelper,
                string routeName,
                object routeValues = null)
            {
                string scheme = urlHelper.RequestContext.HttpContext.Request.Url.Scheme;
                return urlHelper.RouteUrl(routeName, routeValues, scheme);
            }
        }

        public string GetSitemapDocument(IEnumerable<SitemapNode> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");

            foreach (SitemapNode sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),
                    sitemapNode.LastModified == null ? null : new XElement(
                        xmlns + "lastmod",
                        sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    sitemapNode.Frequency == null ? null : new XElement(
                        xmlns + "changefreq",
                        sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                    sitemapNode.Priority == null ? null : new XElement(
                        xmlns + "priority",
                        sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);
            }

            XDocument document = new XDocument(root);
            return document.ToString();
        }*/
    }
}