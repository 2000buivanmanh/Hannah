using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using CKSource.CKFinder.Connector.Config;
using CKSource.CKFinder.Connector.Core;
using CKSource.CKFinder.Connector.Core.Acl;
using CKSource.CKFinder.Connector.Core.Authentication;
using CKSource.CKFinder.Connector.Core.Builders;
using CKSource.CKFinder.Connector.Host.Owin;
using CKSource.CKFinder.Connector.KeyValue.FileSystem;
using CKSource.FileSystem.Local;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HANNAH_NEW_VERSION.Startup))]

namespace HANNAH_NEW_VERSION
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            FileSystemFactory.RegisterFileSystem<LocalStorage>();
            app.Map("/ckfinder/connector", SetupConnector);
        }


        private static void SetupConnector(IAppBuilder app)
        {
            /*
             * Create a connector instance using ConnectorBuilder. The call to the LoadConfig() method
             * will configure the connector using CKFinder configuration options defined in Web.config.
             */
            var connectorFactory = new OwinConnectorFactory();
            var connectorBuilder = new ConnectorBuilder();

            /*
             * Create an instance of authenticator implemented in the previous step.
             */
            var customAuthenticator = new CustomCKFinderAuthenticator();


            connectorBuilder
                /*
                 * Provide the global configuration.
                 *
                 * If you installed CKSource.CKFinder.Connector.Config, you should load the static configuration
                 * from XML:
                 * connectorBuilder.LoadConfig();
                 */
                .LoadConfig()
                .SetAuthenticator(customAuthenticator)
                .SetRequestConfiguration(
                    (request, config) =>
                    {
                        /*
                         * If you installed CKSource.CKFinder.Connector.Config, you might want to load the static
                         * configuration from XML as a base configuration to modify:
                         */
                        config.LoadConfig();

                        /*
                         * Configure settings per request.
                         *
                         * The minimal configuration has to include at least one backend, one resource type
                         * and one ACL rule.
                         *
                         * For example:
                         */

                        config.AddAclRule(new AclRule(
                            new StringMatcher("*"),
                            new StringMatcher("*"),
                            new StringMatcher("*"),
                            new Dictionary<Permission, PermissionType> { { Permission.All, PermissionType.Allow } }));

                        /*
                         * If you installed CKSource.CKFinder.Connector.KeyValue.FileSystem, you may enable caching:
                         */

                        var defaultBackend = config.GetBackend("default");
                        var keyValueStoreProvider = new FileSystemKeyValueStoreProvider(defaultBackend);
                        config.SetKeyValueStoreProvider(keyValueStoreProvider);
                    }
                );

            /*
             * Build the connector middleware.
             */
            var connector = connectorBuilder
                .Build(connectorFactory);

            /*
             * Add the CKFinder connector middleware to the web application pipeline.
             */
            app.UseConnector(connector);
        }

        public class CustomCKFinderAuthenticator : IAuthenticator
        {
            public Task<IUser> AuthenticateAsync(ICommandRequest commandRequest, CancellationToken cancellationToken)
            {
                var claimsPrincipal = commandRequest.Principal as ClaimsPrincipal;

                var roles = claimsPrincipal?.Claims?.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToArray();

                var isAuthenticated = claimsPrincipal.Identity.IsAuthenticated;

                var user = new User(true, roles);
                return Task.FromResult((IUser)user);
            }
        }

    }
}