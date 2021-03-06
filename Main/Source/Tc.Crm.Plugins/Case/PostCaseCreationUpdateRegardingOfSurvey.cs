﻿using System;
using System.ServiceModel;
using Microsoft.Xrm.Sdk;
using Tc.Crm.Plugins.Case.BusinessLogic;

namespace Tc.Crm.Plugins.Case
{
    public class PostCaseCreationUpdateRegardingOfSurvey : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            try
            {
                trace.Trace("Begin - PostCaseCreationUpdateRegardingOfSurvey");
                UpdateRegardingOfSurveyService surveyService = new UpdateRegardingOfSurveyService(context, trace, service);
                surveyService.DoActionsOnCreateCase();
                trace.Trace("End - PostCaseCreationUpdateRegardingOfSurvey");
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
