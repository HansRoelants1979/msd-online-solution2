﻿using System;
using System.ServiceModel;
using Microsoft.Xrm.Sdk;
using Tc.Crm.Plugins.User.BusinessLogic;

namespace Tc.Crm.Plugins.User
{
    public class PostAssociateUserToTeam : IPlugin
    {
        public string[] businessUnitNames;
        public PostAssociateUserToTeam(string unSecureConfig, string SecureConfig)
        {
            if (!string.IsNullOrWhiteSpace(unSecureConfig))
                businessUnitNames = unSecureConfig.Split(',');
        }

        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            try
            {
                trace.Trace("Begin - AssociateUserToTeamService");
                AssociateUserToTeamService associateUserToTeamService = new AssociateUserToTeamService(context, trace, service, businessUnitNames);
                associateUserToTeamService.DoActionsOnUserAssociate();
                trace.Trace("End - AssociateUserToTeamService");
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                throw new InvalidPluginExecutionException(ex.ToString());
            }
            catch (TimeoutException ex)
            {
                throw new InvalidPluginExecutionException(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(ex.ToString());
            }
        }

    }
}