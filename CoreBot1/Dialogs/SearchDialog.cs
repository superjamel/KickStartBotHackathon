// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.3.0

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;

namespace CoreBot1.Dialogs
{
    public class SearchDialog : ComponentDialog
    {
        public SearchDialog()
            : base(nameof(SearchDialog))
        {
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            //Register the step functions
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                StepWhichDocument,
                SearchTermStepAsync,
                SpecificPersonConfirmStepAsync,
                SpecificPersonStepAsync

            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }
        //Execute the first step
        private async Task<DialogTurnResult> StepWhichDocument(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var bookingDetails = (DocumentSearchDetails)stepContext.Options;

            if (bookingDetails.DocumentType == null)
            {
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("Sorry I don't understand you. Ask me to search a Document to start.") }, cancellationToken);
            }
            else
            {
                return await stepContext.PromptAsync(nameof(ChoicePrompt),
                                new PromptOptions
                                {
                                    Prompt = MessageFactory.Text("Wat voor soort document zoekt u?"),
                                    Choices = ChoiceFactory.ToChoices(new List<string> { "Presentatie", "PDF", "Afbeelding", "Alle" }),
                                }, cancellationToken);
            }
        }

        private async Task<DialogTurnResult> SearchTermStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            throw new System.Exception("Not implemented yet.");
        }

        private async Task<DialogTurnResult> SpecificPersonConfirmStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            throw new System.Exception("Not implemented yet.");
        }
        private async Task<DialogTurnResult> SpecificPersonStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            throw new System.Exception("Not implemented yet.");
        }

    }
}
