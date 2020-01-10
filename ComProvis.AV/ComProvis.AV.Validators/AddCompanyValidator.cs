using ComProvis.AV.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComProvis.AV.Validators
{
    public class AddCompanyValidator : AbstractValidator<Company>
    {
        private IDataService _dataService { get; set; }
        public AddCompanyValidator(IDataService dataService)
        {
            _dataService = dataService;

            RuleFor(x => x.ExternalId).
                NotEmpty().WithMessage(Resource.ErrObjectIsNull).
                Must(CustomerExist).WithMessage(Resource.ErrCustomerExist);
            RuleFor(x => x.Name).
                Length(1, 255).WithMessage(Resource.ErrObjectLength255).
                NotEmpty().WithMessage(Resource.ErrObjectIsNull);
            RuleFor(x => x.ContactEmail).
               Length(1, 255).WithMessage(Resource.ErrObjectLength255).
               Matches(Resource.RegexEmail).WithMessage(Resource.ErrObjectIsNotValid);
        }

        bool CustomerExist(string externalCustomerId)
        {
            var product = _dataService.CompanyService.ExternalGet(externalCustomerId);
            if (product?.Id != null) return false;
            return true;
        }
    }
}
