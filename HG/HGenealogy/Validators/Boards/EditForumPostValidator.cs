using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using HGenealogy.Models.Boards;

namespace HGenealogy.Validators.Boards
{
    public class EditForumPostValidator : BaseNopValidator<EditForumPostModel>
    {
        public EditForumPostValidator(ILocalizationService localizationService)
        {            
            RuleFor(x => x.Text).NotEmpty().WithMessage(localizationService.GetResource("Forum.TextCannotBeEmpty"));
        }
    }
}