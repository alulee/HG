using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using HGenealogy.Models.Boards;

namespace HGenealogy.Validators.Boards
{
    public class EditForumTopicValidator : BaseNopValidator<EditForumTopicModel>
    {
        public EditForumTopicValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Subject).NotEmpty().WithMessage(localizationService.GetResource("Forum.TopicSubjectCannotBeEmpty"));
            RuleFor(x => x.Text).NotEmpty().WithMessage(localizationService.GetResource("Forum.TextCannotBeEmpty"));
        }
    }
}