// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.3.0

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoreBot1
{
    public static class LuisHelper
    {
        public static async Task<DocumentSearchDetails> ExecuteLuisQuery(IConfiguration configuration, ILogger logger, ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var bookingDetails = new DocumentSearchDetails();

            try
            {
                // Create the LUIS settings from configuration.
                var luisApplication = new LuisApplication(
                    configuration["LuisAppId"],
                    configuration["LuisAPIKey"],
                    "https://" + configuration["LuisAPIHostName"]
                );

                var recognizer = new LuisRecognizer(luisApplication);

                // The actual call to LUIS
                var recognizerResult = await recognizer.RecognizeAsync(turnContext, cancellationToken);

                var (intent, score) = recognizerResult.GetTopScoringIntent();
                if (intent == "zoek_document")
                {
                    // We need to get the result from the LUIS JSON which at every level returns an array.
                    bookingDetails.SearchTerm = recognizerResult.Entities["zoekterm"] != null ? recognizerResult.Entities["zoekterm"].First.ToString() : null; ;
                    bookingDetails.DocumentType = recognizerResult.Entities["documenttype"] != null ? recognizerResult.Entities["documenttype"].First.ToString() : null;
                    bookingDetails.Person = recognizerResult.Entities["persoon"] != null ? recognizerResult.Entities["persoon"].First.ToString() : null; ;
                }
                else
                {
                    bookingDetails.SearchTerm = null;
                    bookingDetails.DocumentType = null;
                    bookingDetails.Person = null;
                }
            }
            catch (Exception e)
            {
                logger.LogWarning($"LUIS Exception: {e.Message} Check your LUIS configuration.");
            }

            return bookingDetails;
        }
    }
}
