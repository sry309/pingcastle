﻿//
// Copyright (c) Ping Castle. All rights reserved.
// https://www.pingcastle.com
//
// Licensed under the Non-Profit OSL. See LICENSE file in the project root for full license information.
//
using System;
using System.Collections.Generic;
using System.Text;
using PingCastle.Rules;

namespace PingCastle.Healthcheck.Rules
{
	[RuleModel("A-LMHashAuthorized", RiskRuleCategory.Anomalies, RiskModelCategory.NetworkSniffing)]
	[RuleComputation(RuleComputationType.TriggerOnPresence, 5)]
	[RuleANSSI("R37", "paragraph.3.6.2.1")]
	[RuleBSI("M 2.412")]
    public class HeatlcheckRuleAnomalyLMHash : RuleBase<HealthcheckData>
    {
		protected override int? AnalyzeDataNew(HealthcheckData healthcheckData)
        {
            foreach (GPPSecurityPolicy policy in healthcheckData.GPPPasswordPolicy)
            {
                foreach (GPPSecurityPolicyProperty property in policy.Properties)
                {
                    if (property.Property == "LmCompatibilityLevel" || property.Property == "NoLMHash")
                    {
                        if (property.Value == 0)
                        {
							AddRawDetail(policy.GPOName);
							break;
                        }
                    }
                }
            }
			return null;
        }
    }
}
